using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    internal interface ICustomTask
    {
        public Guid guid { get; }
        public bool isCompleted { get; }
        public int Duration { get; set; }
        public int Execute(int currentTime, int timeQuantum);
    }
}
