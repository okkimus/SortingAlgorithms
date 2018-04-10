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
        private int[] unsortedList;
        private int[] tempList;
        private Boolean listSorted = false;
        private Timer timer = new Timer();

        public MainWindow()
        {
            InitializeComponent();
        }


        
        //"Luo lista" -painikkeen toiminta.
        //Tallentaa list -arrayhin listan sekalaisia lukuja, sekä kloonaa listan unsortedList ja tempList listoihin.
        //unsortedList on olemassa jotta "Näytä lista" -painike näyttäisi alkuperäisen, lajittelemattoman, listan.
        //tempList tulee käyttöön SortListButton_Click metodissa.
        private void MakeListButton_Click(object sender, RoutedEventArgs e)
        {
            list = ListMaker.RandomListMaker(ListLength());
            unsortedList = (int[])list.Clone();
            tempList = (int[])list.Clone();
            listCreatedNotification.Visibility = Visibility.Visible;
        }


        //"Näytä lista" painikkeen toiminta
        //Näyttää lajittelemattoman listan uudessa ikkunassa
        private void ShowListButton_Click(object sender, EventArgs e)
        {
            Window1 listWindow = new Window1();

            if (unsortedList != null)
            {
                listWindow.listTextBlock.Text = string.Join(", ", unsortedList);
            }

            listWindow.Show();

        }


        //Tämä metodi määrää lajiteltavan listan pituuden.
        //Käyttäjä valitsee haluamansa listan pituuden listLenghtComboBoxista.
        public int ListLength()
        {

            //Jokaiselle listLengthComboBoxin itemille annettu uniikki Tag.
            //Itemien Tagit ovat kokonaislukuja joidenka avulla listan pituus voidaan laskea.
            var tag = Convert.ToInt32(((ComboBoxItem)listLengthComboBox.SelectedItem).Tag.ToString());
            
            //Listan pituus on 2 potenssiin valitun ComboBoxItemin tägi.
            //Esim. ensimmäisem itemin, eli "256 lukua", tag on 8 ---> value = 2^8.
            value = Convert.ToInt32(Math.Pow(2, tag));
            
            return value;
        }
        
        
        //"Lajittele lista" -painikkeen toiminta.
        //Lajittelee tallennetun listan käyttäjän valitsemalla lajittelualgoritmilla.
        private void SortListButton_Click(object sender, RoutedEventArgs e)
        {
            SortingMachine.Quicksort(list, 0, list.Length - 1);
            listSortedNotification.Visibility = Visibility.Visible;
            listSorted = true;

            //Lajiteltu lista kloonataan tempList:iin ja list:iin kloonataan unsortedList.
            //Tämä sen takia, jotta käyttäjän ei tarvitse aina luoda uutta listaa jokaiselle algoritmille.
            tempList = (int[])list.Clone();
            list = (int[])unsortedList.Clone();
        }


        //"Näytä lajiteltu lista" -painikkeen toiminta.
        //Näyttää lajiteltu lista uudessa ikkunassa showListButton_Click() metodin tyyliin.
        private void ShowSortedListButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 listWindow = new Window1();

            if (listSorted == true && list != null)
            {
                listWindow.listTextBlock.Text = string.Join(", ", tempList);
            }

            else
            {
                listWindow.listTextBlock.Text = "Listaa ei ole vielä lajiteltu";
            }

            listWindow.Show();
        }

        private void CompareSortsButton_Click(object sender, RoutedEventArgs e)
        {
            long[] results = timer.TimeAll(30, list);

            Window1 infoWindow = new Window1();

            String resultString = String.Format("The list was sorted with three different sorting algorithms. \n"
                + "Times of execution:\n"
                + "Bubblesort {0} ms,\n"
                + "Heapsort {1} ms,\n"
                + "Quicksort {2} ms,\n",
                results[0], results[1], results[2]);

            infoWindow.listTextBlock.Text = resultString;

            infoWindow.Show();
        }

        //Kysymysmerkki-painikkeen toiminta.
        //Painike avaa ikkunan jossa on selitys miten valittu lajittelualgoritmi toimii.
        private void AlgorithmInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 listWindow = new Window1
            {
                Title = "Algoritmi-info"
            };

            var algorithmTag = ((ComboBoxItem)algorithmComboBox.SelectedItem).Tag.ToString();

            //Kekolajittelu info.
            if (algorithmTag.Equals("A"))
            {
                listWindow.listTextBlock.Text = "Kekolajittelu on kekorakenteeseen perustuva lajittelualgoritmi. Lajiteltavasta listasta" +
                    "muodostetaan maksimikeko, jonka muodostamisen jälkeen suurin alkio laitetaan listan viimeiseksi ja alkio poistetaan " +
                    "lajittelusta. Tämän jälkeen lajiteltava lista käsitellään uudestaan maksimikekoehdon varmistamiseksi ja tätä " +
                    "jatketaan kunnes lista on lajiteltu.";
            }

            //Kuplalajittelu info.
            else if (algorithmTag.Equals("B"))
            {
                listWindow.listTextBlock.Text = "Kuplalajittelu on erittäin hidas algoritmi, joka käy koko lajiteltavan listan läpi" +
                    "vertaillen kutakin kahta peräkkäistä listan alkiota toisiinsa. Jos alkiot ovat väärässä järjestyksessä algoritmi " +
                    "vaihtaa alkiot keskenään. Kuplalajittelu on erittäin hidas, eikä se tarjoa mitään etuja muihin lajittelualgoritmeihin " +
                    "verratuna.";
            }

            //Lomituslajittelu info.
            else if (algorithmTag.Equals("C"))
            {
                listWindow.listTextBlock.Text = "Lomituslajittelussa lajiteltavaa listaa jaetaan pienempiin osajoukkoihin, jotka lajitellaan " +
                    "itsenäisesti jonka jälkeen osajoukot yhdistään takaisin yhdeksi listaksi. Lomituslajittelu on tehokas ja vakaa " +
                    "lajittelualgoritmi, mutta vaatii tavallisen vektorimuotoisen listan lajittelussa enemmän muistia. Tämä algoritmi " +
                    "on erityisen hyödyllinen linkitettyjen listojen järjestämisessä.";
            }

            //Algoritmien selityksien lähteenä käytetty Wikipediaa.
            listWindow.Show();
        }
    }
}
