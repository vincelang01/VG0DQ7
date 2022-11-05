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
    public partial class WorkSheetForm : Form
    {
        StaticData staticData = StaticData.Instance;
        Services serviceOrder { get; set; }
        bool recordClose = false;
        private DataTable dt;

        public WorkSheetForm()
        {
            InitializeComponent();
            dt = new DataTable();
            serviceOrder = new Services();
            FillDataTable();
        }

        private void FillDataTable()
        {
            DataColumn name = new DataColumn()
            {
                ColumnName = " ",
                DataType = typeof(string),
                ReadOnly = true,
            };
            DataColumn CostOfMaterial = new DataColumn()
            {
                ColumnName = "AnyagKöltség",
                DataType = typeof(string),
                ReadOnly = true
            };
            DataColumn TimeOfWork = new DataColumn()
            {
                ColumnName = "Munkaidő",
                DataType = typeof(string),
                ReadOnly = true
            };
            DataColumn checkBoxColumn = new DataColumn()
            {
                ColumnName = "  ",
                DataType = typeof(bool),
                ReadOnly = false
            };
            dt.Columns.Add(name);
            dt.Columns.Add(CostOfMaterial);
            dt.Columns.Add(TimeOfWork);
            dt.Columns.Add(checkBoxColumn);
            DataColumn CostOfService = new DataColumn()
            {
                ColumnName = "Munkadíj",
                DataType = typeof(string),
                ReadOnly = true

            };
            dt.Columns.Add(CostOfService);

            foreach (Work item in staticData.works)
            {
                bool checkBox = false;
                dt.Rows.Add(item.Name, item.CostOfWork.ToString() + " Ft", item.WorkTime, checkBox, " ");
            }
            datagridviewDesign();
        }

        private void datagridviewDesign()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 11);
            dataGridView1.RowTemplate.Height = 34;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            List<Work> works = StaticData.Instance.works;
            if (dataGridView1.DataSource != null)
            {
                bool newBool = false;

                DataRow dr = dt.NewRow();
                dr[0] = staticData.works[e.RowIndex].Name.ToString();
                dr[1] = staticData.works[e.RowIndex].CostOfWork.ToString() + " Ft";
                dr[2] = staticData.works[e.RowIndex].WorkTime.ToString();

                if (dt.Rows[e.RowIndex][3].Equals(true))
                {
                    newBool = true;
                    
                    dr[4] = staticData.works[e.RowIndex].CostOfService.ToString() + " Ft";
                    serviceOrder.AddItem(staticData.works[e.RowIndex]);
                    txt_costOfService.Text = serviceOrder.TotalWorkTimeCost.ToString() + " Ft";
                    txt_workCost.Text = serviceOrder.TotalCostOfService.ToString() + " Ft";
                }
                else
                {
                    newBool = false;
                    dr[4] = " ";
                    serviceOrder.RemoveItem(staticData.works[e.RowIndex]);
                    txt_costOfService.Text = serviceOrder.TotalWorkTimeCost.ToString() + " Ft";
                    txt_workCost.Text = serviceOrder.TotalCostOfService.ToString() + " Ft";
                }
                dt.Rows[e.RowIndex].Delete();
                dr[3] = newBool;
                dt.Rows.InsertAt(dr, e.RowIndex);
            }
            datagridviewDesign();
        }

        private void WorkSheetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!recordClose)
            {
                if (MessageBox.Show("Biztosan be szeretné zárni az ablakot?\nMinden nem elmentett változtatás elveszik!", "Figyelmeztetés", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                }
                else
                {
                    e.Cancel = true;
                }
            }
            recordClose = false;
        }

        private void btn_record_Click(object sender, EventArgs e)
        {
            bool hasTrue = false;
            int j = 0;
            while(j < dataGridView1.Rows.Count && !hasTrue)
            {
                if (dataGridView1.Rows[j].Cells[3].Value.Equals(true))
                {
                    hasTrue = true;
                }
                j++;
            }
            
            if(hasTrue && MessageBox.Show("Rögzíti a munkalapot?", "Munkalap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.Equals(true))
                    {
                        serviceOrder.AddItem(staticData.works[i]);
                    }
                }
                StaticData.Instance.Services.Add(serviceOrder);
                recordClose = true;

                Close();
            }
            else
            {
                if(MessageBox.Show("Nincs mentett munka!\nBezárja a lapot?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    recordClose = true;
                    Close();
                }
            }
        }
    }
}
