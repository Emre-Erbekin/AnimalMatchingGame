

namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainBtn_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            PlayAgainBtn.IsVisible = false;
            PlayAgainBtn.Text = "Tekrar Oyna";
            List<string> animalEmoji = [
                "🐶", "🐶",
                "🐦", "🐦",
                "🐵", "🐵",
                "🐘", "🐘",
                "🐍", "🐍",
                "🐐", "🐐",
                "🦁", "🦁",
                "🐟", "🐟"];

            foreach (var button in AnimalButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Text = nextEmoji;
                button.BackgroundColor = Colors.LightBlue;
                animalEmoji.RemoveAt(index);
            }
            TimeElapsedLabel.IsVisible = true;
            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
        }
        int tenthsOfSecondsElapsed = 0;


        private bool TimerTick()
        {
            if (!this.IsLoaded) return false;
            tenthsOfSecondsElapsed += 1;
            TimeElapsedLabel.Text = "Zaman: " + (tenthsOfSecondsElapsed / 10f).ToString("0.0s");

            if (PlayAgainBtn.IsVisible) { tenthsOfSecondsElapsed = 0; return false; }
            return true;
        }

        bool findingMatch = false;
        int matchesFound = 0;
        private Button lastClicked;

        private void Button_Clicked(object sender, EventArgs e)
        {
            

            if (sender is Button buttonClicked)
            {
                // gönderen butonu artık buttonClicked olraka işleyebilriiz
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && (findingMatch == false))
                {
                    lastClicked = buttonClicked;
                    buttonClicked.BackgroundColor = Colors.Red;
                    findingMatch = true;
                }
                else
                {
                    if ((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text))
                    {
                        matchesFound++;
                        lastClicked.Text = " ";
                        buttonClicked.Text = " ";

                    }
                    lastClicked.BackgroundColor = Colors.LightBlue;
                    buttonClicked.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;
                }
            }

            if (matchesFound >= 8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainBtn.IsVisible = true;
            }

        }
    }
}