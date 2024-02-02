using GuessTheNumber.Enums;

namespace GuessTheNumber.GameRules
{
    internal class Game
    {
        public EDifficulty Difficulty { get; }
        public int Goal { get; }
        public int RemainingGuesses { get; set; }
        public DateTime StartDateTime { get; }
        public List<int> Guesses { get; set; } = new List<int>();

        internal Game(EDifficulty difficulty)
        {
            Difficulty = difficulty;
            RemainingGuesses = GetTriesAmmount();
            Goal = CreateNewGoal();
            StartDateTime = DateTime.Now;
        }

        internal bool CheckGuess(int guess)
        {
            return guess == Goal;
        }

        private int CreateNewGoal()
        {
            int gap = GetGoalGap();

            Random random = new Random();
            int goal = random.Next(gap+1); //from 0 to (goal)

            return goal;
        }

        private int GetGoalGap()
        {
            const int minimun = 10;

            switch (Difficulty)
            {
                case EDifficulty.Easy:
                    return minimun;
                case EDifficulty.Normal:
                    return 50;
                //case EDifficulty.Hard:
                //    return 100;
                //case EDifficulty.Ultra:
                //    return 500;
                default:
                    return minimun;
            }
        }

        private int GetTriesAmmount()
        {
            const int minimun = 3;

            switch (Difficulty)
            {
                case EDifficulty.Easy:
                    return minimun;
                case EDifficulty.Normal:
                    return 5;
                //case EDifficulty.Hard:
                //    return 10;
                //case EDifficulty.Ultra:
                //    return 10;
                default:
                    return minimun;
            }
        }

    }
}
