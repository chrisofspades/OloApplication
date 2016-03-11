using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OloApplication.Helpers
{
    public class ArrayComparer<T> : IEqualityComparer<T[]>
    {
        public bool Equals(T[] x, T[] y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(T[] obj)
        {
            return obj.Aggregate(string.Empty, (s, i) => s + i.GetHashCode(), s => s.GetHashCode());
        }
    }

}