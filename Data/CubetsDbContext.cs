using Microsoft.EntityFrameworkCore;
using cubets_core.Modules.Auth.Models;
using cubets_core.Modules.GameState.Models;
using cubets_core.Modules.Score.Models;

namespace cubets_core.Data
{
    public class CubetsDbContext : DbContext
    {
        public CubetsDbContext(DbContextOptions<CubetsDbContext> options) : base(options)
        {
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerState> PlayerStates { get; set; }
        public DbSet<ScoreEntry> ScoreEntries { get; set; }
    }
}
