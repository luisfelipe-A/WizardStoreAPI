using WizStore.Entities;

namespace WizStore.Models.Users
{
    public class AuthenticateResponse
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            UserId = user.UserId;
            Name = user.Name;
            Username = user.Username;
            Role = user.Role;
            Token = token;
        }
    }
}
