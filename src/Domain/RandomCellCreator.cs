using System;
using System.Collections.Generic;
using System.Linq;
using thegame.Models;

namespace Domain;

public class RandomCellCreator : IRandomCellCreator
{
    private Cell[,] _cells;

    public void CreateAndAddRandomCellToGame(Game game)
    {
        _cells = game.Field;

        var newCellPos = GetPosOfRandomEmptyCell(game);
        var random = new Random();
        var newContent = random.Next(1, 3);

        var oldCell = _cells[newCellPos.X, newCellPos.Y];
        var newCell = new Cell(oldCell.Id, oldCell.Pos, newContent * 2);
        
        game.Field[newCellPos.X, newCellPos.Y] = newCell;
    }

    public Vector GetPosOfRandomEmptyCell(Game game)
    {
        var emptyCells = GetEmptyCells(game);
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