using Microsoft.EntityFrameworkCore;
using cubets_core.Modules.GameState.Models;
using cubets_core.Modules.Score.Models;
using cubets_core.Modules.Player.Models;
using cubets_core.Modules.Auth.Models;
using CubetsCore.Modules.Auth.Models;

namespace cubets_core.Data
{
    public class CubetsDbContext : DbContext
    {
        public CubetsDbContext(DbContextOptions<CubetsDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<RevokedToken> RevokedTokens { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerState> PlayerStates { get; set; }
        public DbSet<ScoreEntry> ScoreEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // konfigurasi relasi atau constraint khusus di sini
            modelBuilder.Entity<User>()
                .HasOne(u => u.Player)
                .WithOne(p => p.User)
                .HasForeignKey<Player>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
