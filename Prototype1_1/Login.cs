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
    public partial class Login : UserControl
    {
        Form1 form;
        public Login(Form1 form1)   //传入Form1避免再创建实例
        {
            InitializeComponent();
            form = form1;
        }
        UserControl user;
        private void button1_Click(object sender, EventArgs e)
        {
            form.panel1.Controls.Clear();
            string userid = textBox1.Text.ToString();
            string password = textBox2.Text.ToString();
            //password = Md5.MD5Encrypt32(password);
            user = Factory.Choose(userid,comboBox1.SelectedIndex,password);     //简单工厂
            if(user != null)
            {
                user.Parent = form.panel1;
                user.Dock = DockStyle.Fill;
                form.EnableItem(true);
                this.Dispose();
                GC.Collect();   //释放窗体后强制垃圾回收
            }
        }
        public bool logout()
        {
            if (MBox.Hint1("确定需要退出吗？"))
            {
                user.Dispose(); //退出账户时可以进行内存释放
                GC.Collect();   //强制回收内存
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("挂号员");
            comboBox1.Items.Add("门诊医生");
            comboBox1.Items.Add("收费员");
            comboBox1.SelectedIndex = 0;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
