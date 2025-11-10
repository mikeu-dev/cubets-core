namespace cubets_core.Modules.Auth.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int HighScore { get; set; }

        public int LowScore { get; set; }
    }
}
