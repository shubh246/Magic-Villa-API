using AutoMapper;
using Magic_Villa.Data;
using Magic_Villa.Models;
using Magic_Villa.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Magic_Villa.Controllers
{
    [Route("api/v{version:apiVersion}/VillaApi")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaApiController : ControllerBase
    {
        //private readonly ILogging logger;

        /* public VillaApiController(ILogging _logger)
         {
             logger = _logger;
         }*/
        //private readonly ApplicationDbContext db;
        private readonly IVillaRepository dbvilla;
        private readonly IMapper mapper;
        protected readonly ApiResponse response;
         public VillaApiController(IVillaRepository _dbvilla,IMapper _mapper)
         {
            dbvilla = _dbvilla;
            mapper = _mapper;
            this.response = new();
         }
        [HttpGet]
        [ResponseCache(CacheProfileName="Default30")]
        
        public async Task<ActionResult<ApiResponse>> GetVilla([FromQuery] int? occupancy, [FromQuery]string? search, int pagesize = 0, int pagenumber = 1)
        {
            try
            {
                IEnumerable<Villa> VillaList;
                if (occupancy > 0)
                {
                    VillaList = await dbvilla.GetAllAsync(u => u.Occupancy == occupancy,pagesize:pagesize,pagenumber:pagenumber);
                }
                else {
                    VillaList = await dbvilla.GetAllAsync(); }
                if (!string.IsNullOrEmpty(search))
                {
                    VillaList = VillaList.Where(u=>u.Name.ToLower().Contains(search));
                }
                Pagination pagination = new() { PageNumber = pagenumber, PageSize = pagesize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                IEnumerable<VillaDto> VillaDtoList = mapper.Map<IEnumerable<VillaDto>>(VillaList);
                response.Result = VillaDtoList;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            // return Ok( await db.Villas.ToListAsync());
            /*return new List<VillaDto>{
            new VillaDto{Id=1,Name="Shubham"},
           new VillaDto{Id=2,Name="Shubham23"}

        };*/
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        
        [HttpGet("{id:int}")]
        [ResponseCache(Duration = 30)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetVilla(int id)
        {
            try
            {
                //logger.Log("Geetting all villas","");
                if (id == 0)
                {
                    return BadRequest();
                }

                var villa = await dbvilla.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                response.Result = mapper.Map<VillaDto>(villa);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateVilla([FromBody] VillaCreateDto CreateDto)
        {
            try
            {
                if (await dbvilla.GetAsync(u => u.Name.ToLower() == CreateDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessage", "Villa Already Exists");
                    return BadRequest(ModelState);
                }
                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                Villa villa = mapper.Map<Villa>(CreateDto);
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
                //await db.Villas.AddAsync(model);
                // db.SaveChanges();
                await dbvilla.CreateAsync(villa);
                response.Result = mapper.Map<VillaDto>(villa);
                response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetVilla", new { id = villa.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<ActionResult<ApiResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await dbvilla.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await dbvilla.RemoveAsync(villa);
                //response.Result = mapper.Map<VillaDto>(villa);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}",Name="UpdateVilla")]
        public async Task<ActionResult<ApiResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto) {
            try
             {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }
                //var villa = await dbvilla.GetAsync(v => v.Id == id, tracked: false);
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
                await dbvilla.UpdateAsync(model);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;


        }
        [Authorize(Roles = "admin")]
        [HttpPatch("{id:int}",Name="UpdateVillaPart")]
        public async Task<ActionResult<ApiResponse>> UpdatePartialVilla(int id,JsonPatchDocument<VillaUpdateDto> patchDto) {
            try
            {
                if (patchDto == null || id == 0)
                {
                    return BadRequest();
                }
                var villa = await dbvilla.GetAsync(u => u.Id == id, tracked: false);
                if (villa == null)
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
                patchDto.ApplyTo(villaDto, ModelState);
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

                await dbvilla.UpdateAsync(model);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
    }
}
