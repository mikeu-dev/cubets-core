using cubets_core.Modules.Score.DTOs;
using cubets_core.Modules.Score.Models;
using cubets_core.Modules.Score.Services;
using Microsoft.AspNetCore.Mvc;

namespace cubets_core.Modules.Score.Controllers
{
    public class ScoreController
    {
        private readonly ScoreService _scoreService;

        public ScoreController(ScoreService scoreService) => _scoreService = scoreService;

        [HttpGet("playerId")]
        public ActionResult<ScoreEntryDto> GetHighScore(int playerId)
        {
            var highScore = _scoreService.GetTopScoresAsync(playerId);
            if (highScore == null) return NotFound();
            return Ok(highScore);
        }
        private ActionResult<ScoreEntryDto> Ok(Task<List<ScoreEntry>> highScore)
        {
            throw new NotImplementedException();
        }
        private ActionResult<ScoreEntryDto> NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
