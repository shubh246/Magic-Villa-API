using AutoMapper;
using MagicVilla_Utility;
using MagivVilla_Web.Models;
using MagivVilla_Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagivVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService villaService;
        private readonly IMapper mapper;
        public HomeController(IVillaService _villaService, IMapper _mapper)
        {
            villaService = _villaService;

            mapper = _mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<VillaDto> list = new();
            var response = await villaService.GetAllAsync<ApiResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));

            }
            return View(list);

        }
        

        

        
    }
}