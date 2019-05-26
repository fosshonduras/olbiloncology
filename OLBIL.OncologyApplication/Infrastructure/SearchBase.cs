using System;
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
        public List<FilterSpec> Filters { get; set; }

        public class FilterSpec
        {
            public string Column { get; set; }
            public string SearchTerm { get; set; }
            public string Type { get; set; }
            public string MaxValue { get; set; }
        }
    }

    public static class FilterSpecExtensions
    {
        public static bool TryGetValue(this IEnumerable<SearchBase.FilterSpec> filters, string key, out SearchBase.FilterSpec result, bool caseSensitive = true)
        {
            var searchKey = caseSensitive ? key : (key ?? string.Empty).ToLowerInvariant();
            foreach (var item in filters)
            {
                if (item == null) { continue; }

                var itemKey = caseSensitive ? item.Column : (item.Column ?? string.Empty).ToLowerInvariant();
                if (itemKey == searchKey)
                {
                    result = item;
                    return true;
                }
            }
            result = null;
            return false;
        }
    }
}
