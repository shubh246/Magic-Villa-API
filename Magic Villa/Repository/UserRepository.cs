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
        public string secretKey;
        public UserRepository(ApplicationDbContext _db,IConfiguration configuration) 
        {
            db = _db;
            secretKey = configuration.GetValue<String>("ApiSettings:Secret");



        }

        public bool IsUniqueUser(string username)
        {
            var user=db.LocalUsers.FirstOrDefault(x=>x.UserName== username);
            if (user == null)
            {
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = db.LocalUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower()&&
            x.Password== loginRequestDto.Password);
            if(user == null) {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(TokenDescriptor);
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };



            return loginResponseDto;
        }

        public async Task<LocalUser> Register(RegistrationRequestDto registrationRequestDto)
        {
            LocalUser user = new LocalUser()
            {
                UserName = registrationRequestDto.UserName,
                Password = registrationRequestDto.Password,
                Name = registrationRequestDto.Name,
                Role = registrationRequestDto.Role

            };
            db.LocalUsers.Add(user);
            await db.SaveChangesAsync();
            user.Password="";
            return user;



        }
    }
}
