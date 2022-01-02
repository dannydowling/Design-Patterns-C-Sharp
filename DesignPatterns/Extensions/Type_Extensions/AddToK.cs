﻿using System;
using System.Collections.Generic;

namespace DesignPatterns.Extensions
{
    public static class AddToK
    {
        public static dynamic FactorOfK(this object k, object[] candidates)
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
            SortedSet<int> sortedNumbers = new SortedSet<int>();
            foreach(var number in numbers)
            {
                sortedNumbers.Add(number.GetHashCode());
            }

            var hashSet = new HashSet<object>();
            
            foreach (var num in sortedNumbers)
            {
                var op1 = num;
                var op2 = Math.Abs(kStrung - num);

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


