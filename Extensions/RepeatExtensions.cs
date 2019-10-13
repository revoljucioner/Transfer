using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class RepeatExtensions
    {
        public static List<T> Repeat<T>(this Func<T> func, int repeatCount)
        {
            var list = new List<T>();
            for (var i = 0; i < repeatCount; i++)
                list.Add(func());

            return list;
        }
    }
}
