using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Media;

namespace Rekenspels
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Random random = new Random();
        int getal1;
        int getal2;
        int difficulty = 0;
        string math;

        int number3P;
        int currentHp = 3;

        SoundPlayer correctSound = new SoundPlayer("Sounds/victory.wav");
        SoundPlayer wrongSound = new SoundPlayer("Sounds/lose.wav");
        bool soundEnabled = true;

        bool isPaused = false;

        DispatcherTimer dt = new DispatcherTimer();
        int counter = 99;

        string selectedOperator = "";

        void StartTimer()
        {
            if (dt.IsEnabled) return;

            counter = 99;
            timerTB.Text = counter.ToString();

            dt.Interval = TimeSpan.FromSeconds(1);

            dt.Tick -= dtTicker;
            dt.Tick += dtTicker;
            dt.Start();
        }

        private void dtTicker(object sender, EventArgs e)
        {
            if (counter > 0)
            {
                counter--;
                timerTB.Text = counter.ToString();
            }
            else
            {
                dt.Stop();
                MessageBox.Show("Tijd!");
            }
        }

        void MakeNewMath()
        {
            getal1 = random.Next(1, difficulty);
            int number1P = getal1;
            getal2 = random.Next(1, difficulty);
            int number2P = getal2;

            string math = selectedOperator;

            if (math == "") 
            {
                int choice = random.Next(1, 5);

                if (choice == 1) math = "+";
                else if (choice == 2) math = "-";
                else if (choice == 3) math = "*";
                else math = "/";
            }

            if (math == "+")
            {
                number3P = number1P + number2P;
            }
            else if (math == "-")
            {
                number3P = number1P - number2P;
            }
            else if (math == "*")
            {
                number3P = number1P * number2P;
            }
            else if (math == "/")
            {
                number2P = random.Next(1, difficulty); 
                number3P = number1P / number2P;
            }

            firstNum.Text = getal1.ToString();
            mathOp.Text = math;
            secondNum.Text = getal2.ToString();

            resultTB.Text = string.Empty;
        }

        void mathResult(int userAnswer)
        {
            Level.Text = (int.Parse(Level.Text) + 1).ToString();
            if (userAnswer == number3P)
            {
                score.Text = (int.Parse(score.Text) + 1).ToString();
                if (int.Parse(Record.Text) < int.Parse(score.Text))
                {
                    Record.Text = score.Text;
                }
                back.Background = new SolidColorBrush(Colors.Green);
                if(soundEnabled)
                {
                    correctSound.Play();
                }
            }
            else
            {
                back.Background = new SolidColorBrush(Colors.Red);
                if(soundEnabled)
                {
                    wrongSound.Play();
                    
                }

                currentHp--;
                hp.Text = currentHp.ToString();

                if (currentHp == 0) 
                {
                    GameOver();
                    
                }
            }
        }

        void GameOver()
        {
            dt.Stop();
            MessageBox.Show("Game over!");
            resultTB.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            currentHp = 3;
            back.Background = new SolidColorBrush(Colors.LightYellow);
            hp.Text = currentHp.ToString();
            if(difficulty == 0)
            {
                MessageBox.Show("Kies een moeilijkheidsgraad!");
                return;
            }
            score.Text = "0";
            MakeNewMath();
            StartTimer();
        }

        private void Check_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(resultTB.Text, out int userAnswer))
            {
                mathResult(userAnswer);
                MakeNewMath();

            }
        }
        private void Restart_Button_Click(object sender, RoutedEventArgs e)
        {
            currentHp = 3;
            hp.Text = currentHp.ToString();
            resultTB.IsEnabled = true;
            StartTimer();
            MakeNewMath();
            score.Text = "0";
            Level.Text = "0";
            counter = 100;
            back.Background = new SolidColorBrush(Colors.LightYellow);
        }

        private void resultTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Easy_Button_Click(object sender, RoutedEventArgs e)
        {
            difficulty = 11;
            Easy.Background = new SolidColorBrush(Colors.LightBlue);
            Normal.Background = new SolidColorBrush(Colors.Orange);
            Hard.Background = new SolidColorBrush(Colors.Red);
        }

        private void Mid_Button_Click(object sender, RoutedEventArgs e)
        {
            difficulty = 21;
            Easy.Background = new SolidColorBrush(Colors.Green);
            Normal.Background = new SolidColorBrush(Colors.LightBlue);
            Hard.Background = new SolidColorBrush(Colors.Red);
        }

        private void Hard_Button_Click(object sender, RoutedEventArgs e)
        {
            difficulty = 51;
            Easy.Background = new SolidColorBrush(Colors.Green);
            Normal.Background = new SolidColorBrush(Colors.Orange);
            Hard.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void Button_Sound(object sender, RoutedEventArgs e)
        {
            soundEnabled = !soundEnabled;

            if (soundEnabled)
            {
                soundBtn.Content = "🔊 ON";
                
            }
            else
            {
                soundBtn.Content = "🔇OFF";
            }
        }

        private void Button_NewSum(object sender, RoutedEventArgs e)
        {
            MakeNewMath();
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            if (!isPaused)
            {
                isPaused = true;
                dt.Stop();
                pauseBtn.Content = "▶";
                resultTB.IsEnabled = false;
                back.Background = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                isPaused = false;
                dt.Start();
                pauseBtn.Content = "⏸";
                resultTB.IsEnabled = true;
                back.Background = new SolidColorBrush(Colors.LightYellow);
            }


        }

        private void Button_Plus(object sender, RoutedEventArgs e)
        {
            selectedOperator = "+";


        }

        private void Button_Min(object sender, RoutedEventArgs e)
        {

            selectedOperator = "-";
        }

        private void Button_Keer(object sender, RoutedEventArgs e)
        {
            selectedOperator = "*";


        }

        private void Button_Del(object sender, RoutedEventArgs e)
        {

            selectedOperator = "/";

        }

        private void Button_Rand(object sender, RoutedEventArgs e)
        {
            selectedOperator = "";

        }
    }
}