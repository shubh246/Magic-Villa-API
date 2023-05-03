using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagivVilla_Web.Models.VM
{
    public class VillaNumberDeleteVM
    {
        public VillaNumberDeleteVM() 
        { 
            VillaNumber=new VillaNumberDTO();
        }
        public VillaNumberDTO VillaNumber { get; set; }
        public string VillaNumberName { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> villaList { get; set; }
    }
}
