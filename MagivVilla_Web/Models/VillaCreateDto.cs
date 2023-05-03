using System.ComponentModel.DataAnnotations;

namespace MagivVilla_Web.Models
{
    public class VillaCreateDto
    {
        
        [Required]
        [MaxLength(255)]
        public String? Name { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public String Details { get; set; }

        public Double Rate { get; set; }
        public String ImageUrl { get; set; }
        public String Amenity { get; set; }
    }
}
