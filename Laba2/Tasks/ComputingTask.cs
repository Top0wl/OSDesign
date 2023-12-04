using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Laba2.Tasks
{
    internal class ComputingTask : ICustomTask
    {
        private Guid _guid = Guid.NewGuid();
        private int _duration;
        protected Stopwatch stopwatch = new Stopwatch();
        Random rnd = new Random();

        public int Duration
        {
            get => _duration;
            set => _duration = value;
        }

        private int dur()
        {
            double result = 0; // Используем volatile

            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++) // Произвольное количество итераций
            {
                result += Math.Sqrt(i); // Простая математическая операция
            }

            stopwatch.Stop();

            return (int)stopwatch.ElapsedTicks;
        }

        public bool isCompleted => Duration <= 0 ? true : false;

        public Guid guid => _guid;

        public ComputingTask(int duration)
        {
            Duration = dur();
        }

        public int Execute(int currentTime, int timeQuantum)
        {
            if (Duration - timeQuantum < 0)
            {
                var result = Duration;
                Duration = 0;
                return result;
            }
            else
            {
                Duration -= timeQuantum;
                return timeQuantum;
            }
        }
    }
}
