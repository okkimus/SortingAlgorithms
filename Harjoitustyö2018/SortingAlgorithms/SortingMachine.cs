using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class SortingMachine
    {
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

        public static void HeapSort(int[] list)
        {
            Heapify(list);
            int listSize = list.Length;

            int end = listSize - 1;

            while (end > 0)
            {
                SwapIndices(list, end, 0);

                end--;

                SiftDown(list, 0, end);
            }
        }

        public static void Heapify(int[] list)
        {
            int listSize = list.Length;
            // find the parent on last node
            int start = (listSize - 1) / 2;

            while (start >= 0)
            {
                SiftDown(list, start, listSize - 1);

                start--;
            }
        }

        public static void SiftDown(int[] list, int start, int end)
        {
            int root = start;

            while (root * 2 + 1 <= end)
            {
                int swap = root;
                int leftChild = swap * 2 + 1;
                int rightChild = swap * 2 + 2;
                
                if (list[swap] < list[leftChild])
                {
                    swap = leftChild;
                }
                if (rightChild <= end && list[swap] < list[rightChild])
                {
                    swap = rightChild;
                }

                if (swap == root)
                {
                    return;
                }
                SwapIndices(list, root, swap);
                root = swap;
            }
        }

        public static void SwapIndices(int[] list, int a, int b)
        {
            int temp = list[a];
            list[a] = list[b];
            list[b] = temp;
        }

        public static void Quicksort(int[] list, int lo, int hi)
        {
            if (lo < hi)
            {
                int p = Partition(list, lo, hi);
                Quicksort(list, lo, p - 1);
                Quicksort(list, p, hi);
            }
        }

        private static int Partition(int[] list, int lo, int hi)
        {
            int pivot = list[hi];
            int i = lo - 1;

            for (int j = lo; j < hi; j++)
            {
                if (list[j] < pivot)
                {
                    i++;
                    SwapIndices(list, j, i);
                }
            }
            SwapIndices(list, i + 1, hi);
            return i + 1;
        }
    }
}
