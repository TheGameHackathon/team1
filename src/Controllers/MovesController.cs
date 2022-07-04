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
            var direction = userInput.KeyPressed;
            var currentGameState = _gamesRepository.GetOrCreate(gameId);

            foreach (var cell in currentGameState.Cells)
            {
                switch ((int) direction)
                {
                    case 37:
                        MoveCell(cell, -1, 0);
                        break;
                    case 38:
                        MoveCell(cell, 0, 1);
                        break;
                    case 39:
                        MoveCell(cell, 1, 0);
                        break;
                    case 40:
                        MoveCell(cell, 0, -1);
                        break;
                }
            }
            
            _gamesRepository.Update(currentGameState);
            return Ok(currentGameState);
        }

        private void MoveCell(Cell cell, int dx, int dy)
        {
            cell.Pos = new Vector() {X = cell.Pos.X + dx, Y = cell.Pos.Y + dy};
        }
    }
}