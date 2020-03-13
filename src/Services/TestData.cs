using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using thegame.Models;

namespace thegame.Services
{
    public class TestData
    {
        public static GameDto AGameDto(int width, int height, int numberOfColors)
        {
            CellDto[] testCells = new CellDto[width * height];
            Random random = new Random();
            for (int i = 0; i < width * height; i++)
            {
                testCells[i] = new CellDto(i.ToString(), new Vec(i % width, i / width), "color" + random.Next(0, numberOfColors), "", 0);
            }

            return new GameDto(testCells, true, true, width, height, Guid.NewGuid(), false, 0);
        }        
    }
}