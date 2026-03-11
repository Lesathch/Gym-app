namespace DTOs
{
    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginResponse
    {
        public string Token { get; set; } = "";
        public string RefreshToken { get; set; } = "";

        public string Role { get; set;} = "";
        public string Name { get; set;} = "";
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = "";
    }
}