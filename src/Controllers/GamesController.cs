using Microsoft.AspNetCore.Mvc;
using thegame.GameData;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{
    public class GamesController : Controller
    {
        public static GamesRepo repo;

        [Route("api/games/easy")]
        [HttpPost]
        public IActionResult StartEasyGame()
        {
            repo = new GamesRepo(5, 3, 3);
            GameMemory.Memory.Add(repo.Field.Id, repo);
            return new ObjectResult(repo.Field);
        }

        [Route("api/games/medium")]
        [HttpPost]
        public IActionResult StartMediumGame()
        {
            repo = new GamesRepo(10, 8, 4);
            GameMemory.Memory.Add(repo.Field.Id, repo);
            return new ObjectResult(repo.Field);
        }

        [Route("api/games/hard")]
        [HttpPost]
        public IActionResult StartHardGame()
        {
            repo = new GamesRepo(32, 18, 5);
            GameMemory.Memory.Add(repo.Field.Id, repo);
            return new ObjectResult(repo.Field);
        }
    }
}
