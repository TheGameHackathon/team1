using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services
{
    public class GamesRepo
    {
        public GameDto Field { get; set; }
        public static Vec PlayerPosition { get; } = new Vec(0, 0);
        public CellDto PlayerCell { get => Field.GetCellAtCoords(PlayerPosition); }

        private int width;
        private int height;
        private int numberOfColors;

        public GamesRepo(int width, int height, int numberOfColors)
        {
            Field = TestData.AGameDto(width, height, numberOfColors);

            this.width = width;
            this.height = height;
            this.numberOfColors = numberOfColors;
        }

        public void UpdateMap()
        {
            Field = TestData.AGameDto(width, height, numberOfColors);
        }
    }
}