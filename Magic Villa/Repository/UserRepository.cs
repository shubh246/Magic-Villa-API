using AutoMapper;
using Magic_Villa.Data;
using Magic_Villa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Magic_Villa.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public string secretKey;
        private readonly IMapper mapper;
        public UserRepository(ApplicationDbContext _db,IConfiguration configuration, UserManager<ApplicationUser> _userManager, IMapper _mapper, RoleManager<IdentityRole> _roleManager) 
        {
            db = _db;
            secretKey = configuration.GetValue<String>("ApiSettings:Secret");
            userManager = _userManager;
            roleManager = _roleManager;
            mapper = _mapper;


        }

        public bool IsUniqueUser(string username)
        {
            var user=db.ApplicationUsers.FirstOrDefault(x=>x.UserName== username);
            if (user == null)
            {
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = db.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool IsValid=await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if(user == null||IsValid==false) {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }
            var roles = await userManager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserName.ToString()),
                    new Claim(ClaimTypes.Role,roles.FirstOrDefault())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(TokenDescriptor);
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = mapper.Map<UserDto>(user)
                //Role = roles.FirstOrDefault()
            };



            return loginResponseDto;
        }

        public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registrationRequestDto.UserName,
                Email = registrationRequestDto.UserName,
                NormalizedEmail = registrationRequestDto.UserName.ToUpper(),
                //Password = registrationRequestDto.Password,
                Name = registrationRequestDto.Name
                //Role = registrationRequestDto.Role

            };
            try
            {
                var result = await userManager.CreateAsync(user, registrationRequestDto.Password);
                if(result.Succeeded)
                {
                    if(!roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await roleManager.CreateAsync(new IdentityRole("admin"));
                        await roleManager.CreateAsync(new IdentityRole("customer"));
                    }
                    await userManager.AddToRoleAsync(user, "admin");
                    var userToReturn = db.ApplicationUsers.FirstOrDefault(u => u.UserName == registrationRequestDto.UserName);
                    return mapper.Map<UserDto>(userToReturn);

                }
            }
            catch(Exception e)
            {

            }
            return new UserDto();



        }
    }
}
