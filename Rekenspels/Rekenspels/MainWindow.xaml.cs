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

        DispatcherTimer dt = new DispatcherTimer();
        int counter = 99;

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

            int choice = random.Next(1, 5);
            if (choice == 1)
            {
                math = "+";
                number3P = number2P + number1P;
            }
            else if (choice == 2)
            {
                math = "-";
                number3P = number1P - number2P;
            }
            else if (choice == 3)
            {
                math = "/";
                number3P = number1P / number2P;
            }
            else
            {
                math = "*";
                number3P = number1P * number2P;
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
            }
            else
            {
                back.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
    }
}