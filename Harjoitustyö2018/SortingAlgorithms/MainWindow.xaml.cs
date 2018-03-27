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
            Window1 listWindow = new Window1();

            if (list != null)
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
            list = ListMaker.RandomListMaker(ListLength());
            listCreatedNotification.Visibility = Visibility.Visible;
        }

        
        //Tämä metodi määrää lajiteltavan listan pituuden.
        //Käyttäjä valitsee haluamansa listan pituuden listLenghtComboBoxista.
        public int ListLength()
        {

            //Tägit listLengthComboBoxin itemeille annettuja joilla lasketaan listan pituus
            var tag = Convert.ToInt32(((ComboBoxItem)listLengthComboBox.SelectedItem).Tag.ToString());
            
            //Listan pituus on 2 potenssiin valitun ComboBoxItemin tägi
            //Esim. ensimmäisem itemin, eli "256 lukua", tag on 8 ---> value = 2^8
            value = Convert.ToInt32(Math.Pow(2, tag));
            
            return value;
        }
        
        
        //"Lajittele lista" -painikkeen toiminta
        //Lajittelee tallennetun listan käyttäjän valitsemalla lajittelualgoritmilla
        private void SortListButton_Click(object sender, RoutedEventArgs e)
        {
            SortingMachine.BubbleSort(list);
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

        private void AlgorithmInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 listWindow = new Window1();

            listWindow.listTextBlock.Text = "";

            listWindow.Show();
        }
    }
}
