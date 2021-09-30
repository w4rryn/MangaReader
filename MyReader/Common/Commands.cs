using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyReader.Common
{
    public class Command : ICommand
    {
        protected readonly Func<bool> canExecute;

        protected readonly Action<object> execute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public Command(Action<object> execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute;
        }

        public Command(Action<object> execute) : this(execute, null)
        {
        }

        public virtual bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public virtual void Execute(object parameter)
        {
            execute(parameter);
        }
    }

    public class CommandAsync : Command
    {
        private bool isExecuting = false;

        public event EventHandler Started;

        public event EventHandler Ended;

        public bool IsExecuting
        {
            get { return isExecuting; }
        }

        public CommandAsync(Action<object> execute, Func<bool> canExecute) : base(execute, canExecute)
        {
        }

        public CommandAsync(Action<object> execute) : base(execute)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return ((base.CanExecute(parameter)) && (!this.isExecuting));
        }

        public override void Execute(object parameter)
        {
            try
            {
                isExecuting = true;
                Started?.Invoke(this, EventArgs.Empty);

                Task task = Task.Factory.StartNew(() =>
                {
                    execute(parameter);
                });
                task.ContinueWith(t =>
                {
                    OnRunWorkerCompleted(EventArgs.Empty);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
            }
        }

        private void OnRunWorkerCompleted(EventArgs e)
        {
            isExecuting = false;
            Ended?.Invoke(this, e);
        }
    }
}