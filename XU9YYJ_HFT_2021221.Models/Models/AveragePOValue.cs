using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU9YYJ_HFT_2021221.Models.Models
{
    public class AveragePOValue
    {
        public string SupplierName { get; set; }
       // public string ProductName { get; set; }
        public double AverageValue { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as AveragePOValue;

            if (other == null)
                return false;

            return other.SupplierName == SupplierName && other.AverageValue == AverageValue;
        }

        public override string ToString()
        {
            return $"{SupplierName} - {AverageValue}";
        }
    }
}
