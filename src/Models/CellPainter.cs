using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thegame.Controllers;
using thegame.Services;

namespace thegame.Models
{
    public class CellPainter
    {
        public static void PaintOnlyPlayerCell(GameDto game, Vec playerPosition, string color)
        {
            game.GetCellAtCoords(playerPosition).Type = color;
        }

        public static void PaintAdjacentCellsOfColor(CellDto cell, string color)
        {
            var connected = GamesController.repo.Field.GetAllColorConnectedCells(cell);
            foreach (var one in connected)
            {
                one.Type = color;
            }
        }
    }
}
