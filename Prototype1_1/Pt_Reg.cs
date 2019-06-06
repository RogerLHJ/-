using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Prototype1_1
{
    /// <summary>
    /// 挂号信息表
    /// </summary>
    class Pt_Reg
    {
        private int id;
        private int doc_id;
        private int card_id;
        private int reg_charge;
        private int is_cancel;
        private DateTime gmt_created;
        private DateTime gmt_modified;

        public int Id { get => id; set => id = value; }
        public int Doc_id { get => doc_id; set => doc_id = value; }
        public int Card_id { get => card_id; set => card_id = value; }
        public int Reg_charge { get => reg_charge; set => reg_charge = value; }
        public int Is_cancel { get => is_cancel; set => is_cancel = value; }
        public DateTime Gmt_created { get => gmt_created; set => gmt_created = value; }
        public DateTime Gmt_modified { get => gmt_modified; set => gmt_modified = value; }
        /// <summary>
        /// 病案号搜索病人信息
        /// </summary>
        /// <returns></returns>
        public static DataTable p_search()
        {
            DataTable pdt = new DataTable();
            string comstr = string.Empty;
            pdt =  DbOS.dataSet(comstr).Tables[0];
            return pdt;
        }
        /// <summary>
        /// 挂号信息
        /// </summary>
        public static void p_insert()
        {
            string Comstr = string.Empty;
            try
            {
                DbOS.GetSqlcom(Comstr);
            }
            catch (Exception ex)
            {
                MBox.Warn("申请错误！");
            }
        }
    }
}
