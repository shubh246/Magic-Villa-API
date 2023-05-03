using System.ComponentModel.DataAnnotations;

namespace MagivVilla_Web.Models
{
    public class VillaNumberDTO
    {
        public int VillaNo { get; set; }
        public String SpecialDetails { get; set; }
        [Required]
        public int Villaid { get; set; }
        public VillaDto Villa { get; set; }
    }
    
}
