namespace cubets_core.Modules.Score.DTOs
{
    public class ScoreEntryDto
    {
        public int PlayerId { get; set; }
        public int Score { get; set; }
        public DateTime AchievedAt { get; set; }
    }
}
