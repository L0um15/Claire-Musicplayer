using System;
using System.Collections.Generic;
using System.Text;

namespace Claire_Musicplayer
{
    /// <summary>
    /// Simple Paginator Logic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct Paginator<T>
    { 
        readonly T[] _arr;
        public readonly int PageSize;
        public readonly int ItemsTotal { get => _arr.Length; }
        public readonly int PageCount 
        {
            get
            {
                return (int)MathF.Ceiling((float)ItemsTotal / PageSize);
            }
        }

        public Paginator(T[] arr, int pageSize) 
        {
            _arr = arr;
            PageSize = pageSize;
        }

        public Page<T> GetPage(int pageNumber)
        {

            if (!IsValidPage(pageNumber))
                throw new ArgumentOutOfRangeException();

            int index = pageNumber - 1;

            int pageStart = index * PageSize;
            int lengthToEnd = ItemsTotal - pageStart;

            if(lengthToEnd / PageSize == 0)
                return new Page<T>(_arr.AsSpan(pageStart, lengthToEnd), pageNumber);
            else
                return new Page<T>(_arr.AsSpan(pageStart, PageSize), pageNumber);
        }
        public bool IsValidPage(int pageNumber)
        {
            return !(pageNumber > PageCount || pageNumber < 1);
        }
    }
    public readonly ref struct Page<T>
    {
        public Page(Span<T> span, int pageNumber)
        {
            Span = span;
            PageNumber = pageNumber;
        }

        public readonly Span<T> Span;
        public readonly int PageNumber;
    }
}
