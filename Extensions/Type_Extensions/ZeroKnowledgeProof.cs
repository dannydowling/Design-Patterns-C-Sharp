using System;
using System.Collections.Generic;

namespace DesignPatterns.Extensions
{
    public static class ZeroKnowledgeProof
    {
        // a zero knowledge proof is a proof that something exists by comparing 
        // it to a candidate. In this, I'm only returning the hashcode of the thing, if it succeeds.

        public static int Compare<T>(T sourceObject, object candidate) where T : IComparable<T>
        {
            if (candidate != null)
            {
                object[] objectasArray = candidate as object[];

               return Factor_Of.FactorOfK(sourceObject, objectasArray).GetHashCode();
            }
            return 0;
        }
    }
}
   