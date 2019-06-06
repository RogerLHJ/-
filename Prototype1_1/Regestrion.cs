using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;


namespace Prototype1_1
{
    public partial class Regestrion : UserControl
    {
        /// <summary>
        /// 病案号
        /// </summary>
        private long bin_id = 0;
        private void Bid()
        {
            bin_id = DateTime.Now.Ticks;
            //Console.WriteLine(bin_id);
        }
        private void id_load()
        {
            string sql = @"SELECT MAX(id) FROM patient;";
            DataTable id_data = new DataTable();
            id_data = DbOS.dataSet(sql).Tables[0];
            int id = Convert.ToInt32(id_data.Rows[0][0].ToString());
            id = id + 1;
            textBox1.Text = id.ToString();
        }
        private int rer_id;
        public Regestrion(int re_id)
        {
            InitializeComponent();
            rer_id = re_id;
            Bid();
            label16.Text = "№" + bin_id.ToString();
            dep_load();
            comboBox3.Items.Add("身份证");
            all_load();
            EventArgs e = EventArgs.Empty;
            toolStripButton1_Click(this,e);
            id_load();
        }
        /// <summary>
        /// 初始化部门信息
        /// </summary>
        private void dep_load()
        {
            DataTable dep_table = new DataTable();
            dep_table = Department.Dep_search();
            foreach (DataRow row in dep_table.Rows)
            {
                string de_items = row[1].ToString();
                comboBox1.Items.Add(de_items);
            }
            dep_table.Dispose();
            comboBox1.SelectedIndex = 0;
        }
        /// <summary>
        /// 初始化专科信息
        /// </summary>
        private void br_dep_load()
        {
            try
            {
                DataTable br_dep_table = new DataTable();
                br_dep_table = br_department.Br_dep_search(comboBox1.SelectedItem.ToString());
                foreach (DataRow row in br_dep_table.Rows)
                {
                    string br_de_items = row[0].ToString();
                    comboBox2.Items.Add(br_de_items);
                }
                br_dep_table.Dispose();
                comboBox2.SelectedIndex = 0;
            }
            catch
            {
                MBox.Warn("该科室暂无医生！");
            }
        }
        /// <summary>
        /// 初始化医生
        /// </summary>
        private void doc_load()
        {
            DataTable doc_table = new DataTable();
            doc_table = Docotor.doc_search(comboBox2.SelectedItem.ToString());
            foreach (DataRow row in doc_table.Rows)
            {
                string doc_items = row[0].ToString();
                comboBox4.Items.Add(doc_items);
            }
            doc_table.Dispose();
            try
            {
                comboBox4.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MBox.Warn("该科室仍无医师！");
            }

        }
        /// <summary>
        /// 初始化排班中的日期
        /// </summary>
        private void date_load()
        {
            DataTable date_table = new DataTable();
            date_table = Docotor.date_search(comboBox4.SelectedItem.ToString());
            foreach (DataRow row in date_table.Rows)
            {
                string date_items = row[0].ToString();
                comboBox5.Items.Add(date_items);
            }
            date_table.Dispose();
            try
            {
                comboBox5.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MBox.Warn("已满号！");
            }

        }
        /// <summary>
        /// 时间段的初始化
        /// </summary>
        private void time_load()
        {
            DataTable time_table = new DataTable();
            time_table = Docotor.time_search(comboBox5.SelectedItem.ToString(), comboBox4.SelectedItem.ToString());
            foreach (DataRow row in time_table.Rows)
            {
                string time_items = row[0].ToString();
                comboBox9.Items.Add(time_items);
            }
            time_table.Dispose();
            try
            {
                comboBox9.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MBox.Warn("已满号！");
            }

        }
        /// <summary>
        /// 收费价格初始化
        /// </summary>
        private void charge_load()
        {
            DataTable charge_table = new DataTable();
            charge_table = Docotor.charge_search(comboBox4.SelectedItem.ToString());
            foreach (DataRow row in charge_table.Rows)
            {
                string charge_items = row[0].ToString();
                comboBox7.Items.Add(charge_items);
            }
            charge_table.Dispose();
            try
            {
                comboBox7.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }

        }
        /// <summary>
        /// 过敏源的初始化
        /// </summary>
        private void all_load()
        {
            DataTable all_table = new DataTable();
            all_table = Docotor.all_search();
            foreach (DataRow row in all_table.Rows)
            {
                string all_items = row[0].ToString();
                comboBox6.Items.Add(all_items);
            }
            all_table.Dispose();
            try
            {
                comboBox6.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }

        }
        patient Patient = new patient();
        /// <summary>
        /// 病人信息插入
        /// </summary>
        private void Pat_Message()
        {
            try
            {
                Patient.Id = Convert.ToInt32(textBox1.Text);
                Patient.Pat_name = textBox2.Text;
                Patient.Pat_id = textBox3.Text;
                string bir = Patient.Pat_id.Substring(6, 8).Trim();
                dateTimePicker1.Value = DateTime.ParseExact(bir, "yyyyMMdd", null);
                Patient.Is_married = Convert.ToInt16(radioButton1.Checked);
                Patient.Pat_telnum = textBox4.Text;
                Patient.Pat_address = textBox5.Text;
                Patient.Pat_allergen = comboBox6.Text;
                Patient.Pat_pre_his = richTextBox1.Text;
                Patient.Start = comboBox5.Text;
                Patient.End = comboBox9.Text;

            }
            catch
            {
                MBox.Warn("信息填写错误！");
            }
        }

