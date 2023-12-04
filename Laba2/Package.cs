using Laba2.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    internal class Package
    {
        public List<ICustomTask> _tasks = new List<ICustomTask>();
        public Package(int countTasks, double probabilityProcessor = 0.33, double probabilityOutput = 0.33)
        {
            Random random = new Random();

            for (int i = 0; i < countTasks; i++)
            {
                double randomValue = random.NextDouble();
                int choice = GetTaskType(probabilityOutput, probabilityProcessor, randomValue);

                // В зависимости от выбора добавляем соответствующую функцию в список _tasks
                switch (choice)
                {
                    case 0:
                        _tasks.Add(new ComputingTask(30));
                        break;
                    case 1:
                        _tasks.Add(new IOTask(10, 300));
                        break;
                }
            }
        }

        public Package(List<ICustomTask> tasks)
        {
            _tasks = tasks;
        }
        private int GetTaskType(double probabilityOutput, double probabilityProcessor, double randomValue)
        {
            double cumulativeProbability = 0;

            // Сравниваем случайное значение с накопленными вероятностями
            if (randomValue < cumulativeProbability + probabilityOutput)
            {
                return 0; // Тип задачи OutputOperation
            }
            cumulativeProbability += probabilityOutput;

            return 1; // Тип задачи ProcessorOperation
        }
        public int Start()
        { 
            RoundRobin roundRobin = new RoundRobin();
            roundRobin._queue = new Queue<ICustomTask>(_tasks);
            return roundRobin.Start();
        }
    }
}
