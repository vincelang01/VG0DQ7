using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VG0DQ7.Classes;

namespace VG0DQ7
{
    public partial class MainForm : Form
    {
        List<Work> works;
        public MainForm()
        {
            InitializeComponent();
            fizetésToolStripMenuItem.Enabled = false;
            munkalapToolStripMenuItem.Enabled = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Biztosan be akarod zárni az alkalmazást?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            } 
        }

        private void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fájlMegnyitásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = Application.StartupPath
            };

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    works = new List<Work>(new Loader<Work>().LoadFromFile(openFileDialog.FileName, Parser.Parse));
                    StaticData.Instance.SetData(works);
                    munkalapToolStripMenuItem.Enabled = true;

                } catch(FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (IndexOutOfRangeException ex)
                {
                    MessageBox.Show("Rossz formátumú fájl!\n" + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
            }
        }

        private void névjegyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lang Vince\nVG0DQ7\n" + DateTime.Now.ToShortDateString(), "Névjegy", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void munkalapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new WorkSheetForm().ShowDialog();
            if(StaticData.Instance.Services.Count > 0) { fizetésToolStripMenuItem.Enabled = true; }
        }

        private void fizetésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(StaticData.Instance.Services.Count == 0) { MessageBox.Show("Nincs rögzített munkalap!");  }
            else if(MessageBox.Show("Biztosan szeretne fizetni?\n", "Fizetés megerősítése", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new PayForm().ShowDialog();
                fizetésToolStripMenuItem.Enabled = false;
            }
        }
    }
}
