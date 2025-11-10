namespace cubets_core.Modules.Score.Models
{
    public class ScoreEntry
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }

        public int Score { get; set; }

        public DateTime AchievedAt { get; set; }
    }
}
