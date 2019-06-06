using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype1_1
{
    public partial class Chasier : UserControl
    {
        private int ca_id;
        public Chasier(int c_id)
        {
            InitializeComponent();
            ca_id = c_id;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            string[] yibao = new string[] { "无", "城镇职工基本医疗保险", "城镇居民基本医疗保险", "新型农村合作医疗" };
            for(int i = 0; i < 4; i++)
            {
                comboBox2.Items.Add(yibao[i]);
            }
            comboBox2.SelectedIndex = 0;

        }
        private double item_c = 0;
        private double med_c = 0;
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            
        }
        private double total = 0;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            total = 0;
            try
            {
                if (checkBox2.Checked == true)
                {
                    med_c = med_show();
                    total = total + med_show();
                }
            }
            catch
            {

            }
            try
            {
                if (checkBox3.Checked == true)
                {
                    item_c = item_show();
                    total = total + item_show();
                }
            }
            catch
            {

            }
            textBox1.Text = total.ToString();
        }
        private double med_show()
        {
            double med_total = 0;
            DataTable p_chage = new DataTable();
            long id = 0;
            id = Convert.ToInt64(toolStripTextBox1.Text.ToString());
            string sqlcom = @"SELECT A.b_id,B.pat_name,A.med_name,A.med_price , A.med_data FROM p_medcient A, patient B,registration C WHERE A.b_id = {0} AND A.b_id = C.id AND C.card_id = B.id;";
            string com = string.Format(sqlcom, id);
            p_chage = DbOS.dataSet(com).Tables[0];
            label4.Text = id.ToString();
            label6.Text = p_chage.Rows[0][1].ToString();
            label7.Text = DateTime.Now.ToString();
            
            string[] Listhead = new string[] { "病案号", "患者姓名", "药物名", "药物单价", "药物数量" };
            dataGridView4.DataSource = p_chage;
            for (int i = 0; i < 5; i++)
            {
                dataGridView4.Columns[i].HeaderCell.Value = Listhead[i];
            }
            int cnt = p_chage.Rows.Count;
            for (int i = 0; i < cnt; i++)
            {
                double m_price = Convert.ToDouble(p_chage.Rows[i][3].ToString());
                double m_data = Convert.ToDouble(p_chage.Rows[i][4].ToString());
                med_total = med_total + m_price * m_data;
            }
            return med_total;
        }
        private double item_show()
        {
            double item_total = 0;
            DataTable item_chage = new DataTable();
            long id = Convert.ToInt64(toolStripTextBox1.Text.ToString());
            string sqlcom = @"SELECT A.pat_id,B.pat_name,A.item_name,A.item_price FROM item A, patient B,registration C WHERE A.pat_id = C.id AND B.id = C.card_id AND A.pat_id = {0};";
            string com = string.Format(sqlcom, id);
            item_chage = DbOS.dataSet(com).Tables[0];
            string[] Listhead = new string[] { "病案号", "患者姓名", "检查项目", "检查价格" };
            dataGridView2.DataSource = item_chage;
            for (int i = 0; i < 4; i++)
            {
                dataGridView2.Columns[i].HeaderCell.Value = Listhead[i];
            }
            int cnt = item_chage.Rows.Count;
            for (int i = 0; i < cnt; i++)
            {
                double m_price = Convert.ToDouble(item_chage.Rows[i][3].ToString());
                item_total = item_total + m_price;
            }
            return item_total;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == false)
            {
                total = total - med_c;
            }
            else if (checkBox2.Checked == true && toolStripTextBox1.Text.Length == 16)
            {
                total = total + med_c;
            }
            textBox1.Text = total.ToString();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine(toolStripTextBox1.Text.ToString().Length);
            if (checkBox3.Checked == false)
            {
                total = total - item_c;
            }
            else if (checkBox3.Checked == true && toolStripTextBox1.Text.Length == 16)
            {
                total = total + item_c;
            }
            textBox1.Text = total.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:textBox2.Text = total.ToString();
                    break;
                case 1:
                    textBox2.Text = (total*0.8).ToString();
                    break;
                case 2:
                    textBox2.Text = (total * 0.9).ToString();
                    break;
                case 3:
                    textBox2.Text = (total * 0.7).ToString();
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlcom = @"INSERT Pcharge VALUES({0},'{1}',{2},'{3}');";
            string sqlcom1 = @"UPDATE patient SET pat_census = '{0}' WHERE pat_name = '{1}' ;";
            sqlcom1 = string.Format(sqlcom1, textBox2.Text.ToString(), label6.Text.ToString());
            double cc = Convert.ToDouble(textBox1.Text.ToString());
            double pc = Convert.ToDouble(textBox2.Text.ToString());
            string date = DateTime.Now.ToString();
            sqlcom = string.Format(sqlcom, cc, comboBox2.Text.ToString(), pc, date);
            DbOS.dataSet(sqlcom);
            DbOS.GetSqlcom(sqlcom1);
        }
        private void Charge()
        {
            double med_total = 0;
            DataTable p_chage = new DataTable();
            string sqlcom = @"SELECT * FROM Pcharge";
            p_chage = DbOS.dataSet(sqlcom).Tables[0];
            string[] Listhead = new string[] { "收费(元)", "医保形式", "实收(元)" ,"收费时间"};
            dataGridView1.DataSource = p_chage;
            for (int i = 0; i < 4; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }
        private void pChage()
        {
            double med_total = 0;
            DataTable p_chage = new DataTable();
            string sqlcom = @"SELECT id,pat_name,pat_census FROM patient WHERE id = {0}";
            int id = Convert.ToInt32(toolStripTextBox2.Text.ToString());
            sqlcom = string.Format(sqlcom, id);
            p_chage = DbOS.dataSet(sqlcom).Tables[0];
            string[] Listhead = new string[] { "诊疗卡号", "患者姓名", "实收(元)"};
            dataGridView1.DataSource = p_chage;
            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }
        private void 日结算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Charge();
        }

        private void 月结算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Charge();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            pChage();
        }
    }
}
