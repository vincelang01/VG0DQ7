using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VG0DQ7.Classes
{
    internal class StaticData
    {
        private static StaticData instance;
        public List<Work> works { get; private set; }
        public List<Services> Services { get; private set; }

        private StaticData()
        {
            Services = new List<Services>();
        }

        public static StaticData Instance
        {
            get 
            { 
                if (instance == null) { instance = new StaticData(); }
                return instance;   
            }
        }

        public void SetData(List<Work> works)
        {
            this.works = works;
            Services = new List<Services>();
        }

        public void AddServiceOrder(Services services)
        {
            Services.Add(services);
        }
    }
}
