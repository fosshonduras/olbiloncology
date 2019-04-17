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
    }
}
