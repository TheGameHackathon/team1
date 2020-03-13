using System;
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
            var game = GamesRepo.Field;

            if (userInput.ClickedPos != null)
            {
                var clickedCell = game.GetCellAtCoords(userInput.ClickedPos.X, userInput.ClickedPos.Y);
                var clickedColor = clickedCell.Type;

                PaintOnlyPlayerCell(game, GamesRepo.PlayerPosition, clickedColor);

                //foreach (var cell in game.Cells.Where(c => c.Type != clickedColor))
                //{
                //    cell.Type = clickedColor;
                //    cell.Content = "*";
                //}
            }

            GamesRepo.Field = game;
            return new ObjectResult(game);
        }

        private void PaintOnlyPlayerCell(GameDto game, Vec playerPosition, string color)
        {
            game.GetCellAtCoords(playerPosition).Type = color;
        }
    }
}