namespace Domain
{
    public class LeftUpBot : IBot
    {
        private Directions prefStep = Directions.Left;

        private enum Directions
        {
            Left = 37,
            Up = 38,
            Right = 39,
            Down = 40,
        }

        public void MakeStep(Game currentGame)
        {
            var cashField = currentGame.Field;
            if (prefStep == Directions.Left)
                currentGame.MoveCells((int) Directions.Up);
            else
                currentGame.MoveCells((int) Directions.Left);
            
            if (!StepIsDone(cashField, currentGame.Field))
                currentGame.MoveCells((int) Directions.Right);

        }

        private bool StepIsDone(Cell[,] beforeCommand, Cell[,] afterCommand)
        {
            for (var i = 0; i < beforeCommand.GetLength(0); i++)
            for (var j = 0; j < beforeCommand.GetLength(1); j++)
                    if (beforeCommand[i, j].Value != afterCommand[i, j].Value)
                        return true;
            
            return false;
        }
    }
}