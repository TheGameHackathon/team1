using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using thegame.Controllers;

namespace Domain
{
    public class Game
    {
        public Game()
        {
            
        }
        
        public Game(Guid id)
        {
            Id = id;
        }
        
        public Game(Cell[] cells, int width, int height, Guid id,
            bool isFinished, int score)
        {
            Cells = cells;
            MonitorKeyboard = false;
            MonitorMouseClicks = false;
            Width = width;
            Height = height;
            Id = id;
            IsFinished = isFinished;
            Score = score;
            Field = GenerateField(cells, width, height);
        }
        public Cell[] Cells { get; set; }
        public Cell[,] Field { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool MonitorKeyboard { get; set; }
        public bool MonitorMouseClicks { get; set; }
        public Guid Id { get; set; }
        public bool IsFinished { get; set; }
        public int Score { get; set; }

        private Cell[,] GenerateField(Cell[] cells, int width, int height)
        {
            var field = new Cell[height, width];
            foreach (var cell in cells)
                field[cell.Pos.X, cell.Pos.Y] = cell;
            return field;
        }

        public void MoveCells(int direction)
        {
            var dxdy = SetMoveShift(direction);
            var (irange, jrange) = SetForDirection(direction);
            
            foreach(var i in irange)
            {
                foreach (var j in jrange)
                {
                    var cell = Field[i, j];

                    while (CanBeMoved(cell, dxdy.X, dxdy.Y))
                    {
                        MoveCell(cell, dxdy.X, dxdy.Y);
                        cell = Field[cell.Pos.X + dxdy.X, cell.Pos.Y + dxdy.Y];
                    }

                    if (InBound(new Vector() {X = cell.Pos.X + dxdy.X, Y = cell.Pos.Y + dxdy.Y}) && 
                        cell.HasNeighbour(Field[cell.Pos.X + dxdy.X, cell.Pos.Y + dxdy.Y]))
                    {
                        var rightCell = Field[cell.Pos.X + dxdy.X, cell.Pos.Y + dxdy.Y];
                        if (cell.TryMerge(rightCell, out var _))
                        {
                            rightCell.Value = cell.Value + rightCell.Value;
                            cell.Value = 0;
                        }
                    }
                }
            }
        }

        private Vector SetMoveShift(int direction)
        {
            return direction switch
            {
                37 => new Vector() {X = -1, Y = 0},
                38 => new Vector() {X = 0, Y = 1},
                39 => new Vector() {X = 1, Y = 0},
                40 => new Vector() {X = 0, Y = -1},
                _ => new Vector() {X = 0, Y = 0}
            };
        }

        private (IEnumerable<int>, IEnumerable<int>) SetForDirection(int direction)
        {
            return direction switch
            {
                37 => (Enumerable.Range(0, Width - 1), Enumerable.Range(0, Height - 1)),
                39 => (Enumerable.Range(Width - 1, 0), Enumerable.Range(0, Height - 1)),
                38 => (Enumerable.Range(0, Width - 1), Enumerable.Range(0, Height - 1)),
                40 => (Enumerable.Range(0, Width - 1), Enumerable.Range(Height - 1, 0)),
                _ => (Enumerable.Empty<int>(), Enumerable.Empty<int>())
            };
        }
        
        private bool CanBeMoved(Cell cell, int dx, int dy)
        {
            return !(InBound(new Vector() {X = cell.Pos.X + dx, Y = cell.Pos.Y + dy}) || 
                     cell.HasNeighbour(Field[cell.Pos.X + dx, cell.Pos.Y + dy]));
        }

        private bool InBound(Vector cellPos)
        {
            return !(cellPos.X < 0 ||
                     cellPos.X >= Width ||
                     cellPos.Y <= 0 ||
                     cellPos.Y >= Height);
        }
        
        private void MoveCell(Cell cell, int dx, int dy)
        {
            var newPos = new Vector() {X = cell.Pos.X + dx, Y = cell.Pos.Y + dy};
            Field[newPos.X, newPos.Y].Value = cell.Value;
            cell.Value = 0;
        }
    }
}