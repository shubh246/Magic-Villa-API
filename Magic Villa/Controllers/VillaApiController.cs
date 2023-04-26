using AutoMapper;
using Magic_Villa.Data;
using Magic_Villa.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Magic_Villa.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        //private readonly ILogging logger;

        /* public VillaApiController(ILogging _logger)
         {
             logger = _logger;
         }*/
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
         public VillaApiController(ApplicationDbContext _db,IMapper _mapper)
         {
            db = _db;
            mapper = _mapper;
         }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVilla()
        {
            IEnumerable<Villa> VillaList = await db.Villas.ToListAsync();
            IEnumerable<VillaDto> VillaDtoList = mapper.Map<IEnumerable<VillaDto>>(VillaList);
            return Ok(VillaDtoList);
            // return Ok( await db.Villas.ToListAsync());
            /*return new List<VillaDto>{
            new VillaDto{Id=1,Name="Shubham"},
           new VillaDto{Id=2,Name="Shubham23"}

        };*/
        }
        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            //logger.Log("Geetting all villas","");
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<VillaDto>(villa));

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto CreateDto)
        {
            if (await db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == CreateDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Villa Already Exists");
                return BadRequest(ModelState);
            }
            if (CreateDto == null)
            {
                return BadRequest(CreateDto);
            }
            Villa model = mapper.Map<Villa>(CreateDto);
            //if (villaDto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            //Villa model = new()
            //{
            //    Amenity = villaDto.Amenity, 
            //    Details = villaDto.Details,
            //    //Id=villaDto.Id,
            //    ImageUrl=villaDto.ImageUrl,
            //    Name=villaDto.Name,
            //    Occupancy=villaDto.Occupancy,
            //    Rate=villaDto.Rate,
            //    Sqft=villaDto.Sqft,
            //};
           await db.Villas.AddAsync(model);
            db.SaveChanges();

            
            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
             db.Villas.Remove(villa);    
            await db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}",Name="UpdateVilla")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto) {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }
            var villa =await  db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
            /*villa.Name=villaDto.Name;   
            villa.Sqft = villaDto.Sqft; 
            villa.Occupancy= villaDto.Occupancy;*/
            Villa model = mapper.Map<Villa>(updateDto);
            //Villa model = new()
            //{
            //    Amenity = villaDto.Amenity,
            //    Details = villaDto.Details,
            //    Id = villaDto.Id,
            //    ImageUrl = villaDto.ImageUrl,
            //    Name = villaDto.Name,
            //    Occupancy = villaDto.Occupancy,
            //    Rate = villaDto.Rate,
            //    Sqft = villaDto.Sqft,
            //};
            db.Villas.Update(model);
            await db.SaveChangesAsync();
            return NoContent();


        }
        [HttpPatch("{id:int}",Name="UpdateVillaPart")]
        public async Task<IActionResult> UpdatePartialVilla(int id,JsonPatchDocument<VillaUpdateDto> patchDto) {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa=await db.Villas.AsNoTracking().FirstOrDefaultAsync(u=>u.Id== id); 
            if(villa == null)
            {
                return NotFound();
            }
            VillaUpdateDto villaDto = mapper.Map<VillaUpdateDto>(villa);
            //VillaUpdateDto villaDto = new()
            //{
            //    Amenity = villa.Amenity,
            //    Details = villa.Details,
            //    Id = villa.Id,
            //    ImageUrl = villa.ImageUrl,
            //    Name = villa.Name,
            //    Occupancy = villa.Occupancy,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft,
            //};
            patchDto.ApplyTo(villaDto,ModelState);
            Villa model = mapper.Map<Villa>(villaDto);
            //Villa model = new()
            //{
            //    Amenity = villaDto.Amenity,
            //    Details = villaDto.Details,
            //    Id = villaDto.Id,
            //    ImageUrl = villaDto.ImageUrl,
            //    Name = villaDto.Name,
            //    Occupancy = villaDto.Occupancy,
            //    Rate = villaDto.Rate,
            //    Sqft = villaDto.Sqft,
            //};


            db.Villas.Update(model);
           await  db.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
