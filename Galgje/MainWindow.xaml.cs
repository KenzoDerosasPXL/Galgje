using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Galgje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string SCORE;
        string geheimwoord;
        int second;
        string[] test;
        int teller = 1;
        private string[] score = new string[3] { "", "", ""};

        private string[] galgjeWoorden = new string[]
                {
                    "grafeem", "tjiftjaf", "maquette", "kitsch", "pochet", "convocaat", "jakkeren", "collaps", "zuivel", "cesium", "voyant", "spitten", "pancake", "gietlepel",
                    "karwats", "dehydreren", "viswijf", "flater", "cretonne", "sennhut", "tichel", "wijten", "cadeau", "trotyl", "chopper", "pielen", "vigeren", "vrijuit", "dimorf",
                    "kolchoz","janhen","plexus","borium","ontweien","quiche","ijverig","mecenaat","falset","telexen","hieruit","femelaar","cohesie","exogeen","plebejer","opbouw","zodiak",
                    "volder","vrezen","convex","verzenden","ijstijd","fetisj","gerekt","necrose","conclaaf","clipper","poppetjes","looikuip","hinten","inbreng","arbitraal","dewijl",
                    "kapzaag","welletjes","bissen","catgut","oxymoron","heerschaar","ureter","kijkbuis","dryade","grofweg","laudanum","excitatie","revolte","heugel","geroerd","hierbij",
                    "glazig","pussen","liquide","aquarium","formol","kwelder","zwager","vuldop","halfaap","hansop","windvaan","bewogen","vulstuk","efemeer","decisief","omslag","prairie",
                    "schuit","weivlies","ontzeggen","schijn","sousafoon"
                };

        Window1 w1;

        public MainWindow()
        { 
            InitializeComponent();

        }
        public void generategeheimwoord()
        {
            geheimwoord = textwoord.Text;
            
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((w1) == null)
            {
                
            }
            else
            {
                test = w1.score123;
            }
            if (textseconden.Text == "")
            {
                second = 11;
            }
            else if (5 <= Convert.ToInt32(textseconden.Text) && Convert.ToInt32(textseconden.Text) <= 20)
            {
                second = Convert.ToInt32(textseconden.Text) + 1;
            }
            else
            {
                MessageBox.Show("tijd moet tussen 5 en 20");
                textseconden.Text = "";
            }
            if (textwoord.Text == "")
            {

                MessageBox.Show($"Vul eerst een woord in ");
            }
            else if (textwoord.Text != "" && second != 0)
            {
                generategeheimwoord();
                char[] ingaveCode = new char[geheimwoord.Length];
                for (int i = 0; i < geheimwoord.Length; i++)
                {
                    ingaveCode[i] = '*';
                }
                teller = 0;
                w1 = new Window1(this);
                w1.Geheimwoord = geheimwoord.ToLower();
                w1.Ingavecode = ingaveCode;
                w1.Second = second;
                w1.time = second;
                w1.textgeradenletters.Text = new string(ingaveCode);
                w1.Score = score;
                w1.ShowDialog();
                
                
            }
            second = 0;
            textwoord.Text = "";
            textseconden.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (textseconden.Text == "")
            {
                second = 11;

            }
            else if (5 <= Convert.ToInt32(textseconden.Text) && Convert.ToInt32(textseconden.Text) <= 20)
            {
                second = Convert.ToInt32(textseconden.Text) + 1;
            }
            else
            {
                MessageBox.Show("tijd moet tussen 5 en 20");
                textseconden.Text = "";

            }

            if (second != 0)
            {
                Random getal = new Random();
                geheimwoord = galgjeWoorden[(int)getal.Next(0, galgjeWoorden.Length + 1)]; // tussen 0 en 100
                char[] ingaveCode = new char[geheimwoord.Length];
                for (int i = 0; i < geheimwoord.Length; i++)
                {
                    ingaveCode[i] = '*';
                }

                w1 = new Window1(this);
                w1.Geheimwoord = geheimwoord.ToLower();
                w1.Ingavecode = ingaveCode;
                w1.Second = second;
                w1.time = second;
                w1.textgeradenletters.Text = new string(ingaveCode);
                w1.ShowDialog();
                //this.Close();

            }
            textseconden.Text = "";
            textwoord.Text = "";
            second = 0;

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
        private void MenuItemscore_Click(object sender, RoutedEventArgs e)
        {
            if ((w1) == null)
            {
                
            }
            else if (teller == 0 && w1 != null)
            {
                test = w1.score123;
                SCORE = "";
                for (int i = 0; i < 3; i++)
                {
                    SCORE += $"{Convert.ToString(score[i])}\n";
                }
                teller++;
                
            }
            
            MessageBox.Show($"{SCORE}");
        }

    }
}
