using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VG0DQ7.Classes
{
    internal class Parser
    {
        public static Work Parse(string[] columns)
        {
            return new Work(columns[0], int.Parse(columns[1]), int.Parse(columns[2]));
        }
    }
}
