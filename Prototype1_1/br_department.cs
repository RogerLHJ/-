using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype1_1
{
    class br_department
    {
        private int id;
        private string br_dep_name;
        private int dep_id;
        private DateTime gmt_created;
        private DateTime gmt_modified;

        public int Id { get => id; set => id = value; }
        public string Br_dep_name { get => br_dep_name; set => br_dep_name = value; }
        public int Dep_id { get => dep_id; set => dep_id = value; }
        public DateTime Gmt_created { get => gmt_created; set => gmt_created = value; }
        public DateTime Gmt_modified { get => gmt_modified; set => gmt_modified = value; }
        public static DataTable Br_dep_search(string item)
        {
            DataTable br_de_dt = new DataTable();
            string comstr = @"SELECT DISTINCT br_dep_name FROM arrangement WHERE dep_name = '{0}' AND res > 0;";
            comstr = string.Format(comstr, item);
            br_de_dt = DbOS.dataSet(comstr).Tables[0];
            return br_de_dt;
        }
    }
}
