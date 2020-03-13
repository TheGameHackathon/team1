using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thegame.Services;

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

        public static void PaintAdjacentCellsOfColor(CellDto cell, string color)
        {
            string oldColor = cell.Type;
            cell.Type = color;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (Math.Abs(dx) + Math.Abs(dy) == 1)
                    {
                        int nextX = cell.Pos.X + dx;
                        int nextY = cell.Pos.Y + dy;
                        if (nextX >= 0 && nextX < GamesRepo.Field.Width && nextY >= 0 && nextY < GamesRepo.Field.Height)
                        {
                            var nextCell = GamesRepo.Field.GetCellAtCoords(nextX, nextY);
                            if (nextCell.Type == oldColor)
                            {
                                PaintAdjacentCellsOfColor(nextCell, color);
                            }
                        }
                    }
                }
            }
        }
    }
}
