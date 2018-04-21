using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static string Left(this string str, int Length)
        {
            return str.Substring(0, Length < str.Length ? Length : str.Length);
        }
        public static string Right(this string str, int Length)
        {
            return str.Substring(str.Length - Length < 0 ? 0 : str.Length - Length, str.Length - Length < 0 ? str.Length : Length);
        }
        public static List<T> ToList<T>(this ArrayList arrayList)
        {
            List<T> list = new List<T>(arrayList.Count);
            foreach (T instance in arrayList)
            {
                list.Add(instance);
            }
            return list;
        }

    }
}
