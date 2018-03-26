using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class ListMaker
    {
        public static int[] RandomListMaker(int n)
        {
            int[] rndmList = new int[n];
            Random rng = new Random();

            for (int i = 0; i < rndmList.Length; i++)
            {
                int rngValue = rng.Next(0, n + 1);

                if (rndmList.Contains(rngValue))
                {
                    i--;
                }

                else
                {
                    rndmList[i] = rngValue;
                }


            }

            return rndmList;
        }
    }
}
