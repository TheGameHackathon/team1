using System;
using Domain;

namespace thegame.Services
{
    public interface IGamesRepository
    {
        Game GetOrCreate(Guid id);
        void Update(Game game);
        void Delete(Guid id);
    }

    public class GamesRepository : IGamesRepository
    {
        public Game GetOrCreate(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Game game)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}