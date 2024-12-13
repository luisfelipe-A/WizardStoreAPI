using System.Text.Json.Serialization;

namespace WizStore.Entities
{
    public class User(int userId, string? name, string username, string passwordHash, Role role)
    {
        public int UserId { get; set; } = userId;
        public string? Name { get; set; } = name;
        public string Username { get; set; } = username;
        public Role Role { get; set; } = role;

        [JsonIgnore]
        public string PasswordHash { get; set; } = passwordHash;
    }
}
