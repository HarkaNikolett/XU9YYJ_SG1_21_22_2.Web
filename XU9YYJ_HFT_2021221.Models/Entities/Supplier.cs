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
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength (30)]
        public string  Name { get; set; }

        [Required]
        public int VATNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string Address { get; set; }

        
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }


    }
}
