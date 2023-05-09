namespace Magic_Villa.Models
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        //public string Role { get; set; }    
        public string Token { get; set; }   
    }
}
