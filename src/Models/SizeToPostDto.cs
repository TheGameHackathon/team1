using System.ComponentModel;

namespace thegame.Models
{
    public class SizeToPostDto
    {
        [DefaultValue(4)]
        public int Size { get; set; }
    }
}