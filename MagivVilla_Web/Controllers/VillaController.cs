using AutoMapper;
using MagivVilla_Web.Models;
using MagivVilla_Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace MagivVilla_Web.Controllers
{
    public class VillaController:Controller
    {
        private readonly IVillaService villaService;
        private readonly IMapper mapper;
        public VillaController(IVillaService _villaService, IMapper _mapper)
        {
            villaService = _villaService;
            
            mapper = _mapper;
        }
        public async Task<IActionResult> GetIndexVilla()
        {
            List<VillaDto> list = new();
            var response = await villaService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list=JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));

            }
            return View(list);

        }
        public async Task<IActionResult> CreateVilla()
        {
            
            return View();

        }
        [HttpPost]
        
        public async Task<IActionResult> CreateVilla(VillaCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await villaService.CreateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Created Successfully";
                    return RedirectToAction(nameof(GetIndexVilla));

                }

            }
            //TempData["error"] = "Error Encountered";
            return View(model);

        }
        public async Task<IActionResult> UpdateVilla(int Villaid)
        {
            var response = await villaService.GetAsync<ApiResponse>(Villaid);
            if (response != null && response.IsSuccess)
            {
              VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
                return View(mapper.Map<VillaUpdateDto>(model));

            }
            return NotFound();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await villaService.UpdateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                    TempData["success"] = "Villa Updated Successfully";
                {
                    return RedirectToAction(nameof(GetIndexVilla));

                }

            }
            TempData["error"] = "Error Encountered";
            return View(model);
        }
        
        public async Task<IActionResult> DeleteVilla(int Villaid)
        {
            var response = await villaService.GetAsync<ApiResponse>(Villaid);
            if (response != null && response.IsSuccess)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
                //return View(mapper.Map<VillaUpdateDto>(model));
                return View(model);

            }
            return NotFound();

        }


       [HttpPost]
        public async Task<IActionResult> DeleteVilla(VillaDto model)
        {
            
            
                var response = await villaService.DeleteAsync<ApiResponse>(model.Id);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Deleted Successfully";

                return RedirectToAction(nameof(GetIndexVilla));

                }

            TempData["error"] = "Error Encountered";
            return View(model);
        }
        /*public async Task<IActionResult> DeleteVillaDirect(int villaId)
        {
            var Delresp= await villaService.DeleteAsync<ApiResponse>(villaId);
            if (Delresp != null && Delresp.IsSuccess)
            {
                TempData["success"] = "Villa Deleted successfully";
                return RedirectToAction(nameof(GetIndexVilla));
            }
            TempData["error"] = "Error encountered.";
            var response = await villaService.GetAsync<ApiResponse>(villaId);
            VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
            return View(model);
        }*/
    }
}
    
