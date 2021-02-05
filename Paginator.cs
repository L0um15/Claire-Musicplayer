using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claire_Musicplayer
{
    public class Paginator<T>
    {
        public IEnumerable<T> Data { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int ItemsTotal { get; private set; }
        public int PageCount { get; private set; }

        /// <summary>
        /// Simple Paginator Logic
        /// </summary>
        /// <param name="data">Processed data</param>
        /// <param name="pageSize">Number of items per page</param>
        public Paginator(IEnumerable<T> data, int pageSize)
        {
            Data = data;
            ItemsTotal = data.Count();
            PageSize = pageSize;
            PageCount = (int)Math.Ceiling((decimal)ItemsTotal / (decimal)PageSize);
        }

        public List<T> GetPage(int pageNumber)
        {
            PageNumber = pageNumber;
            return Data.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
        }

    }
}