        private void Pat_show()
        {
            DataTable pat_table = new DataTable();
            string pat_id = textBox3.Text;
            string id = textBox1.Text;

            try
            {
                string com = string.Empty;
                if (pat_id.Length == 18)
                {

                    com = string.Format("SELECT * FROM patient WHERE pat_id = '{0}'", pat_id);
                }
                if (id.Length == 10)
                {
                    com = string.Format("SELECT * FROM patient WHERE id = {0}", id);
                }
                pat_table = DbOS.dataSet(com).Tables[0];
                textBox1.Text = pat_table.Rows[0][0].ToString();
                textBox2.Text = pat_table.Rows[0][1].ToString();
                textBox3.Text = pat_table.Rows[0][3].ToString();
                string bir = textBox3.Text.ToString().Substring(6, 8).Trim();
                dateTimePicker1.Value = DateTime.ParseExact(bir, "yyyyMMdd", null);
                bool married = Convert.ToBoolean(pat_table.Rows[0][4].ToString());
                if (married)
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                textBox4.Text = pat_table.Rows[0][2].ToString();
                textBox5.Text = pat_table.Rows[0][5].ToString();
                comboBox6.Text = pat_table.Rows[0][7].ToString();
                richTextBox1.Text = pat_table.Rows[0][6].ToString();
            }
            catch
            {
                MBox.Warn("请输入正确的身份证号或诊疗卡号！");
            }
        }
        private void label9_Click(object sender, EventArgs e)
        {
            Pat_Message();
            string com = string.Format("INSERT INTO patient(id, pat_name,pat_telnum,pat_id,is_married,pat_address,pat_pre_his,pat_allegen)" + "VALUES('{0}','{1}' ,'{2}', '{3}', {4} ,'{5}','{6}','{7}')", Patient.Id, Patient.Pat_name, Patient.Pat_telnum, Patient.Pat_id, Patient.Is_married, Patient.Pat_address, Patient.Pat_pre_his, Patient.Pat_allergen);
            DbOS.GetSqlcom(com);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox4.Items.Clear();
            br_dep_load();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            doc_load();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataTable doc_table = new DataTable();
            string com = @"SELECT dep_name as 科室名,br_dep_name as 专科名,doc_name as 医生姓名,date as 值班日期,time as 值班时段,res as 剩余号数,re_charge as 挂号费用 FROM arrangement ;";
            doc_table = DbOS.dataSet(com).Tables[0];
            dataGridView1.DataSource = doc_table;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime today2 = new DateTime(now.Year, now.Month, now.Day);
            string today = today2.ToString("yyyy/MM/dd");
            DataTable p_table = new DataTable();
            string com = String.Format("SELECT B.id as 病案号, A.pat_name as 患者姓名,A.pat_telnum as 联系电话 ,B.doc_name as 医生姓名,B.re_date as 就诊日期,B.re_time as 就诊时间 ,B.reg_charge as 挂号金额 FROM patient A,registration B WHERE B.is_cancel = 0 AND A.id = B.card_id AND B.re_date ='{0}';", today);
            Console.WriteLine(com);
            p_table = DbOS.dataSet(com).Tables[0];
            dataGridView1.DataSource = p_table;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataTable p_table = new DataTable();
            string com = @"SELECT B.id as 病案号, A.pat_name as 患者姓名,A.pat_telnum as 联系电话 ,B.doc_name as 医生姓名,B.re_date as 就诊日期,B.re_time as 就诊时间 ,B.reg_charge as 挂号金额 FROM patient A,registration B WHERE B.is_cancel = 1 AND A.id = B.card_id; ";
            p_table = DbOS.dataSet(com).Tables[0];
            dataGridView1.DataSource = p_table;

        }
        private void Reg(int cancel)
        {
            DataTable p_table = new DataTable();
            string doc_index = comboBox4.SelectedItem.ToString();
            long b_id = Convert.ToInt64(label16.Text.ToString().Substring(1, 16));
            long p_id = Convert.ToInt64(textBox1.Text.ToString());
            int re_charge = Convert.ToInt32(comboBox7.Text.ToString());
            string start = Patient.Start.ToString();
            string end = Patient.End.ToString();
            string comstr = string.Format("INSERT INTO registration VALUES({0},'{1}','{2}','',{3},{4},'{5}','{6}',0);", b_id, doc_index, p_id, re_charge, cancel, start, end);
            DbOS.GetSqlcom(comstr);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Pat_Message();
            Reg(0);
            Bid();
            print();
            button2_Click(this, e);
            string com = @"SELECT res FROM arrangement WHERE  doc_name = '{0}' AND date = '{1}'  AND time = '{2}';";
            com = string.Format(com, comboBox4.SelectedItem.ToString(), comboBox5.SelectedItem.ToString(), comboBox9.SelectedItem.ToString());
            DataTable data = DbOS.dataSet(com).Tables[0];
            int res = Convert.ToInt32(data.Rows[0][0].ToString());
            res = res - 1;
            string sqlcom = @"UPDATE arrangement SET res = {0} WHERE doc_name = '{1}' AND date = '{2}'  AND time = '{3}';";
            sqlcom = string.Format(sqlcom, res, comboBox4.SelectedItem.ToString(), comboBox5.SelectedItem.ToString(), comboBox9.SelectedItem.ToString());
            DbOS.GetSqlcom(sqlcom);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Pat_show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            richTextBox1.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox9.Items.Clear();
            time_load();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            comboBox7.Items.Clear();
            charge_load();
            date_load();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pat_Message();
            Reg(1);
            Bid();
            button2_Click(this, e);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //增加


        /// <summary>
        /// 退号单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            string s = "";
            long id;
            if (dataGridView1.Focused == true)
            {
                int RowIndex = dataGridView1.CurrentCell.RowIndex; //当前单元格所在行
                s = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
                if (s == "")
                {

                    MessageBox.Show("未选中！");
                }
                else
                {

                    id = Convert.ToInt64(s);
                    if (MessageBox.Show(s + "是否退号?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        string comstr = string.Format("UPDATE registration SET is_cancel = 1 WHERE id = '{0}';", id);
                        DbOS.GetSqlcom(comstr);
                        //成功则刷新挂号页面
                        toolStripButton1_Click(this, e);

                    }

                }
            }
            else
            {
                MessageBox.Show("请选择病人！");
            }
        }

        /// <summary>
        /// 恢复挂号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            string s = "";
            long id;
            if (dataGridView1.Focused == true)
            {
                int RowIndex = dataGridView1.CurrentCell.RowIndex; //当前单元格所在行
                s = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
                if (s == "")
                {

                    MessageBox.Show("未选中！");
                }
                else
                {
                    s = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
                    id = Convert.ToInt64(s);
                    if (MessageBox.Show(s + "是否恢复挂号?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        string comstr = string.Format("UPDATE registration SET is_cancel = 0 WHERE id = '{0}';", id);
                        DbOS.GetSqlcom(comstr);
                        //成功则刷新挂号页面
                        toolStripButton1_Click(this, e);

                    }

                }
            }
            else
            {
                MessageBox.Show("请选择病人！");
            }
        }


        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，您的电脑可能未安装Excel");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1 
            //写入标题             
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            { worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText; }
            //写入数值
            for (int r = 0; r < dataGridView1.Rows.Count; r++)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dataGridView1.Rows[r].Cells[i].Value;
                }

                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            MessageBox.Show(fileName + "资料保存成功", "提示", MessageBoxButtons.OK);
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);  //fileSaved = true;                 
                }
                catch (Exception ex)
                {//fileSaved = false;                      
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//强行销毁           }
        }


        /// <summary>
        /// 打印功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DGVPrinterForm dGVPrinterForm = new DGVPrinterForm(dataGridView1);
            dGVPrinterForm.ShowDialog();
        }

        /// <summary>
        /// 查询功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string s = toolStripTextBox1.Text;
            if (s != "")
            {
                long id = Convert.ToInt64(s);
                DataTable p_table = new DataTable();
                string comstr = string.Format("SELECT B.id as 病案号, A.pat_name as 患者姓名,A.pat_telnum as 联系电话 ,B.doc_name as 医生姓名,B.re_date as 就诊日期,B.re_time as 就诊时间 ,B.reg_charge as 挂号金额,B.is_cancel as 状态 FROM patient A,registration B WHERE B.card_id = {0} AND A.id = B.card_id; ", id);
                Console.WriteLine(comstr);
                p_table = DbOS.dataSet(comstr).Tables[0];
                dataGridView1.DataSource = p_table;

            }
            else
            {
                MessageBox.Show("请输入病人ID！");
            }



        }

        private void 医生工作量统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable p_table = new DataTable();
            string comstr = "select dep_name as 科室, doc_name as 医生姓名, sum(10 - res) as 当月工作量 from arrangement group by doc_name;";
            Console.WriteLine(comstr);
            p_table = DbOS.dataSet(comstr).Tables[0];
            dataGridView1.DataSource = p_table;
        }

        private void 挂号收款汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable p_table = new DataTable();
            string comstr = "select br_dep_name as 科室,sum((10-res)*re_charge) as 当月挂号收款 from arrangement group by br_dep_name;";
            Console.WriteLine(comstr);
            p_table = DbOS.dataSet(comstr).Tables[0];
            dataGridView1.DataSource = p_table;
        }

        private void 当日结账ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable p_table = new DataTable();
            string comstr = "select br_dep_name as 科室,date as 日期,sum((10-res)*re_charge) as 收款 from arrangement group by br_dep_name,date;";
            Console.WriteLine(comstr);
            p_table = DbOS.dataSet(comstr).Tables[0];
            dataGridView1.DataSource = p_table;
        }
        private void print()
        {
            if (textBox2.Text != "")
            {
                string today = DateTime.Now.ToLocalTime().ToString();
                string[] str = new string[8];
                str[0] = "病  案  号：" + label16.Text;
                str[1] = "姓    名：" + textBox2.Text;
                str[2] = "时      间：" + today;
                str[3] = "科    室：" + comboBox1.Text;
                str[4] = "专    科：" + comboBox2.Text;
                str[5] = "医    生：" + comboBox4.Text;
                str[6] = "就诊时间：" + comboBox5.Text + " " + comboBox9.Text;
                str[7] = "挂 号 费：" + comboBox7.Text + " 元";
                RePrinterForm rePrinterForm = new RePrinterForm(str);
                rePrinterForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("请输入信息");
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {
            try
            {
                Pat_Message();
                string com = @"UPDATE patient SET pat_name = '{0}',pat_telnum = '{1}',pat_id='{2}',is_married = '{3}',pat_address='{4}',pat_pre_his = '{5}',pat_allegen='{6}' WHERE id ={7} ;";
                com = string.Format(com, Patient.Pat_name, Patient.Pat_telnum, Patient.Pat_id, Patient.Is_married, Patient.Pat_address, Patient.Pat_pre_his, Patient.Pat_allergen, Patient.Id);
                DbOS.GetSqlcom(com);
                MBox.Warn("修改成功！");
            }
            catch
            {
                MBox.Warn("修改有误！");
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            try
            {
                string com = @"DELETE patient WHERE id ={0};";
                com = string.Format(com, Patient.Id);
                DbOS.GetSqlcom(com);
                MBox.Warn("注销成功！");
            }
            catch
            {
                MBox.Warn("注销有误！");
            }
        }
    }
}
