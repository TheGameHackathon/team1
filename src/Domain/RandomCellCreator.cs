using System;
using System.Collections.Generic;
using System.Linq;
using thegame.Models;

namespace Domain
{
    public class RandomCellCreator : IRandomCellCreator
    {
        private Cell[,] _cells;

        public void CreateAndAddRandomCellToGame(Game game)
        {
            _cells = game.Field;

            var newCellPos = GetPosOfRandomEmptyCell(game);

            if (newCellPos == null)
                throw new Exception("Game is over! No empty cells");

            var random = new Random();
            var newContent = random.Next(1, 6) > 1 ? 2 : 4;

            var oldCell = _cells[newCellPos.X, newCellPos.Y];
            var newCell = new Cell(oldCell.Id, oldCell.Pos, newContent);

            game.Field[newCellPos.X, newCellPos.Y] = newCell;
        }

        public Vector GetPosOfRandomEmptyCell(Game game)
        {
            var emptyCells = GetEmptyCells(game);
            if (emptyCells.Length == 0)
                return null;

            var random = new Random();
            var cellNum = random.Next(0, emptyCells.Length - 1);

            return emptyCells[cellNum].Pos;
        }

        Cell[] GetEmptyCells(Game game)
        {
            var result = new List<Cell>();
            foreach (var cell in game.Field)
            {
                if (cell.Value == 0)
                {
                    result.Add(cell);
                }
            }

            return result.ToArray();
        }
    }
}