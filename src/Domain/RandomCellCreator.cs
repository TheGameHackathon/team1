using System;
using System.Collections.Generic;
using thegame.Models;

namespace Domain;

public class RandomCellCreator : IRandomCellCreator
{
    private Cell[] _cells;
    public Cell CreateRandomCell(Game game)
    {
        _cells = game.Cells;
        var emptyCells = GetEmptyCells();

        var random = new Random();
        var cellNum = random.Next(0, emptyCells.Length - 1);
        var newContent = random.Next(1, 4);
        
        emptyCells[cellNum].Content = (newContent * 2).ToString();
        return emptyCells[cellNum];
    }
    
    Cell[] GetEmptyCells()
    {
        var result = new List<Cell>();
        foreach (var cell in _cells)
        {
            if (cell.Content == string.Empty)
            {
                result.Add(cell);
            } 
        }

        return result.ToArray();
    }
}