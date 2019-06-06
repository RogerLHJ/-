using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype1_1
{
    class patient
    {
        private int id;
        private string pat_name;
        private string pat_sex;
        private DateTime pat_age;
        private string pat_telnum;
        private string pat_id;
        private int is_married;
        private string pat_address;
        private string pat_pre_his;
        private string pat_allergen;
        private string pat_census;
        private int pat_height;
        private int pat_weight;
        private string emer_name;
        private DateTime gmt_created;
        private DateTime gmt_modified;
        private string start;
        private string end;

        public int Id { get => id; set => id = value; }
        public string Pat_name { get => pat_name; set => pat_name = value; }
        public string Pat_sex { get => pat_sex; set => pat_sex = value; }
        public DateTime Pat_age { get => pat_age; set => pat_age = value; }
        public string Pat_telnum { get => pat_telnum; set => pat_telnum = value; }
        public string Pat_id { get => pat_id; set => pat_id = value; }
        public int Is_married { get => is_married; set => is_married = value; }
        public string Pat_address { get => pat_address; set => pat_address = value; }
        public string Pat_pre_his { get => pat_pre_his; set => pat_pre_his = value; }
        public string Pat_allergen { get => pat_allergen; set => pat_allergen = value; }
        public string Pat_census { get => pat_census; set => pat_census = value; }
        public int Pat_height { get => pat_height; set => pat_height = value; }
        public int Pat_weight { get => pat_weight; set => pat_weight = value; }
        public string Emer_name { get => emer_name; set => emer_name = value; }
        public DateTime Gmt_created { get => gmt_created; set => gmt_created = value; }
        public DateTime Gmt_modified { get => gmt_modified; set => gmt_modified = value; }
        public string Start { get => start; set => start = value; }
        public string End { get => end; set => end = value; }

        /// <summary>
        /// 患者信息的检索
        /// </summary>
        /// <returns></returns>
        public static DataTable P_search()
        {
            DataTable p_dt = new DataTable();
            string Comstr = string.Empty;
            p_dt = DbOS.dataSet(Comstr).Tables[0];
            return p_dt;
        }
        /// <summary>
        /// 患者诊疗卡申请
        /// </summary>
        public void P_insert(string Comstr)
        {
            try
            {
                DbOS.GetSqlcom(Comstr);
            }
            catch(Exception ex)
            {
                MBox.Warn("申请错误！");
            }
        }
        /// <summary>
        /// 传染病
        /// </summary>
        /// <param name="Comstr"></param>
        public static DataTable inf_search()
        {
            DataTable inf_dt = new DataTable();
            string comstr = @"SELECT * FROM inf;";
            inf_dt = DbOS.dataSet(comstr).Tables[0];
            return inf_dt;
        }
    }
}
