using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VG0DQ7.Classes
{
    internal class Services
    {
        private List<Work> works;

        public int TotalWorkTimeCost
        {
            get
            {
                var minute = (from item in works select item.Minute + item.Hour * 60).Sum();
                int totalHalfHour = (minute % 30 == 0 ? minute / 30 : minute / 30 + 1);
                return totalHalfHour * 7500;
            }
        }

        public int TotalCostOfService
        {
            get { return (from item in works select item.CostOfWork).Sum(); }
        }

        public int TotalTimeOfWork
        {
            get
            {
                var totalMinute = (from item in works select item.Minute + item.Hour * 60).Sum();
                return totalMinute;
            }
        }

        public int TotalNumberOfWork
        {
            get { return works.Count; }
        }

        public Services()
        {
            works = new List<Work>();
        }

        public void AddItem(Work work)
        {
            works.Add(work);
            works = works.Distinct().ToList();
        }

        public void RemoveItem(Work work)
        {
            works.Remove(work);
            works = works.Distinct().ToList();
        }
    }
}
