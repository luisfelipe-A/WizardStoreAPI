using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WizStore.Entities
{
    public class User(string? name, string username, string passwordHash, Role role)
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? Name { get; set; } = name;
        public string Username { get; set; } = username;
        public Role Role { get; set; } = role;

        [JsonIgnore]
        public string PasswordHash { get; set; } = passwordHash;
    }
}
