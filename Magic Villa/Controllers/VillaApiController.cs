using Magic_Villa.Data;
using Magic_Villa.Logging;
using Magic_Villa.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Magic_Villa.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogging logger;

        public VillaApiController(ILogging _logger)
        {
            logger = _logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVilla()
        {
            return Ok(VillaData.villaList);
            /*return new List<VillaDto>{
            new VillaDto{Id=1,Name="Shubham"},
           new VillaDto{Id=2,Name="Shubham23"}

        };*/
        }
        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            logger.Log("Geetting all villas","");
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaData.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            if (VillaData.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Villa Already Exists");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest();
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = VillaData.villaList.OrderByDescending(u => u.Id).First().Id + 1;
            VillaData.villaList.Add(villaDto);
            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaData.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaData.villaList.Remove(villa);
            return NoContent();
        }
        [HttpPut("{id:int}",Name="UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDto villaDto) {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }
            var villa = VillaData.villaList.FirstOrDefault(v => v.Id == id);
            villa.Name=villaDto.Name;   
            villa.Sqft = villaDto.Sqft; 
            villa.Occupancy= villaDto.Occupancy;
            return NoContent();


        }
        [HttpPatch("{id:int}",Name="UpdateVillaPart")]
        public IActionResult UpdatePartialVilla(int id,JsonPatchDocument<VillaDto> patchDto) {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa=VillaData.villaList.FirstOrDefault(u=>u.Id== id); 
            if(villa == null)
            {
                return NotFound();
            }
            patchDto.ApplyTo(villa, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);    
            }
            return NoContent();
        }
    }
}
