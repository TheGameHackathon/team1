using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.GameData;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{
    [Route("api/games/{gameId}/moves")]
    public class MovesController : Controller
    {

        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody]UserInputForMovesPost userInput)
        {
            if (!GameMemory.Memory.TryGetValue(gameId, out GamesRepo repo))
                return null;

            var game = repo.Field;

            if (userInput.ClickedPos != null)
            {
                game.Score++;
                var clickedCell = game.GetCellAtCoords(userInput.ClickedPos);
                var clickedColor = clickedCell.Type;

                CellPainter.PaintAdjacentCellsOfColor(gameId,game.GetCellAtCoords(GamesRepo.PlayerPosition), clickedColor);

                game.IsFinished = game.AllCellsAreOfOneColor();
            }

            return new ObjectResult(game);
        }
    }
}