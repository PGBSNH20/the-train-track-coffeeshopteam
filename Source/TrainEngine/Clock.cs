﻿using System;
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
        private bool isTicking;
        Thread timeThread;

        public Clock()
        {
            Time = new TimeSpan();
            timeThread = new Thread(PassTime);
            timeThread.Start();
            timeThread.IsBackground = true;
        }

        // Method, PassTime()
        public void PassTime()
        {
            // loop, so it repeats itself and continues increasing the Time over time
            // actually increase the Time by for example 1 Minute every loop
            //Start();
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
            if (Time.Minutes % 15 == 0)
            {
                string timeString = Time.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
                Console.WriteLine($"[{timeString}]");
            }
        }

        // method Start()
        public void Start()
        {
            isTicking = true;
        }

        // method stop()
        public void Stop()
        {
            isTicking = false;
        }

        // Method ShowTime()
        public void ShowTime()
        {
            Console.WriteLine($"Current time is {Time.ToString(@"hh\:mm", CultureInfo.InvariantCulture)}"); 
        }

        // Method GetTime() : to give the time to other classes | or Property
        // Don't need this be
        public TimeSpan GetTime()
        {
            return Time;
        }
    }
}
