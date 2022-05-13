using System;

namespace XU9YYJ_HFT_2021221.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string Currency { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }

    }
}
