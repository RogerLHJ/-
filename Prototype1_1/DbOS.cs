using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Prototype1_1
{
    class DbOS
    {
        private static MySqlConnection conn;    //连接为全局变量
        /*为了减少对象的创建，使用静态方法*/
        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection Open()    //连接方法
        {
            //string constr = @"Server=localhost;User ID=root;Password=LHJ1996;Database=hisdb;CharSet='utf8';";
            string constr = @"server=210.38.111.41;port=5656;user=root;password=123456; database=hisdb;CharSet='utf8';";
            try
            {
                conn = new MySqlConnection(constr);
                conn.Open();    //连接成功
                return conn;
            }
            catch(Exception ex)
            {
                MBox.Warn("内部错误！");
                return null;
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public static void Conclose()   //关闭
        {
            if(conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        /// <summary>
        /// 读取表中指定信息
        /// </summary>
        /// <param name="comstr"></param>
        /// <returns></returns>
        public static MySqlDataReader ComRead(string comstr)
        {
            Open();
            MySqlCommand mySqlCommand = conn.CreateCommand();
            mySqlCommand.CommandText = comstr;
            mySqlCommand.CommandType = CommandType.Text;
            MySqlDataReader myread = mySqlCommand.ExecuteReader();
            return myread;
        }
        /// <summary>
        /// 其他的数据库操作命令
        /// </summary>
        /// <param name="comstr"></param>
        public static void GetSqlcom(string comstr)
        {
            Open();
            MySqlCommand mySqlCommand = new MySqlCommand(comstr, conn);
            mySqlCommand.ExecuteNonQuery();
            mySqlCommand.Dispose();
            Conclose();
        }
        /// <summary>
        /// 创建DataSet，将数据缓存本地
        /// </summary>
        /// <param name="comstr"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static DataSet dataSet(string comstr)
        {
            Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(comstr, conn);
            DataSet data = new DataSet();
            mySqlDataAdapter.Fill(data);
            Conclose();
            return data;
        }
    }
}
