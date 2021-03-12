using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TrainEngine
{
    class clock : iclock
    {
        private Thread clockthread;
        public static int minutessincestart;
        public static bool clockIsrunning;
        public fakeclock()
        {
            clockthread = new Thread();
            clockthread.IsBackground = true;
            minutessincestart = 0;
        }
        public void Start()
        {
            clockIsrunning = true;
        }
        public void Stop()
        {
            clockIsrunning = false;
        }
        public static void Tick ()
        {
            while(true)
            {
                Thread.Sleep(200);
                if (clockIsrunning)
                {
                    minutessincestart++;
                    Console.WriteLine($"Tick, {minutessincestart} has passed");
                }
            }
        }
    }
}
