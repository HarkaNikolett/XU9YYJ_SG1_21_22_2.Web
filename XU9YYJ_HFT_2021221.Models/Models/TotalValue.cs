using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU9YYJ_HFT_2021221.Models.Models
{
    public class TotalValue
    {
        public string SupplierName { get; set; }
        public int Value { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as TotalValue;

            if (other == null)
                return false;

            return other.SupplierName == SupplierName && other.Value == Value;
        }

        public override string ToString()
        {
            return $"{SupplierName} - {Value}";
        }
    }
}
