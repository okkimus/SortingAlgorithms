using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class ListMaker
    {

        //RandomListMaker() luo listan sekalaisia lukuja, jonka ohjelma lajittelee käyttäjän valitsemalla algoritmilla
        public static int[] RandomListMaker(int n)
        {
            int[] rndmList = new int[n];
            Random rng = new Random();

            for (int i = 0; i < rndmList.Length; i++)
            {
                //rng.Next() ovat 0 ja n+1, jotta listan pienin alkio on 1 ja suurin on listan pituus
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
