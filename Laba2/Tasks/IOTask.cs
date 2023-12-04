using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2.Tasks
{
    internal class IOTask : ICustomTask
    {
        private int _duration;

        protected Stopwatch stopwatch = new Stopwatch();

        private Guid _guid = Guid.NewGuid();

        public bool isCompleted => CurrentTime >= EndTime ? true : false;

        public int Duration { get => _duration; set => _duration = value; }

        public Guid guid { get => _guid; }

        private int CPUDuration { get; set; }

        private int EndTime { get; set; }

        private int CurrentTime { get; set; }

        public IOTask(int cpuDuration, int duration)
        {
            CPUDuration = cpuDuration;
            Duration = dur();
        }

        public int dur()
        {
            stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Output Operation");
            stopwatch.Stop();
            return (int)stopwatch.ElapsedTicks;
        }

        public int Execute(int currentTime, int timeQuantum)
        {
            CurrentTime = currentTime;
            if (CPUDuration > 0)
            {
                if (CPUDuration - timeQuantum < 0)
                {
                    var result = CPUDuration;
                    CPUDuration = 0;
                    EndTime = currentTime + Duration;
                    return result;
                }
                else
                {
                    CPUDuration -= timeQuantum;
                    return timeQuantum;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
