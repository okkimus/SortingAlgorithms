using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class Timer
    {
        private Stopwatch sw = new Stopwatch();
        
        public long[] TimeAll(int repeats, int[] list)
        {
            long[] results = new long[3];

            for (int i = 0; i < 3; i++)
            {
                results[i] = TakeTime(repeats, i, list);
            }

            return results;
        }
        
        public long TakeTime(int repeats, int sortingAlgorithm, int[] list)
        {
            if (list.Length < 1)
            {
                return 0;
            }

            int[] warmUp = (int[])list.Clone();

            List<int[]> toBeSorted = new List<int[]>();
            for (int i = 0; i < repeats; i++)
            {
                toBeSorted.Add((int[]) list.Clone());
            }

            // Warm up JIT by running the sort once before timing
            if (sortingAlgorithm == 0)
            {
                SortingMachine.BubbleSort(warmUp);

                sw.Start();
                for (int i = 0; i < repeats; i++)
                {
                    SortingMachine.BubbleSort(toBeSorted[i]);
                }
                sw.Stop();
            }
            else if (sortingAlgorithm == 1)
            {
                SortingMachine.HeapSort(warmUp);

                sw.Start();
                for (int i = 0; i < repeats; i++)
                {
                    SortingMachine.HeapSort(toBeSorted[i]);
                }
                sw.Stop();
            }
            else if (sortingAlgorithm == 2)
            {
                SortingMachine.Quicksort(warmUp, 0, warmUp.Length - 1);

                sw.Start();
                for (int i = 0; i < repeats; i++)
                {
                    SortingMachine.Quicksort(toBeSorted[i], 0, list.Length - 1);
                }
                sw.Stop();
            }

            long timeElapsed = sw.ElapsedMilliseconds;
            sw.Reset();

            return timeElapsed;
        }
    }
}
