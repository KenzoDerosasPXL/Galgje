using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;



namespace Galgje

{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {



        public string[] Score { get; set; }
        public string Geheimwoord { get; set; }
        public int Second { get; set; }
        public int time { get; set; }
        public char[] Ingavecode { get; set; }
        string juisteletters = "";
        string fouteletters = "";
        int teller = 0;
        private DispatcherTimer Timer;
        private string[] imagesNames = new string[10] { "foto1", "foto2", "foto3", "foto4", "foto5", "foto6", "foto7", "foto8", "foto9", "foto10" };

        string hint = "";
        string hint_letters = "";
        

        


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
            else if (time == 0 && teller < 10)
            {
                Timer.Stop();
                time = Second;
                GameImage.Source = new BitmapImage(new Uri($"images\\{imagesNames[teller]}.png", UriKind.Relative));
                teller++;
                levens(teller);
                Timer.Start();

            }
            if (teller >= 10)
            {
                Timer.Stop();
                MessageBox.Show($"uit levens {teller}");
                this.Close();
            }
        }
        
        private void raad()
        {
            hint = "";
            for (int i = 0; i < Geheimwoord.Length; i++)
            {
                if (!hint.Contains(Geheimwoord.Substring(i, 1)))
                    hint += Geheimwoord.Substring(i, 1);
            }

            hint += hint_letters;
            Random getal = new Random();
            char letter = (char)getal.Next(97, 123);
            if (26 == hint.Length)
            {
                Timer.Stop();
                MessageBox.Show($"alle hints gebruikt, nog niet geraden?");
                Timer.Start();
            }
            else
            {
                while (hint.Contains(letter))
                {
                    letter = (char)getal.Next(97, 123);
                }
                Timer.Stop();
                MessageBox.Show($"{letter}");
                Timer.Start();
                hint_letters += letter;
            }
        }

    private void tijd()
        {
            time = Second;
            Timer.Start();
        }
        private void levens(int teller)
        {
            textlevens.Text = $"{10 - teller} levens";
        }

        private void Raad_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string antwoord = textraadletter.Text.ToLower();
            string[] letters = new string[Geheimwoord.Length];
            for (int i = 0; i < Geheimwoord.Length; i++)
            {
                letters[i] = Geheimwoord.Substring(i, 1);
            }
            if (teller < 10)
            {
                if (antwoord.Length >= 2)
                {
                    if (antwoord == Geheimwoord.ToLower())
                    {
                        textgeradenletters.Text = antwoord;
                        MessageBox.Show($"je hebt het woord geraden");
                        for (int i = 0; i < 3; i++)
                        {
                            if (Score[i] == "")
                            {
                                Score[i] = $"{10 - teller} levens ({DateTime.Now.ToLongTimeString()})";
                                break;
                            }

                            else if (Convert.ToInt32(Score[i].Substring(0, 2).Trim()) < 10 - teller)
                            {
                                Score[i] = $"{10 - teller} levens ({DateTime.Now.ToLongTimeString()})";
                                break;
                            }
                        };
                        this.Close();

                    }
                    else
                    {
                        Timer.Stop();
                        MessageBox.Show("fout woord");
                        fouteletters += antwoord;
                        GameImage.Source = new BitmapImage(new Uri($"images\\{imagesNames[teller]}.png", UriKind.Relative));
                        teller++;
                        levens(teller);
                        tijd();

                    }
                }
                else
                {
                    if (Geheimwoord.Contains(antwoord))
                    {

                        if (juisteletters.Contains(antwoord))
                        {
                            Timer.Stop();
                            MessageBox.Show("Deze juiste letter is al geraden");
                            Timer.Start();
                        }
                        else
                        {
                            for (int i = 0; i < Geheimwoord.Length; i++)
                            {
                                for (int x = 0; x < (antwoord).Length; x++)
                                {
                                    if (letters[i].Contains((antwoord).Substring(x, 1)))
                                    {
                                        Ingavecode[i] = antwoord[0];
                                        textgeradenletters.Text = new string(Ingavecode);
                                        juisteletters += antwoord;
                                        tijd();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (fouteletters.Contains(antwoord))
                        {
                            Timer.Stop();
                            MessageBox.Show("Deze foute letter is al ingevoegd");
                            Timer.Start();
                        }
                        else
                        {
                            Timer.Stop();
                            MessageBox.Show("foute letter");
                            fouteletters += antwoord;
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
                }
            }
            if (juisteletters.Length == Geheimwoord.Length)
            {
                MessageBox.Show($"je hebt het woord geraden ");
                //hier
                
                for (int i = 0; i < 3; i++)
                {
                    if (Score[i] == "")
                    {
                        Score[i] = $"{10 - teller} levens ({DateTime.Now.ToLongTimeString()})";
                        break;
                    }
                    
                    else if (Convert.ToInt32(Score[i].Substring(0,2).Trim()) < 10 - teller)
                    {
                        Score[i] = $"{10 - teller} levens ({DateTime.Now.ToLongTimeString()})";
                        break;
                    }
                };

                this.Close();
            }
            textraadletter.Text = "";
        }
        private void Stop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }



        
        private void Raad_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (GetTemplateChild == 0)
            //{
            //    return null;
            //}
            // else
            //{
            // GetTemplateChild.BorderBrush = Brushes.Gray;
            //}
            //Raad.BorderBrush = Brushes.Gray;
            //Raad.Background = Brushes.LightBlue;
        }

        private void Raad_MouseLeave(object sender, MouseEventArgs e)
        {
            Raad.BorderBrush = Brushes.Black;
            Raad.Background = Brushes.White;
        }

        private void Hint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            raad();
        }

        private void MenuItemAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(1);
        }
        private void MenuItemNieuw_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void MenuItemHint_Click(object sender, RoutedEventArgs e)
        {
            raad();
        }

        private void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            Timer.Stop();
        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            Timer.Start();
        }
        public string[] score123
        {
            get { return Score; }
        }
    }
}
