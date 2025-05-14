using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    public class Farmer
    {
        [Key]
        [Required]
        public int FarmerID { get; set; }
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
