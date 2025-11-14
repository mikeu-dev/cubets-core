using cubets_core.Helpers;
using cubets_core.Modules.Auth.DTOs;
using cubets_core.Modules.Auth.Models;
using CubetsCore.Modules.Auth.DTOs;
using CubetsCore.Modules.Auth.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace cubets_core.Modules.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly Data.CubetsDbContext _dbContext;
        private readonly JwtService _jwt;

        public AuthService(Data.CubetsDbContext dbContext, JwtService jwt)
        {
            _dbContext = dbContext;
            _jwt = jwt;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Username == dto.Username))
                throw new Exception("Username sudah digunakan.");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = HashPassword(dto.Password),
                IsGuest = false
            };

            var player = new Player.Models.Player
            {
                Nickname = dto.Username,
                Level = 1,
                Coins = 0
            };

            user.Player = player;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var token = _jwt.GenerateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                PlayerId = player.Id
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _dbContext.Users
                .Include(u => u.Player)
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                throw new Exception("Username atau password salah.");

            var token = _jwt.GenerateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                PlayerId = user.Player?.Id
            };
        }

        public async Task<AuthResponseDto> GuestLoginAsync()
        {
            var guestUser = new User
            {
                Username = "Guest_" + Guid.NewGuid().ToString("N").Substring(0, 8),
                PasswordHash = "",
                IsGuest = true
            };

            var player = new Player.Models.Player
            {
                Nickname = guestUser.Username,
                Level = 1,
                Coins = 0
            };

            guestUser.Player = player;
            _dbContext.Users.Add(guestUser);
            await _dbContext.SaveChangesAsync();

            var token = _jwt.GenerateToken(guestUser);
            return new AuthResponseDto
            {
                Token = token,
                Username = guestUser.Username,
                PlayerId = player.Id
            };
        }

        public async Task<LogoutResponseDto> LogoutAsync(RevokedTokenRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Token))
                throw new Exception("Token diperlukan!");

            _dbContext.RevokedTokens.Add(new RevokedToken
            {
                Token = dto.Token,
                RevokedAt = DateTime.UtcNow
            });

            await _dbContext.SaveChangesAsync();

            return new LogoutResponseDto
            {
                Success = true,
                Message = "Logout berhasil."
            };
        }


        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
