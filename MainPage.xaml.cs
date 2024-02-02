using GuessTheNumber.GameRules;
using GuessTheNumber.Enums;
using System.Diagnostics;
using Microsoft.Maui.Layouts;

namespace GuessTheNumber
{
    public partial class MainPage : ContentPage
    {
        Game game = null;
        EDifficulty _difficulty = EDifficulty.Easy;

        public MainPage()
        {
            InitializeComponent();
            SetInterfaceOnOppenning();
        }

        private void BtnNewGame_Clicked(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private async void BtnNewGameLittle_Clicked(object sender, EventArgs e)
        {
            bool startNewGame = await AlertNewGame();
            if (startNewGame)
            {
                SetInterfaceOnOppenning();
                StartNewGame();
            }            
        }

        private void BtnCheckGuess_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isCorrectGuess = game.CheckGuess(GetIntFromEntry(EntryNumberGuess.Text));
            
            if(isCorrectGuess)
            {
                SetInterfaceEndGame();
            }
            else
            {
                game.RemainingGuesses--;
                UpdateInterfaceAfterGuess();
            }

            bool hasGuessesLeft = game.RemainingGuesses >= 1;
            if(!hasGuessesLeft)
            {
                SetInterfaceLostGame();
            }
        }

        private int GetIntFromEntry(string str)
        {
            int x = -1;
            if (Int32.TryParse(str, out x))
            {
                return x;
            }

            return x;
        }

        private async Task<bool> AlertNewGame()
        {
            bool answer = await DisplayAlert("Are you shure?", "Start a new game?", "Yes", "No");
            return answer;
        }

        private async Task<bool> SelectDifficulty()
        {
            var difficulties = GetDifficultyNames();
            string action = await DisplayActionSheet("Choose a Difficulty:", "Cancel", null, difficulties);

            if (difficulties.Contains(action))
            {
                _difficulty = GetDifficultyFromUserInputString(action);
                return true;
            }

            return false;
        }

        private string[] GetDifficultyNames()
        {
            var valuesAsArray = Enum.GetValues(typeof(EDifficulty));
            List<string> names = new List<string>();

            foreach(var name in valuesAsArray)
            {
                names.Add(name.ToString());
            }
            
            return names.ToArray();
        }

        private EDifficulty GetDifficultyFromUserInputString(string userInput)
        {
            if (Enum.TryParse(userInput, out EDifficulty result))
            {
                return result;
            }

            return EDifficulty.Easy;
        }

        private async void StartNewGame()
        {
            var difficultySelected = await SelectDifficulty();
            if (difficultySelected)
            {
                game = new Game(_difficulty);
                SetInterfaceWhileInGame();
            }
        }

        internal void UpdateInterfaceAfterGuess()
        {
            LblHeadLine.Text = "Try a guess:";
            LblSubHeadLine.Text = $"Remaining guesses: {game.RemainingGuesses}";
            EntryNumberGuess.Text = string.Empty;
        }

        internal void SetInterfaceOnOppenning()
        {
            BtnNewGame.IsVisible = true;
            LblHeadLine.IsVisible = true;
            LblSubHeadLine.IsVisible = false;
            LblHeadLine.Text = "Welcome to Guessing the Number Game!!!";

            BtnCheckGuess.IsVisible = false;
            BtnNewGameLittle.IsVisible = false;
            EntryNumberGuess.IsVisible = false;
        }

        internal void SetInterfaceWhileInGame()
        {
            BtnNewGame.IsVisible = false;
            LblHeadLine.IsVisible = true;
            LblSubHeadLine.IsVisible = true;
            LblHeadLine.Text = "Try a guess:";
            LblSubHeadLine.Text = $"Remaining guesses: {game.RemainingGuesses}";
            EntryNumberGuess.Text = string.Empty;

            BtnCheckGuess.IsVisible = true;
            BtnNewGameLittle.IsVisible = true;
            EntryNumberGuess.IsVisible = true;
        }

        internal void SetInterfaceEndGame()
        {
            BtnNewGame.IsVisible = true;
            LblHeadLine.IsVisible = true;
            LblSubHeadLine.IsVisible = true;
            LblHeadLine.Text = "You Win!";
            LblSubHeadLine.Text = $"Remaining guesses: {game.RemainingGuesses}";

            BtnCheckGuess.IsVisible = false;
            BtnNewGameLittle.IsVisible = false;
            EntryNumberGuess.IsVisible = false;
        }

        internal void SetInterfaceLostGame()
        {
            BtnNewGame.IsVisible = true;
            LblHeadLine.IsVisible = true;
            LblSubHeadLine.IsVisible = false;
            LblHeadLine.Text = "You Lose!";

            BtnCheckGuess.IsVisible = false;
            BtnNewGameLittle.IsVisible = false;
            EntryNumberGuess.IsVisible = false;
        }
    }

}
