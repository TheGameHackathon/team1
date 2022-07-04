using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{

    [Route("api/games")]
    public class GamesController : Controller
    {
        private readonly IGamesRepository gamesRepository;
        private readonly IMapper mapper;
        public GamesController(IGamesRepository gamesRepository, IMapper mapper)
        {
            this.gamesRepository = gamesRepository;
            this.mapper = mapper;
        }
        
        [HttpPost]
        public IActionResult Index([FromBody] SizeToPostDto sizeToPostDto)
        {
            var game = gamesRepository.GetOrCreate(Guid.NewGuid(), sizeToPostDto.Size, sizeToPostDto.Size);
            var gameDto = mapper.Map<GameDto>(game);
            return Ok(gameDto);
        }
    }
}