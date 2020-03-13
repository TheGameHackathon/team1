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
                var clickedCell = repo.Field.GetCellAtCoords(userInput.ClickedPos);
                var clickedColor = clickedCell.Type;

                CellPainter.PaintAdjacentCellsOfColor(gameId,repo.PlayerCell, clickedColor);

            }

            if (userInput.KeyPressed == 'I')
            {
                string color = new AIPlayer(repo.PlayerCell).PickColor(repo.Field.Cells);

                CellPainter.PaintAdjacentCellsOfColor(gameId, repo.PlayerCell, color);
            }

            repo.Field.IsFinished = repo.Field.AllCellsAreOfOneColor();
            return new ObjectResult(game);
        }
    }
}