using AutoMapper;
using MagivVilla_Web.Models;
using MagivVilla_Web.Models.VM;
using MagivVilla_Web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace MagivVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService villaNumberService;
        private readonly IVillaService villaService;
        private readonly IMapper mapper;
        public VillaNumberController(IVillaNumberService _villaNumberService, IMapper _mapper, IVillaService _villaService)
        {
            villaNumberService = _villaNumberService;

            mapper = _mapper;
            villaService = _villaService;
        }
        public async Task<IActionResult> GetIndexVillaNumber()
        {
            List<VillaNumberDTO> list = new();
            var response = await villaNumberService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));

            }
            return View(list);

        }
        public async Task<IActionResult> CreateVillaNumber()
        {   VillaNumberCreateVM villaNumberVM = new VillaNumberCreateVM();
            var response = await villaService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result)).Select(
                    i=>new SelectListItem
                    {
                        Text=i.Name,
                        Value=i.Id.ToString()

                    }); ;

            }

            return View(villaNumberVM);

        }
        [HttpPost]

        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await villaNumberService.CreateAsync<ApiResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(GetIndexVillaNumber));

                }
                else
                {
                    if (response.ErrorMessage.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessage", response.ErrorMessage.FirstOrDefault());
                    }
                }

            }
            
            var resp = await villaService.GetAllAsync<ApiResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(resp.Result)).Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()

                    }); ;

            }

            
            return View(model);

        }
        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            VillaNumberUpdateVM villaNumberVM = new VillaNumberUpdateVM();
            var response = await villaNumberService.GetAsync<ApiResponse>(villaNo);
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber=mapper.Map<VillaNumberUpdateDto>(model);

            }
            response = await villaService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result)).Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()

                    }); ;
                return View(villaNumberVM);
            }
            return NotFound();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await villaNumberService.UpdateAsync<ApiResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(GetIndexVillaNumber));

                }
                else
                {
                    if (response.ErrorMessage.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessage", response.ErrorMessage.FirstOrDefault());
                    }
                }

            }

            var resp = await villaService.GetAllAsync<ApiResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(resp.Result)).Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()

                    }); ;

            }


            return View(model);
        }
        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            VillaNumberDeleteVM villaNumberVM = new VillaNumberDeleteVM();
            var response = await villaNumberService.GetAsync<ApiResponse>(villaNo);
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber = model;

            }
            response = await villaService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result)).Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()

                    }); ;
                return View(villaNumberVM);
            }
            return NotFound();

        }


        [HttpPost]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
        {


            var response = await villaNumberService.DeleteAsync<ApiResponse>(model.VillaNumber.VillaNo);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa Deleted Successfully";

                return RedirectToAction(nameof(GetIndexVillaNumber));

            }

            TempData["error"] = "Error Encountered";
            return View(model);
        }
    }
}
