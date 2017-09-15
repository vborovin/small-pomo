namespace Pomo
{
    using System;
    using System.Windows;
    using System.Windows.Media.Imaging;

    using Hardcodet.Wpf.TaskbarNotification;

    using Pomo.Commands;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public TaskbarIcon taskbarIcon;
        BitmapImage appIcon;

        public App()
        {
            appIcon = new BitmapImage(new Uri("pack://application:,,,/Pomo;component/Images/icon.ico"));

            var mainWindow = new MainWindow(this);
            mainWindow.Show();

            taskbarIcon = new TaskbarIcon();
            taskbarIcon.ToolTipText = "Small Pomo";
            taskbarIcon.Visibility = Visibility.Collapsed;
            taskbarIcon.IconSource = appIcon;
            taskbarIcon.DoubleClickCommand = new ShowPomoCommand();
            taskbarIcon.DoubleClickCommandParameter = mainWindow;
        }
    }
}
