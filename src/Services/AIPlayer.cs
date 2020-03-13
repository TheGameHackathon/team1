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

            var color = "color" + random.Next(5);

            while (color == _startPosition.Type ||
                !field.Any(c => c.Type == color))
                color = "color" + random.Next(5);

            return color;
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
