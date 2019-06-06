using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype1_1
{
    /// <summary>
    /// 工厂类
    /// </summary>
    class Factory
    {
        /// <summary>
        /// 返回需要的对象
        /// </summary>
        /// <param name="index"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserControl Choose(string uid ,int index, string password)     //简单工厂模式
        {
            UserControl user = null;
            try
            {
                int id = Convert.ToInt32(uid);
                string sql = @"SELECT password FROM login WHERE id = {0};";
                sql = string.Format(sql, id);
                DataTable pw_table = DbOS.dataSet(sql).Tables[0];
                string pw = pw_table.Rows[0][0].ToString();
                if (pw == password)
                {
                    switch (index)
                    {
                        case 0:
                            user = new Regestrion(id);
                            break;
                        case 1:
                            user = new OutPatient(id);
                            break;
                        case 2:
                            user = new Chasier(id);
                            break;
                    }
                }
                else
                {
                    string message = "账号或密码错误！";
                    MBox.Warn(message);
                }
            }
            catch
            {
                MBox.Warn("请输入账号和密码！");
            }
            return user;
        }

        public static UserControl RegChoice(int index)
        {
            UserControl reg = new Regreg();
            /*switch (index)
            {
                case 0:
                    reg = new Regreg();
                    break;
                case 1:
                    reg = new Regreg();
                    break;
                case 2:
                    reg = new Regreg();
                    break;
            }*/
            return reg;
        }
    }
}
