using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [ForeignKey("Farmer")]
        public int FarmerID { get; set; }
        public Farmer Farmer { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Required]
        [MaxLength(200)]   
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
