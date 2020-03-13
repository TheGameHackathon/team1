using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Models
{
    public class CellPainter
    {
        public static void PaintOnlyPlayerCell(GameDto game, Vec playerPosition, string color)
        {
            game.GetCellAtCoords(playerPosition).Type = color;
        }

        public static void PaintCellsList(List<CellDto> cells, string color, string content = "") // добавить int score? Player player, которому начислять score?
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].Type = color;
                cells[i].Content = content;
            }
        }
    }
}
