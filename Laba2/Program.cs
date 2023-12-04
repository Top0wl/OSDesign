using Laba2.Tasks;
using System;

namespace Laba2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<SessionData> sessionDataList = new List<SessionData>();
            int sessionCount = 1000; // Количество сеансов
            int tasksPerSession = 1000; // Количество задач в каждом сеансе

            Console.SetBufferSize(228, 100);

            for (int session = 0; session <= sessionCount; session++)
            {
                float r1 = (float)rnd.NextDouble();
                float r2 = 1 - r1;

                Package package = new Package(tasksPerSession, r1, r2);
                int TotalTime = package.Start();

                Console.WriteLine($"Кол-во задач: {package._tasks.Count()}");
                Console.WriteLine($"Кол-во вычислительных задач: {package._tasks.Where(i => i is ComputingTask).Count()}");
                Console.WriteLine($"Кол-во IO задач: {package._tasks.Where(i => i is IOTask).Count()}");

                Console.WriteLine($"Среднее время выполнения задач в текущем сеансе: {(float)TotalTime / package._tasks.Count()} тиков");


                SessionData sessionData = new SessionData()
                {
                    FractionOperationsOutput = package._tasks.Where(i => i is IOTask).Count(),
                    FractionOperationsProcessor = package._tasks.Where(i => i is ComputingTask).Count(),
                    AverageTicks = (float)TotalTime / package._tasks.Count(),
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
                    FractionOperationsOutput = i.First().FractionOperationsOutput,
                    AverageTicks = i.Average(a => a.AverageTicks),
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
                writer.WriteLine("FractionOperationsOutput,FractionOperationsProcessor,AverageTicks");

                // Записываем данные из списка в CSV файл, используя точку с запятой как разделитель
                foreach (var sessionData in data)
                {
                    writer.WriteLine(
                        $"{(sessionData.FractionOperationsOutput / (sessionData.FractionOperationsProcessor + sessionData.FractionOperationsOutput)).ToString().Replace(",", ".")}" +
                        $",{(sessionData.FractionOperationsProcessor / (sessionData.FractionOperationsProcessor + sessionData.FractionOperationsOutput)).ToString().Replace(",", ".")}" +
                        $",{sessionData.AverageTicks.ToString().Replace(",", ".")}");
                }
            }
        }
    }
}