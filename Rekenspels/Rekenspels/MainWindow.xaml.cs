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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        public void MakeNewTimer()
        {
            int timerGetal = 99;
            for(int i = 0; i > 0; timerGetal--)
            {
                timerGetal--;
                timerTB.Text = timerGetal.ToString();
            }



        }

        Random random = new Random();
        int getal1;
        int getal2;
        string math;


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
            getal1 = random.Next(1, 11);
            int getal1P = getal1;
            getal2 = random.Next(1, 11);
            int getal2P = getal2;
            int getal3 = 0;

            int choice = random.Next(1, 5);
            if (choice == 1)
            {
                math = "+";
                int getal3P = getal2P + getal1P;

            }
            else if (choice == 2)
            {
                math = "-";
                int getal3P = getal1P - getal2P;
            }
            else if (choice == 3)
            {
                math = "/";
                int getal3P = getal1P / getal2P;
            }
            else
            {
                math = "*";
                int getal3P = getal1P * getal2P;
            }

            sumTB.Text = getal1 + "" + math + "" + getal2;

            

        }
        void mathResult(int getal3)
        {

            int result = int.Parse(resultTB.Text);
            if (result != getal3)
            {
                Environment.Exit(0);

            }



        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MakeNewMath();
            StartTimer();

        }

        private void Button_Click_Rez(object sender, RoutedEventArgs e)
        {
               MakeNewMath();
               


        }

        private void resultTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}