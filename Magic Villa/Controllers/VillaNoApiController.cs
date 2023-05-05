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
using System.Data;
using System.Net;

namespace Magic_Villa.Controllers
{
    [Route("api/VillaNumberApi")]
    [ApiController]
    public class VillaNoApiController : ControllerBase
    {
        //private readonly ILogging logger;

        /* public VillaApiController(ILogging _logger)
         {
             logger = _logger;
         }*/
        //private readonly ApplicationDbContext db;
        private readonly IVillaNoRepository dbvillaNo;
        private readonly IVillaRepository dbvilla;
        private readonly IMapper mapper;
        protected readonly ApiResponse response;
         public VillaNoApiController(IVillaNoRepository _dbvillaNo,IMapper _mapper, IVillaRepository _dbvilla)
         {
            dbvillaNo = _dbvillaNo;
            dbvilla = _dbvilla; 
            mapper = _mapper;
            this.response = new();
         }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> VillaList = await dbvillaNo.GetAllAsync(includeProperties:"Villa");
                IEnumerable<VillaNumberDTO> VillaDtoList = mapper.Map<IEnumerable<VillaNumberDTO>>(VillaList);
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
        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetVillaNumber(int id)
        {
            try
            {
                //logger.Log("Geetting all villas","");
                if (id == 0)
                {
                    return BadRequest();
                }

                var villaNo = await dbvillaNo.GetAsync(u => u.VillaNo == id);
                if (villaNo == null)
                {
                    return NotFound();
                }
                response.Result = mapper.Map<VillaNumberDTO>(villaNo);
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
        public async Task<ActionResult<ApiResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDto createDto)
        {
            try
            {
                if (await dbvillaNo.GetAsync(u => u.VillaNo == createDto.VillaNo) != null) 
                {
                    ModelState.AddModelError("ErrorMessage", "VillaNumber Already Exists");
                    return BadRequest(ModelState);
                }
                if(await dbvilla.GetAsync(u=>u.Id==createDto.Villaid)==null) {
                    ModelState.AddModelError("ErrorMessage", "VillaId Invalid");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                VillaNumber villaNo = mapper.Map<VillaNumber>(createDto);
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
                await dbvillaNo.CreateAsync(villaNo);
                response.Result = mapper.Map<VillaNumberDTO>(villaNo);
                response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetVilla", new { id = villaNo.VillaNo }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return response;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<ApiResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villaNo = await dbvillaNo.GetAsync(v => v.VillaNo == id);
                if (villaNo == null)
                {
                    return NotFound();
                }
                await dbvillaNo.RemoveAsync(villaNo);
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
        [HttpPut("{id:int}",Name="UpdateVillaNumber")]
        public async Task<ActionResult<ApiResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDto updateDto) {
            try
            {
                if (updateDto == null || id != updateDto.VillaNo)
                {
                    return BadRequest();
                }
                if (await dbvilla.GetAsync(u => u.Id == updateDto.Villaid) == null)
                {
                    ModelState.AddModelError("ErrorMessage", "VillaId Already Exists");
                    return BadRequest(ModelState);
                }
                var villa = await dbvillaNo.GetAsync(v => v.VillaNo == id, tracked: true);
                /*villa.Name=villaDto.Name;   
                villa.Sqft = villaDto.Sqft; 
                villa.Occupancy= villaDto.Occupancy;*/
                VillaNumber villaNo = mapper.Map<VillaNumber>(updateDto);
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
                await dbvillaNo.UpdateAsync(villaNo);
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
        
    }
}
