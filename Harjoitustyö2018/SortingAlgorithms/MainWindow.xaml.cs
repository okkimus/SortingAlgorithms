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


        //"Suorita lajittelu" -painikkeen toiminta.
        //Lajittelee valitut listat 10 kertaa ja näyttää keskiarvon käyttäjälle.
        //Suoritusajat näytetään omassa ikkunassaan.
        private void CompareSortsButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 infoWindow = new Window1();

            String resultString = String.Format("Lajittelu suoritettu onnistuneesti ajassa. Lajittelu toistettu 30 kertaa satunnaisvaihtelun minimoimiseksi.\n" +
                                                "Tässä tulokset: \n\n");
            String hsResult;
            String bsResult;
            String qsResult;

            if (BubbleSortCheckBox.IsChecked.Value)
            {
                long bubbleSortResult = timer.TakeTime(30, 0, list);
                bsResult = "Kuplalajittelun kokonaisaika " + bubbleSortResult + "ms ja keskiarvo " + (double)bubbleSortResult / 30 + "ms,\n";
            }
            else
            {
                bsResult = "";
            }

            if (HeapSortCheckBox.IsChecked.Value)
            {
                long heapSortResult = timer.TakeTime(30, 1, list);
                hsResult = "Kekolajittelun kokonaisaika " + heapSortResult + "ms ja keskiarvo " + (double) heapSortResult / 30 + "ms,\n";
            }
            else
            {
                hsResult = "";
            }

            if (QuickSortCheckBox.IsChecked.Value)
            {
                long quickSortResult = timer.TakeTime(30, 2, list);
                qsResult = "Pikalajittelun kokonaisaika " + quickSortResult + "ms ja keskiarvo " + (double) quickSortResult / 30 + "ms,\n";
            }
            else
            {
                qsResult = "";
            }


            resultString += bsResult + hsResult + qsResult;

            infoWindow.listTextBlock.Text = resultString;
            
            listSorted = true;

            tempList = (int[])list.Clone();
            list = (int[])unsortedList.Clone();

            infoWindow.Show();
            
        }

        //INFOPAINIKKEIDEN TOIMINTA
        //Kysymysmerkkipainike avaa uuden ikkunan jossa kerrotaan muutamalla lauseella lajittelualgoritmista jonka vieressä infopainike on.
        

        //Kekoklajitteluinfo
        private void AlgorithmInfoButton1_Click(object sender, RoutedEventArgs e)
        {
            Window1 listWindow = new Window1
            {
                Title = "Algoritmi-info"
            };

            listWindow.listTextBlock.Text = "Kekolajittelu on kekorakenteeseen perustuva lajittelualgoritmi. Lajiteltavasta listasta" +
                    "muodostetaan maksimikeko, jonka muodostamisen jälkeen suurin alkio laitetaan listan viimeiseksi ja alkio poistetaan " +
                    "lajittelusta. Tämän jälkeen lajiteltava lista käsitellään uudestaan maksimikekoehdon varmistamiseksi ja tätä " +
                    "jatketaan kunnes lista on lajiteltu.";

            listWindow.Show();
        }


        //Kuplalajitteluinfo
        private void algorithmInfoButton2_Click(object sender, RoutedEventArgs e)
        {
            Window1 listWindow = new Window1
            {
                Title = "Algoritmi-info"
            };

            listWindow.listTextBlock.Text = "Kuplalajittelu on erittäin hidas algoritmi, joka käy koko lajiteltavan listan läpi" +
                        "vertaillen kutakin kahta peräkkäistä listan alkiota toisiinsa. Jos alkiot ovat väärässä järjestyksessä algoritmi " +
                        "vaihtaa alkiot keskenään. Kuplalajittelu on erittäin hidas, eikä se tarjoa mitään etuja muihin lajittelualgoritmeihin " +
                        "verratuna.";

            listWindow.Show();
        }


        //Pikalajitteluinfo
        private void algorithmInfoButton3_Click(object sender, RoutedEventArgs e)
        {
            Window1 listWindow = new Window1
            {
                Title = "Algoritmi-info"
            };

            listWindow.listTextBlock.Text = "Lomituslajittelussa lajiteltavaa listaa jaetaan pienempiin osajoukkoihin, jotka lajitellaan " +
                        "itsenäisesti jonka jälkeen osajoukot yhdistään takaisin yhdeksi listaksi. Lomituslajittelu on tehokas ja vakaa " +
                        "lajittelualgoritmi, mutta vaatii tavallisen vektorimuotoisen listan lajittelussa enemmän muistia. Tämä algoritmi " +
                        "on erityisen hyödyllinen linkitettyjen listojen järjestämisessä.";

            listWindow.Show();
        }
    } 
}

