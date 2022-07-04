using System;
using System.Collections.Generic;
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
        private readonly Dictionary<Guid, Game> entities = new Dictionary<Guid, Game>();
        public Game GetOrCreate(Guid id)
        {
            if (entities.TryGetValue(id, out var game))
            {
                return Clone(id, game);
            }

            var entity = new Game(); //TODO
            entities[id] = entity; //TODO
            return Clone(id, entity);
        }

        public void Update(Game game)
        {
            if (!entities.ContainsKey(game.Id))
                return;

            entities[game.Id] = Clone(game.Id, game);
        }

        public void Delete(Guid id)
        {
            entities.Remove(id);
        }

        private Game Clone(Guid id, Game game)
        {
            return new Game(); // TODO
        }
    }
}