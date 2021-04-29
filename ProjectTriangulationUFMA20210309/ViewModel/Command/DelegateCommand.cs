using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectTriangulationUFMA20210309.ViewModel.Command {

    class DelegateCommand : ICommand {

        #region Fields, Properties and Variables
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;
        #endregion

        #region Constructors
        // When no parameter is available
        public DelegateCommand(Action<object> execute) : this(execute, DefaultCanExecute) {
            // The complete constructors will be used with DefaultCanExecute as canExecute
        }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute) {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }
        #endregion

        #region Command Logic
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            return canExecute(parameter);
        }

        public void Execute(object parameter) {
            execute(parameter);
        }

        // When the command is always available
        private static bool DefaultCanExecute(object parameter) {
            return true;
        }
        #endregion

    }
}
