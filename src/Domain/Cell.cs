namespace Domain
{
    public class Cell
    {
        public Cell(string id, Vector pos, string type, string content)
        {
            Id = id;
            Pos = pos;
            Type = type;
            Content = content;
        }
        public string Id { get; set; }
        public Vector Pos { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}