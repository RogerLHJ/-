using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype1_1
{
    partial class DGVPrinterForm : Form
    {
        
        DGVPrinter dgvPrinter;
        public DGVPrinterForm(DataGridView dgv)
        {
            InitializeComponent();
            dataGridView1.DataSource = dgv.DataSource;       
            dgvPrinter = new DGVPrinter();
            dgvPrinter.SourceDGV = dataGridView1;

            
        }

        private void DGVPrinterForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            dgvPrinter.PrintDataGridView(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgvPrinter.SetupPage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dgvPrinter.PrintPreview();

        }
    }
}
