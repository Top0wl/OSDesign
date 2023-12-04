using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1
{
    internal class RoundRobin
    {
        public Queue<Task> taskQueue = new Queue<Task>();
        private int timeQuantum = 100; // Пример: Квант времени в миллисекундах

        public void Execute()
        {
            while (taskQueue.Count > 0) 
            {
                var currentTask = taskQueue.Dequeue();
                currentTask.Start();
            }
        }
    }


}
