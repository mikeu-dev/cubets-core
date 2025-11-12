using cubets_core.Common;
using cubets_core.Modules.Score.Services;
using Microsoft.AspNetCore.Mvc;

namespace cubets_core.Modules.Score.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController: ControllerBase
    {
        private readonly IResponseService _response;
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService, IResponseService response) 
        {
            _response = response;
            _scoreService = scoreService; 
        }

        [HttpGet("playerId")]
        public async Task<IActionResult> GetTopScores(int playerId)
        {
            var result = await _scoreService.GetTopScoresAsync(playerId);
            return Ok(_response.Success(result, "Get Top Scores Successful!"));
        }
    }
}
