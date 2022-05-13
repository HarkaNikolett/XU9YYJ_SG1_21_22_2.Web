using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XU9YYJ_HFT_2021221.Models.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
       
        public int UnitPrice { get; set; }

        [Required]
        public string Currency { get; set; }

        [MaxLength(30)]
        public string Note { get; set; }

        
        public DateTime Date { get; set; }

        public string SupplierName { get; set; } //public int SupplierId {get; set;}
     
        public int SupplierId{get; set;}
        [NotMapped]
        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Item Item { get; set; }
        



    }
}
