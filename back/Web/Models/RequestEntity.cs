using Model;

namespace Web.Models
{
    public sealed class QueryRequest
    {
        /// <summary>
        /// 当前页 索引
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 顺序值 desc asc
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public string Where { get; set; }
    }

    public static class QueryRequestExtension
    {
        public static Paging<T> ConvertRequestTopaging<T>(this QueryRequest request) where T : class
        {
            var paging = new Paging<T>
                             {
                                 FromIndex = (request.Page - 1) * request.Rows + 1,
                                 ToIndex = request.Page * request.Rows,
                                 OrderType = request.Order.ToUpper().Equals("asc".ToUpper()) ? OrderType.Asc : OrderType.Desc,
                                 OrderName = request.Order,
                                 PageSize=request.Rows,
                                 PageIndex = request.Page,
                             };

            //TODO:haven't where

            return paging;
        }
    }

    //public sealed class QueryResponse<T>
    //{
    //    // ReSharper disable InconsistentNaming
    //    public int total { get; set; }
    //    // ReSharper restore InconsistentNaming

    //    // ReSharper disable InconsistentNaming
    //    public int page { get; set; }
    //    // ReSharper restore InconsistentNaming

    //    // ReSharper disable InconsistentNaming
    //    public int records { get; set; }
    //    // ReSharper restore InconsistentNaming

    //    // ReSharper disable InconsistentNaming
    //    public IList<T> rows { get; set; }
    //    // ReSharper restore InconsistentNaming
    //}

    public sealed class WhereEntity
    {
        public string Column { get; set; }

        public object FilterValue { get; set; }

        public WhereEntity Left { get; set; }

        public WhereEntity Right { get; set; }
    }
}