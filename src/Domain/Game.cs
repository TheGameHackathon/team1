using System;
using System.Collections.Generic;
using System.Linq;
using thegame.Controllers;

namespace Domain
{
    public class Game
    {
        public void SetUp()
        {
            Field = GenerateField(GenerateOneDimensionalField());

            try
            {
                randomCellCreator.CreateAndAddRandomCellToGame(this);
                randomCellCreator.CreateAndAddRandomCellToGame(this);
            }
            catch (Exception e)
            {
                throw new Exception("Game is over");
            }
        }
        public Game()
        {
        }

        public Game(Guid id) : this(4, 4, id)
        {
        }

        public Game(int width, int height, Guid id)
        {
            MonitorKeyboard = true;
            MonitorMouseClicks = false;
            Width = width;
            Height = height;
            Id = id;
            IsFinished = false;
            Score = 0;
            Field = null;
            SetUp();
        }

        public Cell[] Cells
        {
            get => GetCells();
            set => Field = GenerateField(value);
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public bool MonitorKeyboard { get; set; }
        public bool MonitorMouseClicks { get; set; }
        public Guid Id { get; set; }
        public bool IsFinished { get; set; }
        public int Score { get; set; }

        public Cell[,] Field { get; set; }

        private readonly IRandomCellCreator randomCellCreator = new RandomCellCreator();

        private Cell[] GenerateOneDimensionalField()
        {
            var result = new List<Cell>();
            var idCounter = 0;
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++, idCounter++)
                    result.Add(new Cell(idCounter.ToString(), new Vector{X = x, Y=y}, 0));
            }

            return result.ToArray();
        }
        
        private Cell[,] GenerateField(Cell[] cells)
        {
            var generatedField = new Cell[Width, Height];
            foreach (var cell in cells)
                generatedField[cell.Pos.X, cell.Pos.Y] = cell;
            return generatedField;
        }

        private Cell[] GetCells()
        {
            var cells = new Cell[Field.Length];
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    cells[x * Width + y] = Field[x, y];
                }
            }

            return cells;
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

                    if (cell.Value == 0) continue;
                    
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
                            Score += rightCell.Value;
                            CheckForWinning(rightCell);
                            cell.Value = 0;
                        }
                    }
                }
            }

            IsFinished = IsLose();
            if (!IsFinished && !(dxdy.X == 0 && dxdy.Y == 0))
                randomCellCreator.CreateAndAddRandomCellToGame(this);
        }

        private void CheckForWinning(Cell cell)
        {
            if (cell.Value == 2048)
            {
                IsFinished = true;
            }
        }

        private bool IsLose()
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    var cell = Field[i, j];
                    for (var dx = -1; dx <= 1; dx++)
                    {
                        for (var dy = -1; dy <= 1; dy++)
                        {
                            if (dx == dy) continue;
                            var neighbourPos = new Vector() {X = cell.Pos.X + dx, Y = cell.Pos.Y + dy};
                            if (InBound(neighbourPos))
                            {
                                var neighbourCell = Field[neighbourPos.X, neighbourPos.Y];
                                if (!cell.HasNeighbour(neighbourCell)) return false;
                                if (cell.TryMerge(neighbourCell, out var _)) return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
        
        private Vector SetMoveShift(int direction)
        {
            return direction switch
            {
                37 => new Vector() {X = -1, Y = 0},
                38 => new Vector() {X = 0, Y = -1},
                39 => new Vector() {X = 1, Y = 0},
                40 => new Vector() {X = 0, Y = 1},
                _ => new Vector() {X = 0, Y = 0}
            };
        }

        private (IEnumerable<int>, IEnumerable<int>) SetForDirection(int direction)
        {
            return direction switch
            {
                
                37 => (Enumerable.Range(0, Width), Enumerable.Range(0, Height)),
                39 => (Enumerable.Range(0, Width).Reverse(), Enumerable.Range(0, Height)),
                38 => (Enumerable.Range(0, Width), Enumerable.Range(0, Height)),
                40 => (Enumerable.Range(0, Width), Enumerable.Range(0, Height).Reverse()),
                _ => (Enumerable.Empty<int>(), Enumerable.Empty<int>())
            };
        }
        
        private bool CanBeMoved(Cell cell, int dx, int dy)
        {
            return InBound(new Vector() {X = cell.Pos.X + dx, Y = cell.Pos.Y + dy}) && 
                     !cell.HasNeighbour(Field[cell.Pos.X + dx, cell.Pos.Y + dy]);
        }

        private bool InBound(Vector cellPos)
        {
            return !(cellPos.X < 0 ||
                     cellPos.X >= Width ||
                     cellPos.Y < 0 ||
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