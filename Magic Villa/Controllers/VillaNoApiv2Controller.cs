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
    [Route("api/v{version:apiVersion}/VillaNumberApi")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNoApiv2Controller : ControllerBase
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
        public VillaNoApiv2Controller(IVillaNoRepository _dbvillaNo, IMapper _mapper, IVillaRepository _dbvilla)
        {
            dbvillaNo = _dbvillaNo;
            dbvilla = _dbvilla;
            mapper = _mapper;
            this.response = new();
        }
        
        [HttpGet]
        //[MapToApiVersion("2.0")]
        public IEnumerable<string> get()
        {
            return new string[] { "value1", "value2" };
        }
        
        
    }
}
