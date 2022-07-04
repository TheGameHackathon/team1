using System;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{

    [Route("api/games")]
    public class GamesController : Controller
    {
        private readonly IGamesRepository gamesRepository;
        public GamesController(IGamesRepository gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }
        
        [HttpPost]
        public IActionResult Index()
        {
            return Ok(gamesRepository.GetOrCreate(Guid.NewGuid()));
        }
    }
}