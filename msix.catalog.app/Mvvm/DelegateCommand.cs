using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace msix.catalog.app.Mvvm
{
    internal class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T,bool> _canExecute;
        
        public DelegateCommand(Action<T> execute)
            : this(execute, (o)=>true)
        {
        }

        public DelegateCommand(Action<T> execute, Func<T,bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This cannot be an event")]
        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(T parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        public void Execute(T parameter)
        {
            if (this.CanExecute(parameter))
            {
                this._execute(parameter);
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
