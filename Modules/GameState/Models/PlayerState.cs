
namespace cubets_core.Modules.GameState.Models
{
    public class PlayerState
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }

        public Player.Models.Player Player { get; set; } = null!;

        public float X { get; set; }

        public float Y { get; set; }

        public int Lives { get; set; }

        public int Score { get; set; }
    }
}
