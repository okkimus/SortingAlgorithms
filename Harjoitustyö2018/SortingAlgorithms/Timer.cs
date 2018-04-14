using System.Collections.Generic;
using System.Diagnostics;

namespace SortingAlgorithms
{
    class Timer
    {
        private Stopwatch sw = new Stopwatch();
        
        // Ottaa ajan kaikilla kolmella lajittelualgoritmilla. Palauttaa listan, jossa tulokset.
        public long[] TimeAll(int repeats, int[] list)
        {
            long[] results = new long[3];

            for (int i = 0; i < 3; i++)
            {
                results[i] = TakeTime(repeats, i, list);
            }

            return results;
        }
        
        // Ottaa aikaa lajittelualgoritmin suorituksen kestosta.
        // Param: repeats = lajittelukertojen toistomäärä
        //        sortinAlgorithm = käytettävän algoritmin numero
        //        list = lajiteltava lista
        public long TakeTime(int repeats, int sortingAlgorithm, int[] list)
        {
            // Jos listalla on 0 alkiota, palautetaan 0 ja palataan metodista.
            if (list.Length < 1)
            {
                return 0;
            }

            // Luodaan JIT:in lämmittelemiseen käytettävä lista
            int[] warmUp = (int[])list.Clone();

            // Luodaan lista, johon kopioidaan elementeiksi lajiteltavaa listaa (ei haluta lajitella alkuperäistä listaa sivuvaikutuksena)
            List<int[]> toBeSorted = new List<int[]>();
            for (int i = 0; i < repeats; i++)
            {
                toBeSorted.Add((int[]) list.Clone());
            }

            // Valitaan oikea lajittelualgoritmi ja lämmitetään JIT suorittamalla algoritmi kerran.
            // Sen jälkeen toistetaan algortimi "repeats" lukumäärän verran.
            if (sortingAlgorithm == 0)
            {
                SortingMachine.BubbleSort(warmUp);

                // Aloitetaan ajastus
                sw.Start();
                for (int i = 0; i < repeats; i++)
                {
                    SortingMachine.BubbleSort(toBeSorted[i]);
                }
                // Lopetetaan ajastus
                sw.Stop();
            }
            else if (sortingAlgorithm == 1)
            {
                SortingMachine.HeapSort(warmUp);

                // Aloitetaan ajastus
                sw.Start();
                for (int i = 0; i < repeats; i++)
                {
                    SortingMachine.HeapSort(toBeSorted[i]);
                }
                // Lopetetaan ajastus
                sw.Stop();
            }
            else if (sortingAlgorithm == 2)
            {
                SortingMachine.Quicksort(warmUp, 0, warmUp.Length - 1);

                // Aloitetaan ajastus
                sw.Start();
                for (int i = 0; i < repeats; i++)
                {
                    SortingMachine.Quicksort(toBeSorted[i], 0, list.Length - 1);
                }
                // Lopetetaan ajastus
                sw.Stop();
            }

            long timeElapsed = sw.ElapsedMilliseconds;
            // Reset the StopWatch
            sw.Reset();

            return timeElapsed;
        }
    }
}
