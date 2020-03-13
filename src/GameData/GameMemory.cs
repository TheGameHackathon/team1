using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thegame.Services;

namespace thegame.GameData
{
    public class GameMemory
    {
        public static Dictionary<Guid, GamesRepo> Memory = new Dictionary<Guid, GamesRepo>();
    }
}
