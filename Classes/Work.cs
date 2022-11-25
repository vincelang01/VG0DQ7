using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VG0DQ7.Classes
{
    public class Work
    {
        public String Name { get; private set; }
        public int materialCost { get; private set; }
        public int time;

        public int Hour { get { return time / 60; } }
        public int Minute { get { return time % 60; } }
        public int CostOfWork { get { return (Hour * 15000) + (Minute * 15000/60); } }

        public String getWorkTime()
        {
            return $"{(Hour > 0 ? Hour + " ó " : "")}" + $"{(Minute > 0 ? Minute + " p" : " 0 p")}";
        }

        public Work(String Name, int time, int materialCost)
        {
            this.Name = Name;
            this.time = time;
            this.materialCost = materialCost;
        }
    }
}
