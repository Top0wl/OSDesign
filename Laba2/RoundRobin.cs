using Laba2.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    internal class RoundRobin
    {
        public Queue<ICustomTask> _queue = new Queue<ICustomTask>();
        private int timeQuantum = 100; // Пример: Квант времени в миллисекундах

        public int totaltime = 0;

        public int Start() 
        {
            while (_queue.Count > 0)
            {
                var currentTask = _queue.Dequeue();
                var time = currentTask.Execute(totaltime, timeQuantum);
                totaltime += time;

                if (currentTask.isCompleted)
                {
                    //Console.WriteLine($"Процесс: {currentTask.guid}. Закончен");
                }
                else
                {
                    //if(time > 0) Console.WriteLine($"Процесс: {currentTask.guid}. Выполняется");
                    _queue.Enqueue(currentTask);
                }

                if(time == 0 && (_queue.All(i => i is IOTask)))
                {
                    totaltime += timeQuantum;
                }
            }
            return totaltime;
        }
    }
}
