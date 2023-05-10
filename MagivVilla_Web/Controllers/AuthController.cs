using MagicVilla_Utility;
using MagivVilla_Web.Models;
using MagivVilla_Web.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MagivVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        
        public AuthController(IAuthService _authService)
        {
            authService = _authService;

            
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto obj = new();

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public  async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ApiResponse response = await authService.LoginAsync<ApiResponse>(obj);
            if (response != null && response.IsSuccess)
            {
                LoginResponseDto model = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
                var handler = new JwtSecurityTokenHandler();
                var jwt=handler.ReadJwtToken(model.Token);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == "unique_name").Value));
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u=>u.Type=="role").Value));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                HttpContext.Session.SetString(SD.SessionToken, model.Token);
                return RedirectToAction("Index","Home");

            }
            else
            {
                ModelState.AddModelError("Custom Error",response.ErrorMessage.FirstOrDefault());
                return View(obj);

            }


            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            RegistrationRequestDto obj = new();

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {

            ApiResponse result=await authService.RegisterAsync<ApiResponse>(obj);
            if (result != null && result.IsSuccess)
            {
                
               return RedirectToAction(nameof(Login));

            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> AccessDenied()
        {


            return View();
        }

    }
}
