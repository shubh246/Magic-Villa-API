using System.ComponentModel.DataAnnotations;

namespace Magic_Villa.Models
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public String? Name { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public int Occupancy { get; set; }
        public String Details { get; set; }
        public Double Rate { get; set; }
        [Required]
        public String ImageUrl { get; set; }
        public String Amenity { get; set; }
    }
}
