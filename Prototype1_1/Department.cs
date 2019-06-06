using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Prototype1_1
{
    class Department
    {
        private int id;
        private string dep_name;
        private DateTime gmt_created;
        private DateTime gmt_modified;

        public int Id { get => id; set => id = value; }
        public string Dep_name { get => dep_name; set => dep_name = value; }
        public DateTime Gmt_created { get => gmt_created; set => gmt_created = value; }
        public DateTime Gmt_modified { get => gmt_modified; set => gmt_modified = value; }
        /// <summary>
        /// 部门的检索
        /// </summary>
        /// <returns></returns>
        public static DataTable Dep_search()
        {
            DataTable De_dt = new DataTable();
            string comstr = @"select * from department;";
            De_dt = DbOS.dataSet(comstr).Tables[0];
            return De_dt;
        }
    }
}
