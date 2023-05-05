using Magic_Villa.Models;
using Magic_Villa.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Magic_Villa.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        protected ApiResponse response;
        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
            this.response = new();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await userRepository.Login(model);
            if (loginResponse.User == null||string.IsNullOrEmpty(loginResponse.Token)) { 
                response.StatusCode=HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessage.Add("Username pr password is incorrect");
                return BadRequest(response);


            
            
            }
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result=loginResponse;
            return Ok(response);

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequestDto model)
        {
            bool ifUserNameUnique=userRepository.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessage.Add("Username pr password is incorrect");
                return BadRequest(response);
            }
            var user=await userRepository.Register(model);
            if (user==null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessage.Add("Error while registering");
                return BadRequest(response);
            }
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
    }
}
