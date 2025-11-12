using cubets_core.Common;
using cubets_core.Modules.Player.DTOs;
using cubets_core.Modules.Player.Services;
using Microsoft.AspNetCore.Mvc;

namespace cubets_core.Modules.Player.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IResponseService _response;
        private readonly PlayerService _playerService;

        public PlayerController(PlayerService playerService, IResponseService response)
        {
            _response = response;
            _playerService = playerService;
        }

        [HttpGet("{id}")]
        public ActionResult<PlayerDTO> GetPlayer(int id)
        {
            var playerDto = _playerService.GetPlayerDto(id);
            if (playerDto == null) return NotFound(_response.Fail<object>("Player Not Found!"));

            return Ok(_response.Success(playerDto, "Get Player Data Is Successful!"));

        }
    }
}
