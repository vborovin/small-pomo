namespace Pomo.Commands
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    class ShowPomoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var window = parameter as MainWindow;
            return (window != null);
        }

        public void Execute(object parameter)
        {
            var window = parameter as MainWindow;

            if (!window.IsVisible)
            {
                window.Show();
            }

            window.ChangeState(WindowState.Normal);
            window.Activate();
        }
    }
}
