using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Model
{
    public sealed class Paging<T> where T : class
    {
        public int FromIndex { get; set; }

        public int ToIndex { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string OrderName { get; set; }

        public OrderType OrderType { get; set; }

        public Expression<Func<T, bool>> Where { get; set; }
    }

    public enum OrderType
    {
        Desc,
        Asc,
    }

    public sealed class QueryResponse<T>
    {
        // ReSharper disable InconsistentNaming
        public long total { get; set; }
        // ReSharper restore InconsistentNaming

        // ReSharper disable InconsistentNaming
        public int page { get; set; }
        // ReSharper restore InconsistentNaming

        /// <summary>
        /// 总页数
        /// </summary>
        public long Pages { get; set; }

        // ReSharper disable InconsistentNaming
        public int records { get; set; }
        // ReSharper restore InconsistentNaming

        // ReSharper disable InconsistentNaming
        public IList<T> rows { get; set; }
        // ReSharper restore InconsistentNaming
    }
}
