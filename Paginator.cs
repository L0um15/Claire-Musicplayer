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
        /// <param name="pageNumber">Number of the page you want to get</param>
        /// <param name="pageSize">Max amount of items that page can have</param>
        /// <returns></returns>
        public static List<T> Paginate<T>(this List<T> list, int pageNumber, int pageSize)
        {
            return list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
