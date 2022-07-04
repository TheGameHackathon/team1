using System;

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
        }
        public Cell[] Cells { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool MonitorKeyboard { get; set; }
        public bool MonitorMouseClicks { get; set; }
        public Guid Id { get; set; }
        public bool IsFinished { get; set; }
        public int Score { get; set; }

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