using System;

namespace Domain
{
    public class Game
    {
        public Game() : this(4, 4, Guid.NewGuid())
        {
        }

        public Game(Guid id) : this(4, 4, id)
        {
        }

        public Game(int width, int height, Guid id)
        {
            MonitorKeyboard = false;
            MonitorMouseClicks = false;
            Width = width;
            Height = height;
            Id = id;
            IsFinished = false;
            Score = 0;
            Field = new Cell[height, width];
        }

        public Cell[] Cells
        {
            get => GetCells();
            set => Field = GenerateField(value, Width, Height);
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public bool MonitorKeyboard { get; set; }
        public bool MonitorMouseClicks { get; set; }
        public Guid Id { get; set; }
        public bool IsFinished { get; set; }
        public int Score { get; set; }

        public Cell[,] Field { get; set; }

        private Cell[,] GenerateField(Cell[] cells, int width, int height)
        {
            var generatedField = new Cell[width, height];
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
            foreach (var cell in Cells)
            {
                switch (direction)
                {
                    case 37:
                        MoveCell(cell, -1, 0);
                        break;
                    case 38:
                        MoveCell(cell, 0, 1);
                        break;
                    case 39:
                        MoveCell(cell, 1, 0);
                        break;
                    case 40:
                        MoveCell(cell, 0, -1);
                        break;
                }
            }
        }

        private void MoveCell(Cell cell, int dx, int dy)
        {
            cell.Pos = new Vector() {X = cell.Pos.X + dx, Y = cell.Pos.Y + dy};
        }
    }
}