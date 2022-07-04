namespace Domain
{
    public class StupidBot : IBot
    {
        private uint pointer;
        private int[] arrCommands = {37, 38, 39, 40};

        public void MakeStep(Game currentGame)
        {
            currentGame.MoveCells(arrCommands[pointer]);
            pointer++;
            pointer %= 4;
        }
    }
}