using System;

namespace elp87.Helpers
{
    public static class ArrayGeneric
    {
        public static T[] Concat<T>(T[] baseArray, T[] additionalArray)
        {
            int newArrayLength = baseArray.Length + additionalArray.Length;
            T[] newArray = new T[newArrayLength];

            Array.Copy(baseArray, newArray, baseArray.Length);
            Array.Copy(additionalArray, 0, newArray, baseArray.Length, additionalArray.Length);

            return newArray;
        }
    }
}
