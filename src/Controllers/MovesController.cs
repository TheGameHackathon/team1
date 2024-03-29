using System;
using System.Linq;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{

    [Route("api/games/{gameId}/moves")]
    public class MovesController : Controller
    {
        private IGamesRepository _gamesRepository;
        private IMapper _mapper;
        private readonly IBot _bot;

        public MovesController(IGamesRepository gamesRepository, IMapper mapper, IBot bot)
        {
            _gamesRepository = gamesRepository;
            _mapper = mapper;
            _bot = bot;
        } 
        
        [HttpPost]
        public IActionResult Moves([FromRoute] Guid gameId, [FromBody] UserInputDto userInput)
        {
            if (gameId == Guid.Empty)
                return BadRequest();

            var gameEntity = _gamesRepository.FindGameById(gameId);

            if (gameEntity is null)
                return NotFound();

            if (userInput.keyPressed == 'I')
                _bot.MakeStep(gameEntity);
            else
                gameEntity.MoveCells(userInput.keyPressed);
            _gamesRepository.Update(gameEntity);

            var gameDto = _mapper.Map<GameDto>(gameEntity);
            
            return Ok(gameDto);
        }
    }
}