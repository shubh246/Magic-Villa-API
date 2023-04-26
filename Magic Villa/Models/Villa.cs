using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic_Villa.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String? Name { get; set; }
        public String Details { get; set; }
        public Double Rate { get; set; }
        public String ImageUrl { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public String Amenity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
    }
}
