using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Prototype1_1
{
    class Docotor
    {
        private int id;
        private string psw;
        private string doc_name;
        private string doc_sex;
        private string doc_position;
        private int br_dep_id;
        private string doc_eduction;
        private DateTime doc_age;
        private string doc_telephone;
        private string doc_address;
        private string doc_introduction;
        private DateTime gmt_created;
        private DateTime gmt_modified;

        public int Id { get => id; set => id = value; }
        public string Psw { get => psw; set => psw = value; }
        public string Doc_name { get => doc_name; set => doc_name = value; }
        public string Doc_sex { get => doc_sex; set => doc_sex = value; }
        public string Doc_position { get => doc_position; set => doc_position = value; }
        public int Br_dep_id { get => br_dep_id; set => br_dep_id = value; }
        public string Doc_eduction { get => doc_eduction; set => doc_eduction = value; }
        public DateTime Doc_age { get => doc_age; set => doc_age = value; }
        public string Doc_telephone { get => doc_telephone; set => doc_telephone = value; }
        public string Doc_address { get => doc_address; set => doc_address = value; }
        public string Doc_introduction { get => doc_introduction; set => doc_introduction = value; }
        public DateTime Gmt_created { get => gmt_created; set => gmt_created = value; }
        public DateTime Gmt_modified { get => gmt_modified; set => gmt_modified = value; }
        /// <summary>
        /// 医生检索
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static DataTable doc_search(string items)
        {
            DataTable doc_dt = new DataTable();
            string comstr = @"SELECT DISTINCT doc_name FROM arrangement WHERE br_dep_name = '{0}' AND res > 0;";
            comstr = string.Format(comstr, items);
            doc_dt = DbOS.dataSet(comstr).Tables[0];
            return doc_dt;
        }
        public static DataTable date_search(string items)
        {
            DataTable date_dt = new DataTable();
            string comstr = @"SELECT DISTINCT date FROM arrangement WHERE doc_name = '{0}' AND res>0;";
            comstr = string.Format(comstr, items);
            date_dt = DbOS.dataSet(comstr).Tables[0];
            return date_dt;
        }
        public static DataTable time_search(string items1,string items2)
        {
            DataTable time_dt = new DataTable();
            string comstr = @"SELECT DISTINCT time FROM arrangement WHERE date = '{0}' AND doc_name = '{1}' AND res>0;";
            comstr = string.Format(comstr, items1,items2);
            time_dt = DbOS.dataSet(comstr).Tables[0];
            return time_dt;
        }
        public static DataTable all_search()
        {
            DataTable all_dt = new DataTable();
            string comstr = @"SELECT all_med_name FROM allergy_med;";
            all_dt = DbOS.dataSet(comstr).Tables[0];
            return all_dt;
        }
        public static DataTable charge_search(string items)
        {
            DataTable charge_dt = new DataTable();
            string comstr = @"SELECT DISTINCT re_charge FROM arrangement WHERE doc_name = '{0}';";
            comstr = string.Format(comstr, items);
            charge_dt = DbOS.dataSet(comstr).Tables[0];
            return charge_dt;
        }
    }
}
