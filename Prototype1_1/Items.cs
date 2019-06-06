using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype1_1
{
    class Items
    {
        private int id;
        private string item_name;
        private string item_address;
        private int item_price;
        private DateTime gmt_created;
        private DateTime gmt_modified;

        public int Id { get => id; set => id = value; }
        public string Item_name { get => item_name; set => item_name = value; }
        public string Item_address { get => item_address; set => item_address = value; }
        public int Item_price { get => item_price; set => item_price = value; }
        public DateTime Gmt_created { get => gmt_created; set => gmt_created = value; }
        public DateTime Gmt_modified { get => gmt_modified; set => gmt_modified = value; }
    }
}
