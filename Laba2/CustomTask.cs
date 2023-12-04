using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2.Tasks
{
    internal class CustomTask
    {
        public string Name { get; set; }
        public int CPUDuration = 50;
        public int Duration = 50;



        private int _executionTime;


        public int executionTime 
        {
            get => _executionTime;
            set
            {
                if (value >= CPUDuration)
                {
                    _executionTime = CPUDuration;
                }
                else
                {
                    _executionTime = value;
                }
            }
        }

        public Guid Guid = Guid.NewGuid();

        public CustomTask(string name, int duration)
        {
            Name = name;
            Duration = duration;    
        }
    }

    enum ClassTask
    {
        Computed,
        IO,
        Balanced
    }
}
