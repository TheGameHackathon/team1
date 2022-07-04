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
        public MovesController(IGamesRepository gamesRepository, IMapper mapper)
        {
            _gamesRepository = gamesRepository;
            _mapper = mapper;
        } 
        
        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody] UserInputDto userInput)
        {
            if (gameId == Guid.Empty)
                return BadRequest();

            var gameEnity = _gamesRepository.FindGameById(gameId);

            if (gameEnity is null)
                return NotFound();

            gameEnity.MoveCells(userInput.KeyPressed);
            _gamesRepository.Update(gameEnity);

            var gameDto = _mapper.Map<GameDto>(gameEnity);
            
            return Ok(gameDto);
        }
    }
}