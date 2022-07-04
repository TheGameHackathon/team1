using thegame.Models;

namespace Domain;

public interface IRandomCellCreator
{
    Cell CreateRandomCell(Game game);
}