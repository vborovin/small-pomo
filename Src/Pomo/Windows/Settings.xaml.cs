using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pomo
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        TimerControl timerControl;
        MainWindow mainWindow;

        public Settings(TimerControl timerControl, MainWindow mainWindow)
        {
            this.timerControl = timerControl;
            this.mainWindow = mainWindow;

            InitializeComponent();

            WorkSlider.Value = timerControl.WorkTime.Minutes;
            BreakSlider.Value = timerControl.BreakTime.Minutes;
            LongBreakSlider.Value = timerControl.LongBreakTime.Minutes;
            IterationsSlider.Value = timerControl.Iterations;
        }

        private void WorkSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var value = (sender as Slider).Value;
            WorkTime.Content = $"{value} minutes";
        }

        private void BreakSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var value = (sender as Slider).Value;
            BreakTime.Content = $"{value} minutes";
        }

        private void LongBreakSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var value = (sender as Slider).Value;
            LongBreakTime.Content = $"{value} minutes";
        }

        private void IterationsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var value = (sender as Slider).Value;
            Iterations.Content = $"{value} iterations";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var workTime = WorkSlider.Value;
            timerControl.WorkTime = Properties.Settings.Default.workTime = TimeSpan.FromMinutes(workTime);

            var breakTime = BreakSlider.Value;
            timerControl.BreakTime = Properties.Settings.Default.breakTime = TimeSpan.FromMinutes(breakTime);

            var longBreakTime = LongBreakSlider.Value;
            timerControl.LongBreakTime = Properties.Settings.Default.longBreakTime = TimeSpan.FromMinutes(longBreakTime);

            var iterations = (int)IterationsSlider.Value;
            timerControl.Iterations = Properties.Settings.Default.iterations = iterations;

            mainWindow.ReloadTimer();

            Properties.Settings.Default.Save();

            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            mainWindow.Settings.IsEnabled = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {            
            Close();
        }
    }
}
