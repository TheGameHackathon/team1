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
            var game = GamesRepo.Field;

            if (userInput.ClickedPos != null)
            {
                var clickedCell = game.GetCellAtCoords(userInput.ClickedPos);
                var clickedColor = clickedCell.Type;

                //CellPainter.PaintOnlyPlayerCell(game, GamesRepo.PlayerPosition, clickedColor);

                var cells = new List<CellDto>();
                foreach (var cell in game.Cells.Where(c => c.Type != clickedColor))
                {
                    cells.Add(cell);
                }
                CellPainter.PaintCellsList(cells, clickedColor, "*");
            }

            GamesRepo.Field = game;
            return new ObjectResult(game);
        }
    }
}