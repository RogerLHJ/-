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
    public partial class Form1 : Form
    {
        Login login1;
        public Form1()
        {
            InitializeComponent();
        }
        public void EnableItem(bool flag)
        {
            toolStripButton1.Enabled = !flag;
            toolStripButton2.Enabled = flag;
            toolStripButton3.Enabled = flag;
            toolStripSplitButton1.Enabled = flag;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            EnableItem(false);
            login1 = new Login(this);  //传入自己，不需要再创建实例。
            login1.Parent = this.panel1;
            login1.Dock = DockStyle.Fill;
            login1.Show();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            login1 = new Login(this);  //传入自己，不需要再创建实例。
            login1.Parent = this.panel1;
            login1.Dock = DockStyle.Fill;
            login1.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (login1.logout())     //退出按钮触发退出方法
            {
                Form1_Load(sender, e);
            }
        }

        private void 密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RePass rePass = new RePass();
            rePass.ShowDialog();
            Form1_Load(sender, e);
        }

        private void 个人信息修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MBox.Hint1("请联系10086！");
        }
    }
}
