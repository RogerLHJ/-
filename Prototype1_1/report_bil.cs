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
    public partial class report_bil : Form
    {
        public report_bil()
        {
            InitializeComponent();
            item_load();
        }

        private void item_load()
        {
            DataTable item_table = new DataTable();
            string sql = @"SELECT item FROM re_item;";
            item_table = DbOS.dataSet(sql).Tables[0];
            foreach (DataRow row in item_table.Rows)
            {
                string items = row[0].ToString();
                comboBox1.Items.Add(items);
            }
            item_table.Dispose();
            comboBox1.SelectedIndex = 0;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private long bid;
        private void button2_Click(object sender, EventArgs e)
        {
            bid = Convert.ToInt64(textBox1.Text.ToString());
            string item_name = comboBox1.SelectedItem.ToString();
            string sql = @"SELECT price FROM re_item WHERE item = '{0}';";
            sql = string.Format(sql, item_name);
            DataTable item_table = new DataTable();
            item_table = DbOS.dataSet(sql).Tables[0];
            int price = Convert.ToInt32(item_table.Rows[0][0].ToString());
            string sqlcom = @"INSERT item() VALUES('{0}',{1},'{2}','',{3})";
            string date = dateTimePicker1.Value.ToString();
            sqlcom = string.Format(sqlcom, item_name, bid, date,price);
            DbOS.GetSqlcom(sqlcom);
            item_show();
        }
        private void item_show(){
            string sql = @"SELECT pat_id,item_name,item_price FROM item WHERE pat_id = {0};";
            sql = string.Format(sql, bid);
            DataTable table = new DataTable();
            table =  DbOS.dataSet(sql).Tables[0];
            dataGridView1.DataSource = table;
            string[] Listhead = new string[] { "患者病案号", "检查项目", "价格" };
            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = Listhead[i];
            }
        }
    }
}
