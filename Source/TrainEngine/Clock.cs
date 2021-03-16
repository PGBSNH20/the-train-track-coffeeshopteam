using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace TrainEngine
{
    public class Clock
    {
        // Goal a clock that imitates real life, but is shorter, so 1 hour is 60 seconds
        // Static here because There should only be 1 time, so even if there is multiple clocks, the time is to the class, not to the object
        public static TimeSpan Time { get; set; }
        public static bool IsPrintingTime { get; set; }
        private bool isTicking;
        Thread timeThread;

        public Clock()
        {
            Time = new TimeSpan();
            timeThread = new Thread(PassTime);
            timeThread.Start();
            timeThread.IsBackground = true;
        }

        public void PassTime()
        {
            Start();
            while (true)
            {
                if (isTicking)
                {
                    PrintClock();
                    Time = Time.Add(TimeSpan.FromMinutes(1));
                }
                Thread.Sleep(10);
            }
        }

        private void PrintClock()
        {
            if (IsPrintingTime && Time.Minutes % 15 == 0)
            {
                string timeString = Time.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
                Console.WriteLine($"[{timeString}]");
            }
        }

        public void Start()
        {
            isTicking = true;
        }

        public void Stop() 
        {
            isTicking = false;
        }

        public TimeSpan GetTime() 
        {
            return Time;
        }
    }
}
