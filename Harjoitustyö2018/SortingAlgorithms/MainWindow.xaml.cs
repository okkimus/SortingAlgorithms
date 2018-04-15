using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

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
            listWindow.Owner = this;

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
            listWindow.Owner = this;

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
            infoWindow.Owner = this;

            bool isSorted = false;

            // Luodaan uusi säie ikkunan näyttämiseksi
            Thread noticeThread = new Thread(() => {
                Window2 noticeWindow = new Window2();
                string notice = "Lajittelua suoritetaan! \n\nListan pituudesta riippuen tässä voi kestää hetki! :)";

                noticeWindow.noticeTextBlock.Text = notice;
                noticeWindow.Show();

                while (!isSorted)
                {
                    
                }
            });
            noticeThread.SetApartmentState(ApartmentState.STA);
            noticeThread.Start();

            String resultString = String.Format("Lajittelu suoritettu onnistuneesti ajassa. Lajittelu toistettu 30 kertaa satunnaisvaihtelun minimoimiseksi.\n" +
                                                "Tässä tulokset: \n\n");
            String hsResult;
            String bsResult;
            String qsResult;

            if (BubbleSortCheckBox.IsChecked.Value)
            {
                long bubbleSortResult = timer.TakeTime(30, 0, list);
                bsResult = "Kuplalajittelun kokonaisaika " + bubbleSortResult + "ms ja keskiarvo " + ShortenDouble((double) bubbleSortResult / 30) + "ms,\n";
            }
            else
            {
                bsResult = "";
            }

            if (HeapSortCheckBox.IsChecked.Value)
            {
                long heapSortResult = timer.TakeTime(30, 1, list);
                hsResult = "Kekolajittelun kokonaisaika " + heapSortResult + "ms ja keskiarvo " + ShortenDouble((double) heapSortResult / 30) + "ms,\n";
            }
            else
            {
                hsResult = "";
            }

            if (QuickSortCheckBox.IsChecked.Value)
            {
                long quickSortResult = timer.TakeTime(30, 2, list);
                qsResult = "Pikalajittelun kokonaisaika " + quickSortResult + "ms ja keskiarvo " + ShortenDouble((double) quickSortResult / 30) + "ms,\n";
            }
            else
            {
                qsResult = "";
            }

            resultString += bsResult + hsResult + qsResult;

            infoWindow.listTextBlock.Text = resultString;
            
            listSorted = true;

            // Lajitellaan lista vielä pääikkunan toiminnallisuutta varten.
            SortingMachine.Quicksort(tempList, 0, list.Length - 1);
            list = (int[]) unsortedList.Clone();

            isSorted = true;
            infoWindow.Show();
        }

        // Muokkaa luvusta merkkijonomuodon, jossa on enintään 2 desimaalia
        private string ShortenDouble(double number)
        {
            string numberString = number.ToString();
            int commaIndex = numberString.IndexOf(",");

            if (commaIndex + 3 < numberString.Length)
            {
                numberString = numberString.Substring(0, commaIndex + 3);
            }

            return numberString;
        }

        //INFOPAINIKKEIDEN TOIMINTA
        //Kysymysmerkkipainike avaa uuden ikkunan jossa kerrotaan muutamalla lauseella lajittelualgoritmista jonka vieressä infopainike on.
        

        //Kekoklajitteluinfo
        private void AlgorithmInfoButton1_Click(object sender, RoutedEventArgs e)
        {
            Window1 listWindow = new Window1
            {
                Title = "Algoritmi-info",
                Owner = this
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
                Title = "Algoritmi-info",
                Owner = this
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
                Title = "Algoritmi-info",
                Owner = this
            };

            listWindow.listTextBlock.Text = "Pikalajittelu on epävakaa lajittelualgoritmi, jossa lajiteltavasta joukosta yksi alkio vertailukohdaksi. " +
                "Alkio valitaan joko satunnaisesti, tai otetaan esimerkiksi joukon viimeinen alkio. Tätä valittua alkiota kutsutaan sarana-alkioksi (pivot), " +
                "koska se yhdistää aineiston eri osia. Jäljellä olevat alkiot jaetaan kahteen ryhmään sarana-alkiota käyttäen (esimerkiksi niin, että toiseen ryhmään " +
                "saranaa pienemmät ja toiseen saranaa suuremmat/yhtäsuuret). Tuloksena on tilanne, jossa löydetään sarana-alkion paikka edellä mainittujen ryhmien välistä." +
                "Ryhmille jatketaan lajittelua rekursiivisesti, kunnes lajittelu on valmis.";

            listWindow.Show();
        }
    } 
}

