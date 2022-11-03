using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VG0DQ7.Classes;

namespace VG0DQ7
{
    public partial class PayForm : Form
    {
        StaticData data = StaticData.Instance;
        public PayForm()
        {
            InitializeComponent();
            int TotalNumberOfWorks = 0;
            int TotalWorkCost = 0;
            int TotalCostOfService = 0;
            int TotalWorkTime = 0;

            foreach(Services services in data.Services)
            {
                TotalNumberOfWorks += services.TotalNumberOfWork;
                TotalWorkCost += services.TotalWorkTimeCost;
                TotalCostOfService += services.TotalCostOfService;
                TotalWorkTime += services.TotalTimeOfWork;
            }
            int TotalCost = TotalWorkCost + TotalCostOfService;

            txt_totalNumberOfWorkSheet.Text = data.Services.Count.ToString() + " db";
            txt_NumberOfWorks.Text = TotalNumberOfWorks.ToString() + " db";
            txt_TotalCostOfService.Text = TotalCostOfService.ToString() + " Ft";
            txt_TotalWorkCost.Text = TotalWorkCost.ToString() + " Ft";
            txt_TotalWorkTime.Text = $"{(TotalWorkTime % 60 == 0 ? TotalWorkTime / 60 + " ó" + " 0 p" : TotalWorkTime / 60 + " ó " + TotalWorkTime % 60 + " p")}";
            txt_TotalAmount.Text = TotalCost.ToString() + " Ft";

            StaticData.Instance.Services.Clear();
        }
    }
}
