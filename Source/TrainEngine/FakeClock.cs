using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace TrainEngine
{
    public class FakeClock : IFakeClock
    {
        private Thread clockTread;
        public static TimeSpan FakeTime;
        private static bool clockIsRunnig;

        public FakeClock(TimeSpan startTime)
        {
            clockTread = new Thread(Tick);
            clockTread.IsBackground = true;
            FakeTime = startTime;
            clockIsRunnig = false;
            clockTread.Start();
        }

        public void Start()
        {
            clockIsRunnig = true;
        }

        public static void Stop()
        {
            clockIsRunnig = false;
        }

        private static void Tick()
        {
            while (true)
            {
                Thread.Sleep(200);
                if (clockIsRunnig)
                {
                    FakeTime = FakeTime.Add(TimeSpan.FromMinutes(1));

                    Console.WriteLine(FakeTime.ToString(@"hh\:mm", CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
