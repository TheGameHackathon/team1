using System;

namespace Domain
{
    public class Game
    {
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
    }
}