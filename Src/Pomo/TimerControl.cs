using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomo
{
    public class TimerControl
    {
        public bool IsRunning { get; set; } = false;
        public TimerState State { get; set; } = TimerState.Work;
        public TimeSpan WorkTime { get; set; } = Properties.Settings.Default.workTime;
        public TimeSpan BreakTime { get; set; } = Properties.Settings.Default.breakTime;
        public TimeSpan LongBreakTime { get; set; } = Properties.Settings.Default.longBreakTime;
        public int Iterations { get; set; } = Properties.Settings.Default.iterations;
        public int CurrentIteration { get; set; } = 1;
        public enum TimerState { Work, Break }
        public TimeSpan CurrentInterval { get; set; }
    }
}
