using System;
using System.Windows.Input;

namespace AnyListen.ViewModelBase
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
    /// </summary>
    /// <remarks></remarks>
    public class RelayCommand : ICommand
    {
        #region "Declarations"
        public delegate void ExecuteDelegate(object parameter);
        private readonly Func<bool> _canExecute;
        #endregion
        private readonly ExecuteDelegate _execute;

        #region "Constructors"
        public RelayCommand(ExecuteDelegate execute)
            : this(execute, null)
        {
        }

        public RelayCommand(ExecuteDelegate execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region "ICommand"
        public event EventHandler CanExecuteChanged
        {

            add
            {
                if (_canExecute != null)
                {
         