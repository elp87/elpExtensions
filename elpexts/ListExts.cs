using System.Collections.Generic;

namespace elp87.Helpers
{
    public static class ListExts
    {
        public static List<TSource> getRange<TSource>(this List<TSource> fullList, TSource start, TSource end)
        {
            int startIndex = fullList.IndexOf(start);
            int endIndex = fullList.IndexOf(end);
            int range = endIndex - startIndex + 1;
            List<TSource> rangeList = fullList.GetRange(startIndex, range);
            return rangeList;
        }
    }
}
