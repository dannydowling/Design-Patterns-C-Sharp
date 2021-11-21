using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace DesignPatterns.Patterns
{
    internal class Binary_Search
    {
        private T BinarySearch<T>(IEnumerable<T> list, T key) where T : IComparable
        {           
            //we're going to need two sets of data to reduce quickly by
            SortedList<int, T> left = new SortedList<int, T>();
            SortedList<int, T> right = new SortedList<int, T>();

            for (int i = 0; i < list.Count(); i++)
            {
                if (i < list.Count() /2)
                {
                    left.Add(i, list.ElementAt(i));
                }
                else
                {
                    right.Add(i, list.ElementAt(i));
                }
            }
            

            // start in the middle of the list
            for (int i = list.Count() / 2; i < list.Count() - 1; i++)
            {
                T item = list.ElementAt(i);  

                var comparison = key.CompareTo(item); 
                if (comparison == 0)
                {
                    return list.ElementAt(i);  //if it's the one selected, then return
                }

                if (comparison < 0) //if the thing is in the left list
                {
                   //we're going to drop the right list.
                    for (int l = left.Count()/2; l < left.Count() - 1; l++)
                    {
                        right.Add(l, left[l]); //adding in all the last indexers from the left to the right
                        left.Remove(l);
                    }

                }
                else //or the item's in the right list
                {
                    //we're going to drop the right list.
                    for (int r = left.Count() / 2; r < right.Count() - 1; r++)
                    {
                        left.Add(r, right[r]); //adding in all the last indexers from the right to the left
                        right.Remove(r);
                    }
                }

                return list.ElementAt(i);
            }
            return default(T);
            
        }
    }
}
