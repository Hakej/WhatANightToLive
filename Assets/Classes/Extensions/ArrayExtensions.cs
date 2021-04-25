using System;
using System.Collections.Generic;

namespace Classes.Extensions
{
    public static class ArrayExtensions
    {
        private static Random _rand = new Random();

        public static T GetRandomElement<T>(this T[] items)
        {
            if (items.Length == 0)
            {
                return default(T);
            }
            else
            {
                return items[_rand.Next(0, items.Length)];
            }
        }

        public static T GetRandomElement<T>(this List<T> items)
        {
            if (items.Count == 0)
            {
                return default(T);
            }
            else
            {
                return items[_rand.Next(0, items.Count)];
            }
        }
    }
}