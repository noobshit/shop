using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ViewModels
{
    public class PaginatedEnumeration<TItem>
    {
        public PaginatedEnumeration(IEnumerable<TItem> items, int page, int itemsPerPage = 9)
        {
            _items = items;
            Page = page;
            ItemsPerPage = itemsPerPage;
        }

        private readonly IEnumerable<TItem> _items;
        public IEnumerable<TItem> Items { get => _items.Skip(Page * ItemsPerPage).Take(ItemsPerPage); }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public bool HasPreviousPage { get => Page > 0; }
        public bool HasNextPage { get => Page < PageCount - 1; }
        public int PageCount { get => (int) Math.Ceiling(_items.Count() / (double) ItemsPerPage); } 
    }
}
