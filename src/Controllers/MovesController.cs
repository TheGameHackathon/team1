using System;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{

    [Route("api/games/{gameId}/moves")]
    public class MovesController : Controller
    {
        private IGamesRepository _gamesRepository;
        public MovesController(IGamesRepository gamesRepository)
        {
            _gamesRepository = gamesRepository;
        }
        
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody] UserInputDto userInput)
        {
            var currentGameState = _gamesRepository.FindGameById(gameId);

            if (currentGameState is null)
                return NotFound();
            
            if (gameId == Guid.Empty)
                return BadRequest();
            
            currentGameState.MoveCells(userInput.KeyPressed);
            _gamesRepository.Update(currentGameState);
            
            return Ok(currentGameState);
        }
    }
}