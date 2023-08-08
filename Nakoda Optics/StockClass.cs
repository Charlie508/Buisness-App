using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nakoda_Optics
{
    public class StockClass
    {
        public string Compony { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }


        [PrimaryKey]

        public string Id { get; set; }


        public string Quantity { get; set; }
    }
}
