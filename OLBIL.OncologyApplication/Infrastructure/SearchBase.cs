﻿using System;
using System.Collections.Generic;

namespace OLBIL.OncologyApplication.Infrastructure
{
    public class SearchBase : GetListBase
    {
        /// <summary>
        /// The global search term
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// A list of filter specifiers column of the form:
        /// column - searchTerm - type - maxValue
        /// 
        /// types can be Exact, Contains, Range
        /// 
        /// If type is Range, searchTerm becomes minValue
        /// 
        /// maxValue is only needed if the type is Range
        /// </summary>
        public List<Dictionary<string, FilterSpec>> Filters { get; set; }

        public class FilterSpec
        {
            public string SearchTerm { get; set; }
            public string Type { get; set; }
            public string MaxValue { get; set; }
        }
    }
}
