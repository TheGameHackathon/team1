using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services
{
    public class GamesRepo
    {
        public static GameDto Field { get; set; } = TestData.AGameDto(new Vec(1, 1));
        public static Vec PlayerPosition = new Vec(0, 0);
    }
}