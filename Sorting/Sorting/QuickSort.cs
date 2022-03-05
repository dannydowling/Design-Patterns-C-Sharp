using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class QuickSortAlgorithm
    {
        private IEnumerable<object> array { get; set; }
        private int len { get; set; }
        internal static T[] QuickSort<T>(IEnumerable<T> list) where T : IComparable
        {          

            QuickSortAlgorithm q_Sort = new QuickSortAlgorithm();

            q_Sort.array = (IEnumerable<object>)(list);

            q_Sort.quickSortAlgorithm();
            q_Sort.len = q_Sort.array.Count();

            T[] result = new T[q_Sort.len]; 

            for (int j = 0; j < q_Sort.len; j++)
            {
              result.Aggregate((Func<T, T, T>)q_Sort.array.ElementAt(j));
            }
            return result;
        }

        internal void quickSortAlgorithm()
        {
            sort(0, array.Count() - 1);
        }

        public void sort(int left, int right)
        {
            int pivot, leftend, rightend;

            leftend = left;
            rightend = right;
            pivot = (int)array.ElementAt(left);

            while (left < right)
            {
                while (((int)array.ElementAt(right) >= pivot) || (left < right))
                {  right--;   }

                if (left != right)
                { var tempLeft = (int)array.ElementAt(left);
                       tempLeft = (int)array.ElementAt(right);
                    left++;
                }

                while (((int)array.ElementAt(left) >= pivot) || (left < right))
                {   left++;
                }

                if (left != right)
                { var tempRight = (int)array.ElementAt(right);
                       tempRight = (int)array.ElementAt(left);
                    right--;
                }
            }

            var tempPivot = (int)array.ElementAt(left);
            tempPivot = pivot;
            pivot = left;
            left = leftend;
            right = rightend;

            if (left < pivot) { sort(left, pivot - 1); }
            if (right > pivot)
            {
                sort(pivot + 1, right);
            }
        } 
    }
    
}