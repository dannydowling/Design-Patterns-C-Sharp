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
            //First we'll sort the enumeration into a new structure so we can find anything in it
            SortedList<int, T> sortedListT = new SortedList<int, T>();

            for (int i = 0; i < list.Count() - 1; i++)
            {
                sortedListT.Add(i, list.ElementAt(i));
            }

            //we want to split this up to make searching faster
            SortedList<int, T> left = new SortedList<int, T>();
            SortedList<int, T> right = new SortedList<int, T>();

            for (int i = 0; i < sortedListT.Count(); i++)
            {
                if (i < list.Count() /2)
                {
                    left.Add(i, sortedListT.ElementAt(i).Value);
                }
                else
                {
                    right.Add(i, sortedListT.ElementAt(i).Value);
                }
            }
            

            // start in the middle of the list
            for (int i = sortedListT.Count() / 2; i < sortedListT.Count() - 1; i++)
            {
                T item = sortedListT.ElementAt(i).Value;  

                var comparison = key.CompareTo(item); 
                if (comparison == 0)
                {
                    return sortedListT.ElementAt(i).Value;  //if it's the one selected, then return
                }

                if (comparison < 0) //if the thing is in the left list
                {
                    right = new SortedList<int, T>(); //drop all the entries in the right
                    
                    for (int l = left.Count() / 2; l < left.Count() - 1; l--)
                    {
                        right.Add(l, left[l]);                   //fill up the right with the last part of the left list
                        left.RemoveAt(l);                        //and remove what is now in the right list from the left
                    }

                }
                else 
                {
                    left = new SortedList<int, T>();                   
                    for (int r = left.Count() / 2; r < right.Count() - 1; r++)
                    {
                        left.Add(r, right[r]); 
                        right.RemoveAt(r);
                    }
                }

                return list.ElementAt(i);
            }
            return default(T);
            
        }
    }
}
