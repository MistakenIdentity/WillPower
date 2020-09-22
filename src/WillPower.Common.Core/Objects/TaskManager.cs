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
using System.Threading.Tasks;

namespace WillPower
{
    /// <summary>
    /// A class for managing active tasks without any complex magic.
    /// Why isn't something like this part of .Net?
    /// Can be inherited because I was too lazy to make an Interface.
    /// </summary>
    public class TaskManager : System.IDisposable
    {
        const uint MAXTHREADSDEFAULT = 256;

        /// <summary>
        /// The <see cref="System.TimeSpan">System.TimeSpan</see> to wait prior to throwing a <see cref="System.TimeoutException">TimeoutException</see>.
        /// </summary>
        public System.TimeSpan TimeOut { get; set; }

        /// <summary>
        /// The default <see cref="System.Threading.Tasks.TaskScheduler">TaskScheduler</see> of the <see cref="System.Threading.Tasks.TaskFactory">TaskFactory's</see> <see cref="System.Threading.Tasks.TaskScheduler.MaximumConcurrencyLevel">MaximumConcurrencyLevel</see> as an <see cref="System.UInt32">unsigned integer</see>.
        /// </summary>
        public uint MaximumConcurrency
        {
            get
            {
                uint mc = this.TaskFactory.MaximumConcurrency();
                if (maxConcurrency == 0 && mc < 1)
                {
                    maxConcurrency = MAXTHREADSDEFAULT;
                }
                else if (maxConcurrency == 0)
                {
                    maxConcurrency = mc;
                }
                return maxConcurrency;
            }
            set
            {
                uint mc = this.TaskFactory.MaximumConcurrency();
                if (mc < 1)
                {
                    maxConcurrency = value;
                }
                else if (value > mc)
                {
                    maxConcurrency = mc;
                }
                else
                {
                    maxConcurrency = value;
                }
            }
        }
        private uint maxConcurrency;

        /// <summary>
        /// The <see cref="TaskFactory">System.Threading.Tasks.TaskFactory</see> instance used for instantiating tasks.
        /// </summary>
        public TaskFactory TaskFactory { get; set; }

        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="Tasks">System.Threading.Tasks.Task</see> currently loaded.
        /// </summary>
        public System.Collections.Generic.IEnumerable<Task> Tasks 
        {
            get
            {
                return tasks;
            }
            set
            {
                tasks = value.ToList();
            }
        }
        private System.Collections.Generic.List<Task> tasks;

        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="Tasks">System.Threading.Tasks.Task</see> with a Canceled status.
        /// </summary>
        public System.Collections.Generic.IEnumerable<Task> CanceledTasks
        {
            get
            {
                return tasks.Where(x => x.IsCanceled);
            }
        }

        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="Tasks">System.Threading.Tasks.Task</see> with a Faulted status.
        /// </summary>
        public System.Collections.Generic.IEnumerable<Task> FaultedTasks
        {
            get
            {
                return tasks.Where(x => x.IsFaulted);
            }
        }

        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="Tasks">System.Threading.Tasks.Task</see> with a Completed status.
        /// </summary>
        public System.Collections.Generic.IEnumerable<Task> CompletedTasks
        {
            get
            {
                return tasks.Where(x => x.IsCompleted);
            }
        }

        /// <summary>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}">collection</see> of <see cref="Tasks">System.Threading.Tasks.Task</see> with a Running status.
        /// </summary>
        public System.Collections.Generic.IEnumerable<Task> RunningTasks
        {
            get
            {
                return tasks.Where(x => x.Status == TaskStatus.Running);
            }
        }

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
            this.TimeOut = System.TimeSpan.FromSeconds(10);
            this.TaskFactory = new TaskFactory();
            tasks = new System.Collections.Generic.List<Task>();
        }

        /// <summary>
        /// Starts the <see cref="System.Action">action</see> using <see cref="TaskManager.TaskFactory">TaskFactory</see> 
        /// and adds it to the <see cref="TaskManager.Tasks">Tasks</see> collection.
        /// </summary>
        /// <param name="action">The <see cref="System.Action">action</see> to execute.</param>
        public void StartAction(System.Action action)
        {
            WaitForAvailableThread();
            tasks.Add(this.TaskFactory.StartNew(action));
        }

        /// <summary>
        /// Removes all <see cref="TaskManager.Tasks">Tasks</see> that are no longer running.
        /// </summary>
        public void CleanTasks()
        {
            tasks.RemoveAll(x => x.Status != TaskStatus.Running);
        }

        /// <summary>
        /// Safely <see cref="System.IDisposable">Dispose</see> of this instance and all child <see cref="TaskManager.Tasks">Tasks</see>.
        /// </summary>
        public void Dispose()
        {
            CleanTasks();
            foreach (Task t in tasks)
            {
                t.Dispose();
            }
        }

        private void WaitForAvailableThread()
        {
            System.DateTime dtStart = DateTime.Now;
            while (this.MaximumConcurrency <= this.RunningTasks.Count())
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
