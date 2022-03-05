using System;
using System.Collections.Generic;

namespace DesignPatterns.Extensions
{
    public static class Composite_Match_To_Hash
    {
        // this compares the hashes of the objects in an array to an object passed in.
        // if there's a match, it will write out and return the objects that when composited together, 
        // match the hash of the object being matched against.

        public static T FactorOfK<T>(this T k, object[] candidates) where T : IComparable<T>
        {
            return compareToK(candidates, k);
        }

        private static dynamic compareToK(object[] candidates, object k)
        {
            
            var additive = FindAdditives(candidates, k);
            if (additive != null)
                Console.WriteLine($"Found two objects {additive.OP1} and {additive.OP2} that combine to {k}");
            return additive;
    }

        public static Additives FindAdditives(object[] numbers, object k)
        {
            var kStrung = k.GetHashCode();           
            SortedSet<int> sortedHashes = new SortedSet<int>();
            foreach(var number in numbers)
            {
                sortedHashes.Add(number.GetHashCode());
            }

            var hashSet = new HashSet<object>();
            
            foreach (var hash in sortedHashes)
            {
                var op1 = hash;
                var op2 = Math.Abs(kStrung - hash);

                if (hashSet.Contains(op2))
                    return new Additives(op1, op2);

                hashSet.Add(op2);
            }

            return null;
        }
    }


    public class Additives
    {
        public Additives(int op1, int op2)
        {
            OP1 = op1;
            OP2 = op2;
        }

        public int OP1 { get; }
        public int OP2 { get; }

    }
}



