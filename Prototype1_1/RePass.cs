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
    public partial class RePass : Form
    {
        public RePass()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text.ToString());
            string sql = @"SELECT password FROM login WHERE id = {0};";
            sql = string.Format(sql, id);
            DataTable pw_table = DbOS.dataSet(sql).Tables[0];
            string pw = pw_table.Rows[0][0].ToString();
            string tpw = textBox3.Text.ToString();
            if(tpw == textBox4.Text.ToString())
            {
                if (pw == textBox2.Text.ToString())
                {
                    string com = @"UPDATE login SET password = '{0}' WHERE id ={1} ;";
                    com = string.Format(com, tpw, id);
                    DbOS.GetSqlcom(com);
                    MBox.Hint1("修改成功！");
                    this.Close();
                }
                else
                {
                    MBox.Warn("原始密码错误！");
                }
            }
            else
            {
                MBox.Warn("两次密码不匹配！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
