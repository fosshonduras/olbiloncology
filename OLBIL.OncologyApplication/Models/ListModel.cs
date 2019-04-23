using System.Collections.Generic;

namespace OLBIL.OncologyApplication.Models
{
    public class ListModel<T> where T : class
    {
        private List<T> items;
        public List<T> Items
        {
            get { return items ?? (items = new List<T>()); }
            set { items = value; }
        }

        public int ItemCount => Items.Count;

        public int PageIndex { get; set; } = 1;
        public int TotalCount { get; set; }
        public int TotalPages => PageSize > 0 ? (TotalCount / PageSize) : 1;
        public int PageSize { get; set; } = 10;

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
