using Laba1;
using System.Diagnostics;

namespace OSDesign
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sessionCount = 100000; // Количество сеансов
            int tasksPerSession = 100; // Количество задач в каждом сеансе
            List<SessionData> sessionDataList = new List<SessionData>();

            Console.SetBufferSize(228, 100);

            for (int session = 0; session <= sessionCount; session++)
            {
                Package2 package = new Package2(tasksPerSession);

                Stopwatch sessionStopwatch = Stopwatch.StartNew();

                package.Start();

                sessionStopwatch.Stop();
                long sessionTicks = sessionStopwatch.ElapsedTicks;

                // Собираем статистику для текущего сеанса
                long totalTicks = 0;
                foreach (var task in package._tasks)
                {
                    totalTicks += task.Item2;
                }

                foreach (var item in package._tasks.OrderBy(i => i.Item2))
                {
                    Console.WriteLine($"Operation: {item.Item1.Method}, ticks: {item.Item2.ToString()}");
                }

                double countOperationsBalanced = (double)package._tasks.Where(i => i.Item1.Method.Name == "BalanceOperation").Count() / package._tasks.Count;
                double countOperationsOutput = (double)package._tasks.Where(i => i.Item1.Method.Name == "OutputOperation").Count() / package._tasks.Count;
                double countOperationsProcessor = (double)package._tasks.Where(i => i.Item1.Method.Name == "ProcessorOperation").Count() / package._tasks.Count;


                double averageTicks = (double)totalTicks / tasksPerSession;
                double systemPerformance = (double)tasksPerSession / sessionTicks * Stopwatch.Frequency;
                double turnaroundTime = (double)sessionTicks / Stopwatch.Frequency;
                double idleTime = turnaroundTime - (double)totalTicks / Stopwatch.Frequency;

                Console.WriteLine($"Кол-во вычислительных операций: {countOperationsProcessor}");
                Console.WriteLine($"Кол-во Output операций: {countOperationsOutput}");
                Console.WriteLine($"Кол-во Сбалансированных операций: {countOperationsBalanced}");

                Console.WriteLine($"Среднее время выполнения задач в текущем сеансе: {averageTicks} тиков");
                Console.WriteLine($"Производительность системы: {systemPerformance} задач/сек");
                Console.WriteLine($"Оборотное время системы: {turnaroundTime} сек");
                Console.WriteLine($"Простои процессора: {idleTime} сек");


                SessionData sessionData = new SessionData()
                {
                    FractionOperationsBalanced = countOperationsBalanced,
                    FractionOperationsOutput = countOperationsOutput,
                    FractionOperationsProcessor = countOperationsProcessor,
                    AverageTicks = averageTicks,
                    SystemPerformance = systemPerformance,
                    TurnaroundTime = turnaroundTime,
                    IdleTime = idleTime
                };
                sessionDataList.Add(sessionData);
            }


            var avrgData = new List<SessionData>();
            var q = sessionDataList.GroupBy(i => i.FractionOperationsProcessor).ToList();
            foreach (var i in q)
            {
                SessionData sessionData = new SessionData()
                {
                    FractionOperationsProcessor = i.Key,
                    AverageTicks = i.Average(a => a.AverageTicks),
                    SystemPerformance = i.Average(a => a.SystemPerformance),
                    TurnaroundTime = i.Average(a => a.TurnaroundTime),
                    IdleTime = i.Average(a => a.IdleTime)
                };
                avrgData.Add(sessionData);
            }
            
            SaveDataToCsv(avrgData, "session_data.csv");
        }
        static void SaveDataToCsv(List<SessionData> data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Записываем заголовок CSV файла с точкой с запятой в качестве разделителя
                writer.WriteLine("FractionOperationsBalanced,FractionOperationsOutput,FractionOperationsProcessor,AverageTicks,SystemPerformance,TurnaroundTime,IdleTime");

                // Записываем данные из списка в CSV файл, используя точку с запятой как разделитель
                foreach (var sessionData in data)
                {
                    writer.WriteLine($"{sessionData.FractionOperationsBalanced.ToString().Replace(",", ".")}" +
                        $",{sessionData.FractionOperationsOutput.ToString().Replace(",", ".")}" +
                        $",{sessionData.FractionOperationsProcessor.ToString().Replace(",", ".")}" +
                        $",{sessionData.AverageTicks.ToString().Replace(",", ".")}" +
                        $",{sessionData.SystemPerformance.ToString().Replace(",", ".")}" +
                        $",{sessionData.TurnaroundTime.ToString().Replace(",", ".")}" +
                        $",{sessionData.IdleTime.ToString().Replace(",", ".")}");
                }
            }
        }

    }
    class SessionData
    {
        public double FractionOperationsBalanced { get; set; }
        public double FractionOperationsOutput { get; set; }
        public double FractionOperationsProcessor { get; set; }
        public double AverageTicks { get; set; }
        public double SystemPerformance { get; set; }
        public double TurnaroundTime { get; set; }
        public double IdleTime { get; set; }
    }
}