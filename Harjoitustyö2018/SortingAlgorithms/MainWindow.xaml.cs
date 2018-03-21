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

        private int value;
        private int[] list;
        private Boolean listSorted = false;


        public MainWindow()
        {
            InitializeComponent();
        }



        //"Näytä lista" painikkeen toiminta
        //Näyttää listan uudessa ikkunassa
        private void ShowListButton_Click(object sender, EventArgs e)
        {

            //Käynnistä uusi ikkuna, jossa on textBlock, johon lisätään luotu lista.
            Window1 listWindow = new Window1();

            if(list != null)
            {
                listWindow.listTextBlock.Text = string.Join(", ", list);
            }
            //listWindow.listaBlock.Text = string.Join(", ", list);
            listWindow.Show();

        }


        //"Luo lista" painikkeen toiminta
        //Metodi tallentaa int[] list -arrayhin random lukuja lajittelua varten
        private void MakeListButton_Click(object sender, RoutedEventArgs e)
        {
            RandomListMaker(ListLength());
            listCreatedNotification.Visibility = Visibility.Visible;
        }


        //Tämä metodi määrää lajiteltavan listan pituuden.
        //Käyttäjä valitsee haluamansa listan pituuden listLenghtComboBoxilla.
        public int ListLength()
        {

            //Tägit on asetettu Comboboxin itemeille MainWindow.xaml tiedostossa
            var tag = ((ComboBoxItem)listLenghtComboBox.SelectedItem).Tag.ToString();

            if (tag.Equals("1"))
            {
                value = 256;
            }

            else if (tag.Equals("2"))
            {
                value = 512;
            }

            else if (tag.Equals("3"))
            {
                value = 1024;
            }

            else if (tag.Equals("4"))
            {
                value = 2048;
            }

            return value;
        }


        //Tämä metodi luo n pituisen sekoitetun listan ilman duplikaatteja
        //Ajatuksena on, että parametriksi annetaan metodi ListLength()
        public int[] RandomListMaker(int n)
        {
            list = new int[n];
            Random rng = new Random();
            for (int i = 1; i < list.Length; i++)
            {
                int rngValue = rng.Next(1, n + 1);
                if (list.Contains(rngValue))
                {
                    i--;
                }
                else
                {
                    list[i] = rngValue;
                }


            }

            return list;
        }

       
        
        //"Lajittele lista" -painikkeen toiminta
        //Lajittelee tallennetun listan käyttäjän valitsemalla lajittelualgoritmilla
        private void SortListButton_Click(object sender, RoutedEventArgs e)
        {
            listSortedNotification.Visibility = Visibility.Visible;
            listSorted = true;
        }


        //"Näytä lajiteltu lista" -painikkeen toiminta
        //Näyttää lajiteltu lista uudessa ikkunassa showListButton_Click() metodin tyyliin
        private void ShowSortedListButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 listWindow = new Window1();

            if (listSorted == true && list != null)
            {
                listWindow.listTextBlock.Text = string.Join(", ", list);
            }

            else
            {
                listWindow.listTextBlock.Text = "Listaa ei ole vielä lajiteltu";
            }

            listWindow.Show();
        }
    }
}
