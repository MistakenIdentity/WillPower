//
// ********************************************************************************************************
// ********************************************************************************************************
// ***                                                                                                  ***
// *** Code Copyright © 2020, Will `Willow' Osborn.                                                     ***
// ***                                                                                                  ***
// *** This code is provided 'AS IS, NO WARRANTY' and is intended for no specific use or person.        ***
// *** In fact, the code herein is so confuggled, it should not be used by ANYONE EVER and ANYTHING     ***
// *** that happens as a result of its use is COMPLETELY and UTTERLY YOUR FAULT.  :p                    ***
// ***                                                                                                  ***
// *** You have my permission to extract, copy, modify, steal, borrow, beg, fold, spindle, mutilate or  ***
// *** otherwise abuse the code herein PROVIDED YOU LEAVE ME OUT OF IT! You Acknowledge and Accept      ***
// *** FULL and SOLE responsibility and culpability for ANYTHING you do with or around it.              ***
// ***                                                                                                  ***
// ********************************************************************************************************
// ********************************************************************************************************
//

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WillPower
{
    /// <summary>
    /// A class for managing active tasks without any complex magic.
    /// Why isn't something like this part of .Net?
    /// Can be inherited because I was too lazy to make an Interface.
    /// </summary>
    public class TaskManager : IDisposable
    {
        const uint MAXTHREADSDEFAULT = 256;
        bool disposing = false;

        /// <summary>
        /// The delegate handler for <see cref="TaskManager.OnThreadException">OnThreadException</see>.
        /// </summary>
        /// <param name="sender"><see cref="TaskManager">This</see> instance.</param>
        /// <param name="exception">The <see cref="System.Exception">Exception</see> thrown.</param>
        /// <param name="action">The relevant <see cref="AbortableTask">AbortableTask</see>, if any.</param>
        public delegate void OnThreadExceptionHandler(TaskManager sender, Exception exception, AbortableTask action = null);
        /// <summary>
        /// Fires when a <see cref="TaskManager.PendingTasks">Pending Task</see> is fails to execute.
        /// If this event is not handled, an unhandled <see cref="System.Exception">Exception</see> could 
        /// be thrown in the background thread and should be handled elsewhere.
        /// </summary>
        public event OnThreadExceptionHandler OnThreadException;

        /// <summary>
        /// The <see cref="System.TimeSpan">System.TimeSpan</see> to wait for an available thread prior to throwing a 
        /// <see cref="System.TimeoutException">TimeoutException</see>.
        /// Default is 10 seconds.
        /// </summary>
        public TimeSpan TimeOut { get; set; }

        /// <summary>
        /// The <see cref="System.TimeSpan">System.TimeSpan</see> to wait for all threads to complete prior to throwing a 
        /// <see cref="System.TimeoutException">TimeoutException</see>.
        /// Default is 1 hour (60 minutes).
        /// </summary>
        public TimeSpan TimeOutAll { get; set; }

        /// <summary>
        /// The default <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see> of the 
        /// <see cref="System.Threading.Tasks.TaskFactory">TaskFactory's</see> <see cref="System.Threading.Tasks.TaskScheduler.MaximumConcurrencyLevel">
        /// MaximumConcurrencyLevel</see> as an <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        public uint MaximumConcurrency
        {
            get
            {
                uint mc = this.TaskFactory.MaximumConcurrency();
                if (maxConcurrency < 1 && mc < 1)
                {
                    maxConcurrency = MAXTHREADSDEFAULT;
                }
                else if (maxConcurrency < 1)
                {
                    maxConcurrency = mc;
                }
                return maxConcurrency;
            }
            set
            {
                uint mc = this.TaskFactory.MaximumConcurrency();
                if (mc < 1 || value <= mc)
                {
                    maxConcurrency = value;
                }
                else
                {
                    maxConcurrency = mc;
                }
            }
        }
        private uint maxConcurrency;

        /// <summary>
        /// The <see cref="TaskFactory">System.Threading.Tasks.TaskFactory</see> instance used for instantiating tasks.
        /// </summary>
        public TaskFactory TaskFactory { get; set; }

        /// <summary>
        /// The <see cref="System.Collections.Generic.List{T}">collection</see> of <see cref="AbortableTask">Tasks</see> currently loaded.
        /// </summary>
        public System.Collections.Generic.List<AbortableTask> Tasks { get; set; }

        /// <summary>
        /// The <see cref="System.Collections.Generic.List{T}">collection</see> of <see cref="AbortableTask">Tasks</see> with a Canceled status.
        /// </summary>
        public System.Collections.Generic.List<AbortableTask> CanceledTasks
        {
            get
            {
                return this.Tasks.Where(x => x.Status == TaskStatus.Canceled).ToList();
            }
        }

        /// <summary>
        /// The <see cref="System.Collections.Generic.List{T}">collection</see> of <see cref="AbortableTask">Tasks</see> with a Faulted status.
        /// </summary>
        public System.Collections.Generic.List<AbortableTask> FaultedTasks
        {
            get
            {
                return this.Tasks.Where(x => x.Status == TaskStatus.Faulted).ToList();
            }
        }

        /// <summary>
        /// The <see cref="System.Collections.Generic.List{T}">collection</see> of <see cref="AbortableTask">Tasks</see> with a Completed status.
        /// </summary>
        public System.Collections.Generic.List<AbortableTask> CompletedTasks
        {
            get
            {
                return this.Tasks.Where(x => x.Status == TaskStatus.RanToCompletion).ToList();
            }
        }

        /// <summary>
        /// The <see cref="System.Collections.Generic.List{T}">collection</see> of <see cref="AbortableTask">Tasks</see> with a Running status.
        /// </summary>
        public System.Collections.Generic.List<AbortableTask> RunningTasks
        {
            get
            {
                return this.Tasks.Where(x => x.Status == TaskStatus.Running || x.Status == TaskStatus.WaitingForChildrenToComplete).ToList();
            }
        }

        /// <summary>
        /// The <see cref="System.Collections.Generic.List{T}">List</see> of <see cref="AbortableTask">Tasks</see> awaiting execution.
        /// </summary>
        public System.Collections.Generic.List<AbortableTask> PendingTasks { get; set; }

        /// <summary>
        /// Returns <see cref="System.Boolean">true</see> if <see cref="TaskManager.RunningTasks">RunningTasks</see> has one or more items.
        /// </summary>
        public bool HasActiveTasks
        {
            get
            {
                return this.RunningTasks.Count() > 0;
            }
        }

        /// <summary>
        /// .ctor. Creates a new instance of TaskManager.
        /// </summary>
        public TaskManager()
        {
            this.Tasks = new System.Collections.Generic.List<AbortableTask>();
            this.PendingTasks = new System.Collections.Generic.List<AbortableTask>();
            this.TimeOutAll = TimeSpan.FromHours(1);
            this.TimeOut = TimeSpan.FromSeconds(10);
            this.TaskFactory = new TaskFactory();
            this.TaskFactory.StartNew(SpinAwait);
        }

        /// <summary>
        /// Starts the <see cref="System.Action">action</see> using <see cref="TaskManager.TaskFactory">TaskFactory</see> 
        /// and adds it to the <see cref="TaskManager.Tasks">Tasks</see> collection or the 
        /// <see cref="TaskManager.PendingTasks">PendingTasks</see> collection if no threads are available.
        /// </summary>
        /// <param name="action">The <see cref="System.Action">action</see> to be executed.</param>
        public void StartAction(Action action) 
        {
            StartTask(new AbortableTask(action));
        }
        /// <summary>
        /// Starts the <see cref="System.Action">action</see> using <see cref="TaskManager.TaskFactory">TaskFactory</see> 
        /// and adds it to the <see cref="TaskManager.Tasks">Tasks</see> collection or the 
        /// <see cref="TaskManager.PendingTasks">PendingTasks</see> collection if no threads are available
        /// using the provided <see cref="System.Threading.CancellationTokenSource">CancellationTokenSource</see>.
        /// </summary>
        /// <param name="action">The <see cref="System.Action">action</see> to be executed.</param>
        /// <param name="cancellationTokenSource">The <see cref="System.Threading.CancellationTokenSource">source</see> of the <see cref="System.Threading.CancellationToken">CancellationToken</see>.</param>
        public void StartAction(Action action, CancellationTokenSource cancellationTokenSource)
        {
            StartTask(new AbortableTask(action, cancellationTokenSource));
        }
        /// <summary>
        /// Starts the <see cref="AbortableTask">Task</see> using <see cref="TaskManager.TaskFactory">TaskFactory</see> 
        /// and adds it to the <see cref="TaskManager.Tasks">Tasks</see> collection or the 
        /// <see cref="TaskManager.PendingTasks">PendingTasks</see> collection if no threads are available.
        /// </summary>
        /// <param name="task"></param>
        public void StartTask(AbortableTask task)
        {
            if (this.MaximumConcurrency <= this.RunningTasks.Count())
            {
                this.PendingTasks.Add(task);
            }
            else
            {
                this.Tasks.Add(task);
                task.Start(this.TaskFactory.Scheduler);
            }
        }
        /// <summary>
        /// Starts the <see cref="System.Threading.Tasks.Task">Task</see> using <see cref="TaskManager.TaskFactory">TaskFactory</see> 
        /// and adds it to the <see cref="TaskManager.Tasks">Tasks</see> collection or the 
        /// <see cref="TaskManager.PendingTasks">PendingTasks</see> collection if no threads are available.
        /// </summary>
        /// <param name="task">The <see cref="System.Threading.Tasks.Task">Task</see> to be executed.</param>
        public void StartTask(Task task)
        {
            AbortableTask abt = task as AbortableTask;
            abt.CancellationTokenSource = new CancellationTokenSource();
            StartTask(abt);
        }
        /// <summary>
        /// Starts the <see cref="System.Threading.Tasks.Task">Task</see> using <see cref="TaskManager.TaskFactory">TaskFactory</see> 
        /// and adds it to the <see cref="TaskManager.Tasks">Tasks</see> collection or the 
        /// <see cref="TaskManager.PendingTasks">PendingTasks</see> collection if no threads are available
        /// using the provided <see cref="System.Threading.CancellationTokenSource">CancellationTokenSource</see>.
        /// </summary>
        /// <param name="task">The <see cref="System.Threading.Tasks.Task">Task</see> to be executed.</param>
        /// <param name="cancellationTokenSource">The <see cref="System.Threading.CancellationTokenSource">source</see> of the <see cref="System.Threading.CancellationToken">CancellationToken</see>.</param>
        public void StartTask(Task task, CancellationTokenSource cancellationTokenSource)
        {
            AbortableTask abt = task as AbortableTask;
            abt.CancellationTokenSource = cancellationTokenSource;
            StartTask(abt);
        }

        /// <summary>
        /// Removes all <see cref="TaskManager.Tasks">Tasks</see> that are no longer running.
        /// </summary>
        public void CleanTasks()
        {
            this.Tasks.RemoveAll(x => x.Status != TaskStatus.Running && x.Status != TaskStatus.WaitingForChildrenToComplete);
        }

        /// <summary>
        /// Awaits all tasks (or <see cref="TaskManager.TimeOutAll">TimeOutAll</see> elapses).
        /// </summary>
        public void AwaitAll()
        {
            System.DateTime dtStart = System.DateTime.Now;
            while (this.HasActiveTasks)
            {
                System.Threading.Thread.Sleep(10);
                if (System.DateTime.Now - dtStart >= this.TimeOutAll)
                {
                    throw new System.TimeoutException(
                        string.Format(Common.Properties.Resources.ResourceManager.GetString("ThreadNotAvailable"),
                        this.TimeOutAll.ToString("HH:mm:ss.fff")));
                }
            }
        }

        /// <summary>
        /// Awaits all tasks (or <see cref="TaskManager.TimeOutAll">TimeOutAll</see> elapses) and removes them from the 
        /// <see cref="TaskManager.Tasks">Tasks</see> collection.
        /// See <see cref="TaskManager.AwaitAll">AwaitAll</see> and <see cref="TaskManager.CleanTasks">CleanTasks</see>.
        /// </summary>
        public void AwaitAllThenClean()
        {
            AwaitAll();
            CleanTasks();
        }

        /// <summary>
        /// Safely <see cref="System.IDisposable">Disposes</see> of this instance and all child <see cref="AbortableTask">Tasks</see>.
        /// </summary>
        public void Dispose()
        {
            disposing = true;
            CleanTasks();
            foreach (AbortableTask t in this.Tasks)
            {
                t.Abort();
            }
        }

        private void SpinAwait()
        {
            while (!disposing)
            {
                try
                {
                    WaitForAvailableThread();
                    if (disposing) return;
                    if (this.PendingTasks.Count > 0)
                    {
                        AbortableTask action = this.PendingTasks[0];
                        try
                        {
                            action.Start(this.TaskFactory.Scheduler);
                            this.Tasks.Add(action);
                        }
                        catch (Exception ex1)
                        {
                            if (OnThreadException != null)
                            {
                                OnThreadException.Invoke(this, ex1, action);
                            }
                            else
                            {
                                throw;
                            }
                        }
                        this.PendingTasks.Remove(action);
                    }
                }
                catch (Exception ex0)
                {
                    if (OnThreadException != null)
                    {
                        OnThreadException.Invoke(this, ex0);
                    }
                    else
                    {
                        throw;
                    }
                }
                Thread.Sleep(20);
            }
        }

        private void WaitForAvailableThread()
        {
            System.DateTime dtStart = System.DateTime.Now;
            while (!disposing && this.MaximumConcurrency <= this.RunningTasks.Count())
            {
                System.Threading.Thread.Sleep(10);
                if (System.DateTime.Now - dtStart >= this.TimeOut)
                {
                    throw new System.TimeoutException(
                        string.Format(Common.Properties.Resources.ResourceManager.GetString("ThreadNotAvailable"), 
                        this.TimeOut.ToString("HH:mm:ss.fff")));
                }
            }
        }
    }
}
