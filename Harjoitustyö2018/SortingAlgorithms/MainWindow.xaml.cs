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

namespace SortingAlgorithms
{

    public partial class MainWindow : Window
    {

        protected int arvo;
        protected int[] lista;

        public MainWindow()
        {
            InitializeComponent();
        }

        //"Luo lista" painikkeen toiminta
        public void PrintButton_Click(object sender, EventArgs e)
        {

            //Käynnistä uusi ikkuna, jossa on textBlock, johon lisätään luotu lista.
            Window1 listaIkkuna = new Window1();
            listaIkkuna.listaBlock.Text = string.Join(", ", RandomLista(ListaPituus()));
            listaIkkuna.Show();

        }


        //Tämä metodi määrää lajiteltavan listan pituuden.
        //Käyttäjä valitsee haluamansa listan pituuden listLenghtComboBoxilla.
        public int ListaPituus()
        {

            //Tägit on asetettu Comboboxin itemeille MainWindow.xaml tiedostossa
            var tägi = ((ComboBoxItem)listLenghtComboBox.SelectedItem).Tag.ToString();

            if (tägi.Equals("1"))
            {
                arvo = 256;
            }

            else if (tägi.Equals("2"))
            {
                arvo = 512;
            }

            else if (tägi.Equals("3"))
            {
                arvo = 1024;
            }

            else if (tägi.Equals("4"))
            {
                arvo = 2048;
            }

            return arvo;
        }


        //Tämä metodi luo n pituisen sekoitetun listan ilman duplikaatteja
        //Ajatuksena on, että parametriksi annetaan metodi ListaPituus()
        public int[] RandomLista(int n)
        {
            lista = new int[n];
            Random rng = new Random();
            for (int i = 1; i < lista.Length; i++)
            {
                int rngArvo = rng.Next(1, n + 1);
                if (lista.Contains(rngArvo))
                {
                    i--;
                }
                else
                {
                    lista[i] = rngArvo;
                }


            }

            return lista;
        }
    }
}
