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
        string geheimwoord;
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
            if (textwoord.Text == "")
            {
                MessageBox.Show($"Vul eerst een woord in");
            }
            else
            {
                generategeheimwoord();
                char[] ingaveCode = new char[geheimwoord.Length];
                for (int i = 0; i < geheimwoord.Length; i++)
                {
                    ingaveCode[i] = '*';
                }

                w1 = new Window1(this);
                w1.Geheimwoord = geheimwoord;
                w1.Ingavecode = ingaveCode;
                w1.textgeradenletters.Text = new string(ingaveCode);
                w1.Show();
                //this.Close();

            }
            textwoord.Text = "";
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
