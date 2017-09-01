using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace Pomo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        TimerControl timerControl;
        BitmapImage workImage = new BitmapImage(new Uri("pack://application:,,,/Pomo;component/Images/tomato_512.png"));
        BitmapImage breakImage = new BitmapImage(new Uri("pack://application:,,,/Pomo;component/Images/tomato_512_inv.png"));

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new PrimaryScreenPositionProvider(
                corner: Corner.BottomRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public MainWindow()
        {
            InitializeComponent();

            timerControl = new TimerControl();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            ReloadTimer();
        }


        public void ReloadTimer()
        {
            StopTimer();
            timerControl.CurrentInterval = timerControl.WorkTime;
            Time.Content = String.Format("{0:00}:{1:00}", timerControl.CurrentInterval.Minutes, timerControl.CurrentInterval.Seconds);
            Tomato.Source = workImage;
        }

        private void StartTimer()
        {
            timerControl.IsRunning = true;
            timer.Start();
        }

        private void StopTimer()
        {
            timerControl.IsRunning = false;
            timer.Stop();
        }

        private void NewIteration()
        {
            string notificationMessage = "";
            StopTimer();
            switch (timerControl.State)
            {
                case TimerControl.TimerState.Work:
                    if (timerControl.CurrentIteration == timerControl.Iterations)
                    {
                        timerControl.CurrentInterval = timerControl.LongBreakTime;
                        timerControl.CurrentIteration = 0;
                        notificationMessage = "Long break time!";
                    }
                    else
                    {
                        timerControl.CurrentInterval = timerControl.BreakTime;
                        notificationMessage = "Break time!";
                    }
                    timerControl.State = TimerControl.TimerState.Break;
                    Tomato.Source = breakImage;
                    timerControl.CurrentIteration++;
                    break;
                case TimerControl.TimerState.Break:
                    Tomato.Source = workImage;
                    timerControl.State = TimerControl.TimerState.Work;
                    timerControl.CurrentInterval = timerControl.WorkTime;
                    notificationMessage = "Work time!";
                    break;
            }
            notifier.ShowSuccess(notificationMessage);
            System.Media.SystemSounds.Exclamation.Play();
            StartTimer();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timerControl.CurrentInterval.TotalMilliseconds > 0)
            {
                timerControl.CurrentInterval -= timer.Interval;
            }
            else
            {
                NewIteration();
            }
            var time = String.Format("{0:00}:{1:00}", timerControl.CurrentInterval.Minutes, timerControl.CurrentInterval.Seconds);
            Time.Content = time;
        }

        private void Time_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (timerControl.IsRunning)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings(timerControl, this);
            settings.Show();
            Settings.IsEnabled = false;
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            ReloadTimer();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            notifier.Dispose();
        }
    }
}
