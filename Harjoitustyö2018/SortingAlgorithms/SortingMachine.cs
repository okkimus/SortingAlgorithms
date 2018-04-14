using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class SortingMachine
    {
        // Kuplalajittelualgoritmi
        public static void BubbleSort(int[] list)
        {
            for (int i = 1; i < list.Length; i++)
            {
                for (int j = 0; j < list.Length - i; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        int temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }

        }

        // Kekolajittelu
        public static void HeapSort(int[] list)
        {
            // Luodaan maksimikeko
            Heapify(list);

            int listSize = list.Length;
            int end = listSize - 1;

            // Niin kauan kun on lajiteltavia elementtejä.
            // end-muuttujassa on lajittelemattoman osan viimeinen alkio.
            while (end > 0)
            {
                // Viedään lajittelemattoman osan ensimmäinen alkio osan viimeiseksi.
                // Koska keko on maksimikeko, on ensmmäinen alkio suurin -> kuuluu siis viimeiseksi.
                SwapIndices(list, end, 0);

                end--;
                // Alkion siirrossa maksimikeko muuttuu virheelliseksi, joten korjata keko maksimikeoksi.
                SiftDown(list, 0, end);
            }
        }

        // Muokkaa listasta maksimikeon.
        public static void Heapify(int[] list)
        {
            int listSize = list.Length;
            // Etsitään keon viimeisen elementin vanhempi.
            int start = (listSize - 1) / 2;

            // Käydään kaikki elementit (viimeisen elementin vahemmasta) lähtien ylöspäin.
            while (start >= 0)
            {
                // Muodostetaan maksimikekorakennetta.
                SiftDown(list, start, listSize - 1);

                start--;
            }
        }

        // Keon korjausta.
        public static void SiftDown(int[] list, int start, int end)
        {
            // Lähtö solmu (listan indeksi)
            int root = start;

            // Niin kauan kun solmulla on vasen lapsi.
            while (root * 2 + 1 <= end)
            {
                int swap = root;
                int leftChild = swap * 2 + 1;
                int rightChild = swap * 2 + 2;
                
                // Selvitetään minkä indeksien elementtejä täytyy vaihtaa
                if (list[swap] < list[leftChild])
                {
                    swap = leftChild;
                }
                if (rightChild <= end && list[swap] < list[rightChild])
                {
                    swap = rightChild;
                }

                // Jos ei tarvitse vaihtaa mitään, palataan metodista.
                if (swap == root)
                {
                    return;
                }
                // Vaihdetaan elementtien paikkaa, ja pävitetään tarkasteltavaa solmua.
                SwapIndices(list, root, swap);
                root = swap;
            }
        }

        // Vaihtaa listan indekseissä a ja b olevien elementtien paikkaa.
        public static void SwapIndices(int[] list, int a, int b)
        {
            int temp = list[a];
            list[a] = list[b];
            list[b] = temp;
        }

        // Pikalajittelualgoritmi.
        // Param: list = lajiteltava lista
        //        lo = tarkasteltava aloituskohta (käytännössä käyttäjän kutsuessa metodia alku on: 0)
        //        hi = tarkasteltava lopetuskohta (käytännössä käyttäjän kutsuessa metodia alku on: list.Length - 1)
        public static void Quicksort(int[] list, int lo, int hi)
        {
            // Tarkistetaan ettei aloitus- ja lopetuskohta jo saavuttaneet toisensa.
            if (lo < hi)
            {
                // Kutsutaan partitio-metodia. Saadaan takaisin sarana-alkion oikea kohta.
                int p = Partition(list, lo, hi);
                // Suoritetaan lajittelu rekursiivisesti sarana-alkion vasemmalle ja oikealle puolelle.
                Quicksort(list, lo, p - 1);
                Quicksort(list, p, hi);
            }
        }

        // Partitio-metodi lajittelee tutkittavan osan alkiot niin, että ensimmäisessä osassa on sarana-alkiota pienemmät
        // ja toisessa osassa sitä suuremmat alkiot. Sarana-alkioksi tässä toteutuksessa valitaan tutkittavan osan viimeinen alkio.
        private static int Partition(int[] list, int lo, int hi)
        {
            int pivot = list[hi];
            int i = lo - 1;

            // Käydään alkiot läpi ja siirretään niitä tarvittaessa.
            for (int j = lo; j < hi; j++)
            {
                if (list[j] < pivot)
                {
                    // Päivitetään sarana-alkion tulevaa indeksiä ( - 1)
                    i++;
                    SwapIndices(list, j, i);
                }
            }
            // Vaihdetaan sarana-alkio sen oikealle paikalle.
            SwapIndices(list, i + 1, hi);
            // Palautetaan saranan indeksi, että listan jako onnistuu oikein.
            return i + 1;
        }
    }
}
