using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace msix.catalog.app.Mvvm
{
    internal class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public DelegateCommand(Action<T> execute)
            : this(execute, (o) => true)
        {
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This cannot be an event")]
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        [DebuggerStepThrough]
        public bool CanExecute(T parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        public void Execute(T parameter)
        {
            if (CanExecute(parameter))
            {
                _execute(parameter);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
