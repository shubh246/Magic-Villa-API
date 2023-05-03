using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagivVilla_Web.Models.VM
{
    public class VillaNumberUpdateVM
    {
        public VillaNumberUpdateVM() 
        { 
            VillaNumber=new VillaNumberUpdateDto();
        }
        public VillaNumberUpdateDto VillaNumber { get; set; }
        public string VillaNumberName { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> villaList { get; set; }
    }
}
