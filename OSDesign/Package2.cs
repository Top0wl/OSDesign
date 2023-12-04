using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1
{
    internal class Package2 : Package
    {
        public Package2(int countTasks, double probabilityProcessor = 0.33, double probabilityOutput = 0.33, double probabilityBalance = 0.33) : base(countTasks, probabilityProcessor, probabilityOutput, probabilityBalance)
        {
        }

        //Буду использовать output функцию т.к. чтение с потока будет слишком тяжёлой операцией
        protected override long OutputOperation()
        {
            stopwatch = Stopwatch.StartNew();
            Thread.Sleep(10);
            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }

        //Чисто вычислительная функция
        protected override long ProcessorOperation()
        {
            stopwatch = Stopwatch.StartNew();
            Thread.Sleep(5);
            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }

        //Сбалансированная (1 операция вывода, 1 вычислителньная операция)
        protected override long BalanceOperation()
        {
            stopwatch = Stopwatch.StartNew();
            Thread.Sleep(15);
            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }
    }
}
