using System.Threading.Tasks;
using System.Threading;

namespace WillPower
{
    /// <summary>
    /// A simple container for handling <see cref="System.Threading.Tasks.Task">Task</see> cancellation.
    /// Inherits <see cref="System.Threading.Tasks.Task">System.Threading.Tasks.Task</see>.
    /// </summary>
    public class AbortableTask : System.IDisposable
    {
        /// <summary>
        /// The root <see cref="System.Threading.Tasks.Task">Task</see>.
        /// </summary>
        internal Task Task { get; private set; }
        /// <summary>
        /// The <see cref="System.Threading.CancellationTokenSource">source</see> of the 
        /// <see cref="System.Threading.CancellationToken">CancellationToken</see>.
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; set; }
        /// <summary>
        /// The <see cref="System.Threading.CancellationToken">CancellationToken</see>.
        /// </summary>
        public CancellationToken CancellationToken => CancellationTokenSource.Token;

        /// <summary>
        /// Gets the System.Threading.Tasks.TaskStatus of this task.
        /// </summary>
        public TaskStatus Status => Task?.Status ?? TaskStatus.Created;

        /// <summary>
        /// .ctor. Creates a new Instance of AbortableTask.
        /// </summary>
        /// <param name="action">The <see cref="System.Action">action</see> to be executed.</param>
        /// <param name="tokenSource">The <see cref="System.Threading.CancellationTokenSource">source</see> 
        /// of the <see cref="System.Threading.CancellationToken">CancellationToken</see>.</param>
        public AbortableTask(System.Action action, CancellationTokenSource tokenSource) : this(new Task(action), tokenSource)
        { }
        /// <summary>
        /// .ctor. Creates a new Instance of AbortableTask.
        /// </summary>
        /// <param name="action">The <see cref="System.Action">action</see> to be executed.</param>
        public AbortableTask(System.Action action) : this(new Task(action), new CancellationTokenSource())
        { }
        /// <summary>
        /// .ctor. Creates a new Instance of AbortableTask.
        /// </summary>
        /// <param name="task">The <see cref="System.Threading.Tasks.Task">task</see> to be executed.</param>
        /// <param name="tokenSource">The <see cref="System.Threading.CancellationTokenSource">source</see> 
        /// of the <see cref="System.Threading.CancellationToken">CancellationToken</see>.</param>
        public AbortableTask(System.Threading.Tasks.Task task, CancellationTokenSource tokenSource)
        {
            Task = task;
            CancellationTokenSource = tokenSource;
        }
        /// <summary>
        /// .ctor. Creates a new Instance of AbortableTask.
        /// </summary>
        /// <param name="task">The <see cref="System.Threading.Tasks.Task">task</see> to be executed.</param>
        public AbortableTask(System.Threading.Tasks.Task task) : this(task, new CancellationTokenSource())
        { }

        /// <summary>
        /// Invokes Cancel on the <see cref="System.Threading.CancellationTokenSource">CancellationTokenSource</see>.
        /// </summary>
        public void Abort()
        {
            CancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Disposes of this instance.
        /// </summary>
        public void Dispose()
        {
            Abort();
        }
    }
}
