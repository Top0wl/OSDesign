using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1
{
    internal class Package
    {
        public List<(Func<long>, long)> _tasks =new List<(Func<long>, long)>();

        private Stopwatch stopwatch = new Stopwatch();

        public Package(int countTasks, double probabilityProcessor = 0.33, double probabilityOutput = 0.33, double probabilityBalance = 0.33) 
        {
            Random random = new Random();

            for (int i = 0; i < countTasks; i++)
            {
                double randomValue = random.NextDouble();
                int choice = GetTaskType(probabilityOutput, probabilityProcessor, probabilityBalance, randomValue);

                // В зависимости от выбора добавляем соответствующую функцию в список _tasks
                switch (choice)
                {
                    case 0:
                        _tasks.Add((OutputOperation, 0));
                        break;
                    case 1:
                        _tasks.Add((ProcessorOperation, 0));
                        break;
                    case 2:
                        _tasks.Add((BalanceOperation, 0));
                        break;
                }
            }
        }


        private int GetTaskType(double probabilityOutput, double probabilityProcessor, double probabilityBalance, double randomValue)
        {
            double cumulativeProbability = 0;

            // Сравниваем случайное значение с накопленными вероятностями
            if (randomValue < cumulativeProbability + probabilityOutput)
            {
                return 0; // Тип задачи OutputOperation
            }
            cumulativeProbability += probabilityOutput;

            if (randomValue < cumulativeProbability + probabilityProcessor)
            {
                return 1; // Тип задачи ProcessorOperation
            }
            cumulativeProbability += probabilityProcessor;

            // Если не попали в предыдущие категории, то это тип BalanceOperation
            return 2;
        }

        //Буду использовать output функцию т.к. чтение с потока будет слишком тяжёлой операцией
        private long OutputOperation()
        {
            stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Output Operation");
            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }

        //Чисто вычислительная функция
        private long ProcessorOperation()
        {
            stopwatch = Stopwatch.StartNew();
            double result = (10 * 2 + 5) / 3.0 - 4 + (10 * 2 + 5) / 3.0 - 4 - (10 * 2 + 5) / 3.0 - 4;
            result = result - (10 * 2 + 5) / 3.0;
            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }

        //Сбалансированная (1 операция вывода, 1 вычислителньная операция)
        private long BalanceOperation()
        {
            stopwatch = Stopwatch.StartNew();
            double result = (10 * 2 + 5) / 3.0 - 4 + (10 * 2 + 5) / 3.0 - 4 - (10 * 2 + 5) / 3.0 - 4;
            result = result - (10 * 2 + 5) / 3.0;
            Console.WriteLine("Balance Operation");
            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }

        public void Start()
        {
            List<(Func<long>, long)> taskList = new List<(Func<long>, long)>();

            foreach (var item in _tasks)
            {
                var time = item.Item1.Invoke();

                taskList.Add((item.Item1, time));
            }
            _tasks = taskList;
        }

    }

}
