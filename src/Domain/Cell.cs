using thegame.Models;

namespace Domain
{
    public class Cell
    {
        public Cell(string id, Vector pos, int value)
        {
            Id = id;
            Pos = pos;
            Value = value;
        }

        public string Id { get; set; }
        public Vector Pos { get; set; }
        public int Value { get; set; }

        public bool TryMerge(Cell other, out Cell result)
        {
            result = default;
            if (Value != other.Value) return false;

            result = new Cell(Id, Pos, Value + other.Value);
            return true;
        }

        public bool HasNeighbour(Cell other)
        {
            return other.Value != 0;
        }
    }
}