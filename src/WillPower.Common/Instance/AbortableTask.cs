using System.Threading.Tasks;
using System.Threading;

namespace WillPower
{
    /// <summary>
    /// A simple container for handling <see cref="System.Threading.Tasks.Task">Task</see> cancellation.
    /// Inherits <see cref="System.Threading.Tasks.Task">System.Threading.Tasks.Task</see>.
    /// </summary>
    public class AbortableTask : Task, System.IDisposable
    {
        /// <summary>
        /// The root <see cref="System.Threading.Tasks.Task">Task</see>.
        /// </summary>
        public Task Task { get; set; }
        /// <summary>
        /// The <see cref="System.Threading.CancellationTokenSource">source</see> of the <see cref="System.Threading.CancellationToken">CancellationToken</see>.
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; set; }
        /// <summary>
        /// The <see cref="System.Threading.CancellationToken">CancellationToken</see>.
        /// </summary>
        public CancellationToken CancellationToken
        {
            get
            {
                return this.CancellationTokenSource.Token;
            }
        }

        /// <summary>
        /// .ctor. Creates a new Instance of AbortableTask.
        /// </summary>
        /// <param name="action">The <see cref="System.Action">action</see> to be executed.</param>
        /// <param name="tokenSource">The <see cref="System.Threading.CancellationTokenSource">source</see> 
        /// of the <see cref="System.Threading.CancellationToken">CancellationToken</see>.</param>
        public AbortableTask(System.Action action, CancellationTokenSource tokenSource) : base(action, tokenSource.Token)
        {
            this.CancellationTokenSource = tokenSource;
        }
        /// <summary>
        /// .ctor. Creates a new Instance of AbortableTask.
        /// </summary>
        /// <param name="action">The <see cref="System.Action">action</see> to be executed.</param>
        public AbortableTask(System.Action action) : this(action, new CancellationTokenSource())
        { }

        /// <summary>
        /// Invokes Cancel on the <see cref="System.Threading.CancellationTokenSource">CancellationTokenSource</see>.
        /// </summary>
        public void Abort()
        {
            this.CancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Disposes of this instance.
        /// </summary>
        public new void Dispose()
        {
            Abort();
            base.Dispose();
        }
    }
}
