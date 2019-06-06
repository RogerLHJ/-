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
    public partial class OutPatient : UserControl
    {
        private int doc_id;
        private string doc_name;
        private string br_dep_name;
        public OutPatient(int id)
        {
            InitializeComponent();
            doc_id = id;
            inf();
           
        }
        private int flag = 0;
        /// <summary>
        /// 传染病目录初始化
        /// </summary>
        private void inf()
        {

            DataTable inf_table = new DataTable();
            inf_table = patient.inf_search();
            foreach (DataRow row in inf_table.Rows)
            {
                string de_items = row[1].ToString();
                comboBox2.Items.Add(de_items);
            }
            inf_table.Dispose();
            comboBox2.SelectedIndex = 4;
        }
        private void med_search()
        {
            comboBox1.Items.Clear();
            DataTable med_data = new DataTable();
            string com = string.Format("SELECT med_name,med_unit FROM medicine WHERE med_name LIKE '%{0}%';",comboBox1.Text);
            med_data = DbOS.dataSet(com).Tables[0];
            int cnt = med_data.Rows.Count;
            label17.Text = med_data.Rows[0][1].ToString();
            for(int i = 0; i < cnt; i++)
            {
                string med_text = med_data.Rows[i][0].ToString();
                comboBox1.Items.Add(med_text);
            }
        }
        private void 诊断统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSplitButton4_ButtonClick(object sender, EventArgs e)
        {

        }
        private void p_search()
        {
            string sql = @"SELECT A.doc_name, B.br_dep_name FROM doctor A, br_department B WHERE A.id = {0} AND B.id = A.br_dep_id ;";
            sql = string.Format(sql, doc_id);
            DataTable data = DbOS.dataSet(sql).Tables[0];
            string d_name = data.Rows[0][0].ToString();
            doc_name = d_name;
            br_dep_name = data.Rows[0][1].ToString();
            DataTable p_table = new DataTable();
            string com = @"SELECT A.id,B.pat_name,B.pat_telnum FROM registration A, patient B WHERE A.card_id = B.id AND doc_name = '{0}' AND is_red = 0 AND is_cancel = 0; ";
            com = string.Format(com,d_name);
            p_table = DbOS.dataSet(com).Tables[0];
            try
            {
                b_id = Convert.ToInt64(p_table.Rows[0][0].ToString());
            }
            catch
            {

            }
            dataGridView1.DataSource = p_table;
            string[] Listhead = new string[] { "病案号", "患者姓名", "联系电话" };
            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }
        private long b_id;
        private long pb_id;
        private int dm_id;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            flag = 1;
            p_search();
        }

        private void 常用药物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 2;
            DataTable med_table = new DataTable();
            string com = @"SELECT med_name,med_price,med_unit,med_spec,med_effect,med_usage FROM medicine WHERE med_type = 0 AND med_surplus>0; ";
            med_table = DbOS.dataSet(com).Tables[0];
            dataGridView1.DataSource = med_table;
            string[] Listhead = new string[] { "药品名", "药价", "单位", "药物规格","药物功效", "使用说明" };
            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }
        /// <summary>
        /// 患者药物的展示
        /// </summary>
        private void p_med()
        {
            DataTable p_table = new DataTable();
            Console.WriteLine(pb_id);
            string com = string.Format("SELECT id , med_name, med_price,med_data,med_usege,med_unit FROM p_medcient WHERE b_id = {0}", pb_id);
            p_table = DbOS.dataSet(com).Tables[0];
            dataGridView2.DataSource = p_table;
            string[] Listhead = new string[] { "序号", "药品名", "药品单价", "数量", "使用说明","单位" };
            for (int i = 0; i < 6; i++)
            {
                dataGridView2.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            med_search();
        }

        private string GetString(DataGridView dt,int index)
        {

            string s = "";
            if (dt.Focused == true)
            {
                int RowIndex = dt.CurrentCell.RowIndex;     //当前单元格所在行
                if (RowIndex < 0)
                {
                    s = "";
                }
                else
                {
                    s = dt.Rows[RowIndex].Cells[index].Value.ToString();
                }
            }
            return s;
        }

        private void p_mes()
        {
            DataTable p_t = new DataTable();
            string com = string.Format("SELECT B.id, B.doc_name,B.re_date,B.card_id,A.pat_name,A.pat_telnum,A.pat_id FROM patient A,registration B WHERE A.id = B.card_id AND B.id = {0};", pb_id);
            p_t = DbOS.dataSet(com).Tables[0];
            textBox1.Text = b_id.ToString();
            textBox2.Text = br_dep_name;
            textBox3.Text = p_t.Rows[0][1].ToString();
            textBox10.Text = p_t.Rows[0][2].ToString();
            textBox6.Text = p_t.Rows[0][3].ToString();
            textBox5.Text = p_t.Rows[0][4].ToString();
            textBox11.Text = p_t.Rows[0][5].ToString();
            string br = p_t.Rows[0][6].ToString().Substring(6, 8).Trim();
            int sex = Convert.ToInt32(p_t.Rows[0][6].ToString().Substring(16,1));
            if(sex%2 == 0)
            {
                textBox4.Text = "女";
            }
            else
            {
                textBox4.Text = "男";
            }
            dateTimePicker2.Value = DateTime.ParseExact(br, "yyyyMMdd", null);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string com = string.Format("UPDATE registration SET is_red = 1 WHERE id = {0};", b_id);
                DbOS.GetSqlcom(com);
                pb_id = b_id;
                p_search();
                dm_id = 0;
                p_mes();
            }
            catch
            {

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable pm_table = new DataTable();
                string med_com = comboBox1.Text;
                string com1 = string.Format("SELECT * FROM medicine WHERE med_name = '{0}';", med_com);
                pm_table = DbOS.dataSet(com1).Tables[0];
                int m_id = Convert.ToInt32(pm_table.Rows[0][0].ToString());
                string m_name = pm_table.Rows[0][1].ToString();
                double m_price = Convert.ToDouble(pm_table.Rows[0][2].ToString());
                string m_usage = pm_table.Rows[0][7].ToString();
                int m_data = Convert.ToInt32(numericUpDown1.Value);
                string m_unit = pm_table.Rows[0][3].ToString();
                string com2 = string.Format("INSERT p_medcient VALUES({0},{1},{2},'{3}',{4},'{5}',{6},'{7}')", dm_id, pb_id, m_id, m_name, m_price, m_usage, m_data, m_unit);
                DbOS.GetSqlcom(com2);
                dm_id = dm_id + 1;
                p_med();
            }
            catch
            {

            }
        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sid = GetString(dataGridView1,0);
            try
            {
                if (flag == 1)
                {
                    b_id = Convert.ToInt64(sid);
                }
                if (flag == 2)
                {
                    comboBox1.Text = sid.ToString();
                }
            }
            catch
            {

            }
        }

        private void 处方药物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 2;
            DataTable med_table = new DataTable();
            string com = @"SELECT med_name,med_price,med_unit,med_spec,med_effect,med_usage FROM medicine WHERE med_type = 1 AND med_surplus>0; ";
            med_table = DbOS.dataSet(com).Tables[0];
            dataGridView1.DataSource = med_table;
            string[] Listhead = new string[] { "药品名", "药价", "单位", "药物规格", "药物功效", "使用说明" };
            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int card_id = Convert.ToInt32(textBox6.Text);
                long doc_id = 10015;
                string m_his = richTextBox1.Text;
                string t_d = richTextBox3.Text;
                string inf = comboBox2.SelectedItem.ToString();
                string com = @"INSERT prescription VALUES({0},{1},{2},'{3}','{4}','{5}');";
                com = string.Format(com, pb_id, card_id, doc_id, m_his, t_d, inf);
                DbOS.GetSqlcom(com);
                string com1 = @"SELECT cnt FROM inf WHERE inf_name = '{0}';";
                com1 = string.Format(com1, inf);
                DataTable inf_table = new DataTable();
                inf_table = DbOS.dataSet(com1).Tables[0];
                int cnt = Convert.ToInt32(inf_table.Rows[0][0].ToString());
                cnt = cnt + 1;
                string com2 = @"UPDATE inf SET cnt = {0} WHERE inf_name = '{1}';";
                com2 = string.Format(com2, cnt, inf);
                DbOS.GetSqlcom(com2);
                button6_Click(sender, e);
            }
            catch
            {
                MBox.Warn("请输入病历！");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
        }
        string med_name;
        private void button10_Click(object sender, EventArgs e)
        {
            Console.WriteLine(med_name);
            string com = @"DELETE FROM p_medcient WHERE med_name = '{0}' AND b_id = {1};";
            com = string.Format(com, med_name, pb_id);
            DbOS.GetSqlcom(com);
            p_med();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            med_name = GetString(dataGridView2,1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 传染病统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable inf_table = new DataTable();
            string com = @"SELECT inf_name , cnt FROM inf; ";
            inf_table = DbOS.dataSet(com).Tables[0];
            dataGridView1.DataSource = inf_table;
            string[] Listhead = new string[] { "传染病类型", "数量"};
            for (int i = 0; i < 2; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }

        private void 用药统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 0;
            DataTable med_table = new DataTable();
            string com = @"SELECT med_name,SUM(med_data),med_unit FROM p_medcient GROUP BY med_name; ";
            med_table = DbOS.dataSet(com).Tables[0];
            dataGridView1.DataSource = med_table;
            string[] Listhead = new string[] { "药名", "数量","单位" };
            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }

        private void 工作量统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT A.doc_name, B.br_dep_name FROM doctor A, br_department B WHERE A.id = {0} AND B.id = A.br_dep_id ;";
            sql = string.Format(sql, doc_id);
            DataTable data = DbOS.dataSet(sql).Tables[0];
            string d_name = data.Rows[0][0].ToString();
            doc_name = d_name;
            flag = 0;
            DataTable med_table = new DataTable();
            string com = @"SELECT doc_name ,SUM(10 - res) , date, time FROM arrangement GROUP BY doc_name HAVING doc_name = '{0}';";
            com = string.Format(com, doc_name);
            med_table = DbOS.dataSet(com).Tables[0];
            dataGridView1.DataSource = med_table;
            string[] Listhead = new string[] { "医生名", "就诊量","日期","时间" };
            for (int i = 0; i < 4; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }

        private void 检查单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report_bil bil = new report_bil();
            bil.textBox1.Text = pb_id.ToString();
            bil.textBox2.Text = textBox5.Text;
            bil.textBox3.Text = textBox2.Text;
            bil.textBox4.Text = textBox3.Text;
            bil.ShowDialog();
        }

        private void 住院单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Indepar indepar = new Indepar();
            indepar.textBox1.Text = pb_id.ToString();
            indepar.textBox2.Text = textBox5.Text;
            indepar.textBox3.Text = textBox2.Text;
            indepar.textBox4.Text = doc_name;
            indepar.ShowDialog();
        }

        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
            report_bil report = new report_bil();
            report.ShowDialog();
        }
    }
}
