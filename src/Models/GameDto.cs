using System;
using System.Collections.Generic;
using System.Linq;

namespace thegame.Models
{
    public class GameDto
    {
        public GameDto(CellDto[] cells, bool monitorKeyboard, bool monitorMouseClicks, int width, int height, Guid id, bool isFinished, int score)
        {
            Cells = cells;
            MonitorKeyboard = monitorKeyboard;
            MonitorMouseClicks = monitorMouseClicks;
            Width = width;
            Height = height;
            Id = id;
            IsFinished = isFinished;
            Score = score;
        }

        public CellDto[] Cells;
        public int Width;
        public int Height;
        public bool MonitorKeyboard;
        public bool MonitorMouseClicks;
        public Guid Id;
        public bool IsFinished;
        public int Score;

        public CellDto GetCellAtCoords(int x, int y)
        {
            return Cells.FirstOrDefault(c => c.Pos.X == x && c.Pos.Y == y);
        }
        public CellDto GetCellAtCoords(Vec coords)
        {
            return GetCellAtCoords(coords.X, coords.Y);
        }
        public List<CellDto> GetAllNeighbours(CellDto cell)
        {
            List<CellDto> neighbours = new List<CellDto>();
            var up = GetCellAtCoords(cell.Pos.X, cell.Pos.Y+1);
            var down = GetCellAtCoords(cell.Pos.X, cell.Pos.Y-1);
            var right = GetCellAtCoords(cell.Pos.X+1, cell.Pos.Y);
            var left = GetCellAtCoords(cell.Pos.X-1, cell.Pos.Y);
            if(up != null) neighbours.Add(up);
            if(down != null) neighbours.Add(down);
            if(right != null) neighbours.Add(right);
            if(left != null) neighbours.Add(left);
            return neighbours;
        }

        public List<CellDto> GetAllNeighboursSameColor(CellDto cell)
        {
            List<CellDto> neighbours = new List<CellDto>();
            var up = GetCellAtCoords(cell.Pos.X, cell.Pos.Y+1);
            var down = GetCellAtCoords(cell.Pos.X, cell.Pos.Y-1);
            var right = GetCellAtCoords(cell.Pos.X+1, cell.Pos.Y);
            var left = GetCellAtCoords(cell.Pos.X-1, cell.Pos.Y);
            if(up != null && up.Type == cell.Type) neighbours.Add(up);
            if(down != null && down.Type == cell.Type) neighbours.Add(down);
            if(right != null && right.Type == cell.Type) neighbours.Add(right);
            if(left != null && left.Type == cell.Type) neighbours.Add(left);
            return neighbours;
        }

        public List<CellDto> GetAllColorConnectedCells(CellDto cell)
        {
            HashSet<CellDto> visited = new HashSet<CellDto>();
            HashSet<CellDto> connected = new HashSet<CellDto>();
            connected.Add(cell);
            return Recursive(cell, visited, connected);
        }
        private List<CellDto> Recursive(CellDto cell, HashSet<CellDto> visited, HashSet<CellDto> connected)
        {
            List<CellDto> cells;
            if (GetAllNeighboursSameColor(cell).Count == 0)
            {
                cells = new List<CellDto>();
                cells.Add(cell);
                return cells;
            }
            else
            {
                cells = GetAllNeighboursSameColor(cell);
                foreach (var bruh in cells)
                {
                    connected.Add(bruh);
                    if (!visited.Contains(bruh))
                    {
                        visited.Add(bruh);
                        Recursive(bruh, visited, connected);
                    }
                }
            }

            return connected.ToList();
        }


    }
}