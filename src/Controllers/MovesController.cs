using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            var repo = GamesController.repo;

            if (userInput.ClickedPos != null)
            {
                var clickedCell = repo.Field.GetCellAtCoords(userInput.ClickedPos);
                var clickedColor = clickedCell.Type;

                if (GamesController.repo.PlayerCell.Type != clickedColor)
                    repo.Field.Score++;

                CellPainter.PaintAdjacentCellsOfColor(repo.PlayerCell, clickedColor);

                repo.Field.IsFinished = repo.Field.AllCellsAreOfOneColor();
            }

            if (userInput.KeyPressed == 'I')
            {
                var pickedColor = new AIPlayer(repo.PlayerCell).PickColor(repo.Field.Cells);
                CellPainter.PaintAdjacentCellsOfColor(repo.PlayerCell, pickedColor);
                repo.Field.Score++;
            }

            if (userInput.KeyPressed == 'X')
            {
                // Press X to Win
                repo.Field.IsFinished = true;
            }

            GamesController.repo.Field = repo.Field;
            return new ObjectResult(repo.Field);
        }
    }
}