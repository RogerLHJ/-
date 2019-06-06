using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype1_1
{
    class Medicine
    {
        private int id;
        private string med_name;
        private string med_unit;
        private double med_price;
        private string med_effect;
        private string med_usage;
        private int surplus;
        private DateTime med_date;
        private string med_manu;
        private DateTime gmt_created;
        private DateTime gmt_modified;

        public int Id { get => id; set => id = value; }
        public string Med_name { get => med_name; set => med_name = value; }
        public string Med_unit { get => med_unit; set => med_unit = value; }
        public double Med_price { get => med_price; set => med_price = value; }
        public string Med_effect { get => med_effect; set => med_effect = value; }
        public string Med_usage { get => med_usage; set => med_usage = value; }
        public int Surplus { get => surplus; set => surplus = value; }
        public DateTime Med_date { get => med_date; set => med_date = value; }
        public string Med_manu { get => med_manu; set => med_manu = value; }
        public DateTime Gmt_created { get => gmt_created; set => gmt_created = value; }
        public DateTime Gmt_modified { get => gmt_modified; set => gmt_modified = value; }
    }
}
