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

        public bool IsEmpty => Value == 0;

        public bool IsMergeableWith(Cell other) => Value == other.Value;

        public bool HasNeighbour(Cell other) => !other.IsEmpty;
    }
}