using System;
using System.Collections.Generic;

namespace DerbyManagement.BLL
{
    static internal class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random();
            var n = list.Count;
            while (n > 1)
            {
                var k = random.Next(n--);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
