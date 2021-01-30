using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer
{
    public static class Paginator
    {
        /// <summary>
        /// Split large lists into pages.
        /// </summary>
        /// <param name="list">List you want to paginate</param>
        /// <param name="pageIndex">Index of the page you want to get</param>
        /// <param name="pageSize">Max amount of items that page can have</param>
        /// <returns></returns>
        public static IList<T> Paginate<T>(this IList<T> list, int pageIndex, int pageSize)
        {
            return list.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
    }
}
