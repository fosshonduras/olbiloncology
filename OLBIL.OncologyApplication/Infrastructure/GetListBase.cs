using System;
using System.Collections.Generic;

namespace OLBIL.OncologyApplication.Infrastructure
{
    public class GetListBase
    {
        /// <summary>
        /// The dictionary column-sort-direction for every column of interest
        /// </summary>
        public List<SortSpec> SortInfo { get; set; }

        /// <summary>
        /// The page index requested
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// The maximum number of items in the result
        /// </summary>
        public int PageSize { get; set; } = 10;

        public class SortSpec
        {
            public string Column { get; set; }
            public bool Descending { get; set; } = false;
        }
    }
}
