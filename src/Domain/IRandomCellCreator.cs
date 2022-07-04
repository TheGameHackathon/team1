using thegame.Models;

namespace Domain
{
    public interface IRandomCellCreator
    {
        void CreateAndAddRandomCellToGame(Game game);
        Vector GetPosOfRandomEmptyCell(Game game);
    }
}