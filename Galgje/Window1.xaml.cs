using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Galgje

{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public string Geheimwoord { get; set; }
        public char[] Ingavecode { get; set; }
        string juisteletters = "";
        string fouteletters = "";
        int teller = 0;
        private DispatcherTimer Timer;
        private string[] imagesNames = new string[10] { "foto1", "foto2", "foto3", "foto4", "foto5", "foto6", "foto7", "foto8", "foto9", "foto10" };
        int time = 11;
    public Window1()
        {
            InitializeComponent();
        }
        
            public Window1(MainWindow wd)
        {
            InitializeComponent();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                LblTijd.Content = $"{time}";
            }
            else
            {
                Timer.Stop();
                time = 11;
                GameImage.Source = new BitmapImage(new Uri($"images\\{imagesNames[teller]}.png", UriKind.Relative));
                teller++;
                levens(teller);
                
            }
        }
        private void tijd()
        {
            time = 11;
            Timer.Start();
        }
        private void levens(int teller)
            {
                textlevens.Text = $"{10 - teller} levens";
            }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string[] letters = new string[Geheimwoord.Length];
            for (int i = 0; i < Geheimwoord.Length; i++)
            {
                letters[i] = Geheimwoord.Substring(i, 1);
            }
            if (teller < 10)
            {
                if (textraadletter.Text.Length >= 2)
                {
                    if (textraadletter.Text == Geheimwoord)
                    {
                        textgeradenletters.Text = textraadletter.Text;
                        MessageBox.Show($"je hebt het woord geraden");
                        this.Close();

                    }
                    else
                    {
                        Timer.Stop();
                        MessageBox.Show("fout woord");
                        fouteletters += textraadletter.Text;
                        GameImage.Source = new BitmapImage(new Uri($"images\\{imagesNames[teller]}.png", UriKind.Relative));
                        teller++;
                        levens(teller);
                        tijd();

                    }
                }
                else
                {
                    if (Geheimwoord.Contains(textraadletter.Text))
                    {

                        if (juisteletters.Contains(textraadletter.Text))
                        {
                            Timer.Stop();
                            MessageBox.Show("Deze juiste letter is al geraden");
                            Timer.Start();
                        }
                        else
                        {
                            for (int i = 0; i < Geheimwoord.Length; i++)
                            {
                                for (int x = 0; x < (textraadletter.Text).Length; x++)
                                {
                                    if (letters[i].Contains((textraadletter.Text).Substring(x, 1)))
                                    {
                                        Ingavecode[i] = textraadletter.Text[0];
                                        textgeradenletters.Text = new string(Ingavecode);
                                        juisteletters += textraadletter.Text;
                                        tijd();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (fouteletters.Contains(textraadletter.Text))
                        {
                            Timer.Stop();
                            MessageBox.Show("Deze foute letter is al ingevoegd");
                            Timer.Start();
                        }
                        else
                        {
                            Timer.Stop();
                            MessageBox.Show("foute letter");
                            fouteletters += textraadletter.Text;
                            GameImage.Source = new BitmapImage(new Uri($"images\\{imagesNames[teller]}.png", UriKind.Relative));
                            teller++;
                            levens(teller);
                            tijd();
                        }
                    }
                }
                if (teller >= 10)
                {
                    MessageBox.Show($"uit levens {teller}");
                    this.Close();
                    //MainWindow.Show();
                }
            }
            if (juisteletters.Length == Geheimwoord.Length)
            {
                MessageBox.Show($"je hebt het woord geraden ");
                this.Close();
                //MainWindow.Show();
            }
            textraadletter.Text = "";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            //MainWindow.Show();
        }
    }
}
