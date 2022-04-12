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
   