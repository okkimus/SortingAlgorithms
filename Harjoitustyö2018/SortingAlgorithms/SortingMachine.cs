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

        public static void Heapify(int[] list, int index)
        {
            
        }

        public static void ShiftDown(int[] list, int start, int end)
        {
            int root = start;
            int listSize = list.Length;
            int leftChild = root * 2 + 1;
            int rightChild  = root * 2 + 2;

            while (leftChild <= listSize)
            {

                //if (list[start] < list[0]);
            }
        }
    }
}
