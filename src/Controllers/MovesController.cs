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
            var game = GamesController.repo.Field;

            if (userInput.ClickedPos != null)
            {
                var clickedCell = game.GetCellAtCoords(userInput.ClickedPos);
                var clickedColor = clickedCell.Type;

                if (GamesController.repo.PlayerCell.Type != clickedColor)
                    game.Score++;

                CellPainter.PaintAdjacentCellsOfColor(game.GetCellAtCoords(GamesRepo.PlayerPosition), clickedColor);

                game.IsFinished = game.AllCellsAreOfOneColor();
            }

            GamesController.repo.Field = game;
            return new ObjectResult(game);
        }
    }
}