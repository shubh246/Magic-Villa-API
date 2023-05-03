using System.ComponentModel.DataAnnotations;

namespace Magic_Villa.Models
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public String Name { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public String Details { get; set; }
        public Double Rate { get; set; }
        public String ImageUrl { get; set; }
        public String Amenity { get; set; }
    }
}
