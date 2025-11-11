
namespace cubets_core.Modules.Auth.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsGuest { get; set; } = false;

        // relasi satu-ke-satu (tanpa FK di sini)
        public Player.Models.Player? Player { get; set; }
    }
}
