namespace cubets_core.Modules.Auth.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int? PlayerId { get; set; }
    }
}
