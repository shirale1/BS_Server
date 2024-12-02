using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BS_Server.DTO
{
    public class LoginInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
