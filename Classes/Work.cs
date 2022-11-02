using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VG0DQ7.Classes
{
    public class Work
    {
        public string Name { get; }
        private int time;
        private int costOfWork;

        public int CostOfWork { get { return costOfWork; } }
        public int Hour { get { return time / 60; } }
        public int Minute { get { return time % 60; } }
        public int CostOfService { get { return time * 250; } }

        //Time of the work, string for display purpose
        public string WorkTime
        {
            get
            {
                return $"{(Hour > 0 ? Hour + " ó" : "")}" + $"{(Minute > 0 ? Minute + " p" : " 0 p")}";
            }
        }

        public Work(string name, int time, int costOfWork)
        {
            Name = name;
            this.time = time;
            this.costOfWork = costOfWork;
        }
    }
}
