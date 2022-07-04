using System;

namespace Domain
{
    class RandomBot : IBot
    {
        public void MakeStep(Game currentGame)
        {
            var randomDirection = new Random().Next(37, 41);
            currentGame.MoveCells(randomDirection);
        }
    }
}