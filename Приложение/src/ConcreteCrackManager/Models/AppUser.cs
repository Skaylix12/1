
namespace ConcreteCrackManager.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? DisplayName { get; set; }

        public int? RoleId { get; set; }
        public AppRole? Role { get; set; }
    }
}
