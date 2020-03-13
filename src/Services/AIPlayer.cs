using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thegame.Controllers;
using thegame.Models;

namespace thegame.Services
{
    public class AIPlayer
    {
        private CellDto _startPosition;
        public AIPlayer(CellDto startPosition)
        {
            _startPosition = startPosition;
        }

        public string PickColor(CellDto[] field)
        {
            Random random = new Random();
            return ("color" + random.Next(5));
        }

        private List<CellDto> GetNeighborOtherColorCells(CellDto[] field)
        {
           // var sameColorBlob = GamesController.repo.Field.GetAllNeighboursSameColor(_startPosition);
          //  HashSet<CellDto> visited = new HashSet<CellDto>();
            //visited = sameColorBlob.ToHashSet();
            throw new NotImplementedException();
        }

    }
}
