using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU9YYJ_HFT_2021221.Models.Models
{
    public class QtyOfItems
    {
        public int SupplierId { get; set; }
        public int Qty { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as QtyOfItems;

            if (other == null)
                return false;
            else
            {
                return other.SupplierId == SupplierId && other.Qty == Qty;
            }
        }
        public override string ToString()
        {
            return $"{SupplierId} - {Qty}";
        }
    }
}
