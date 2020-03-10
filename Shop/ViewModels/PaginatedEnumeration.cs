using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.ViewModels
{
    public class PaginatedEnumeration<TItem>
    {
        public PaginatedEnumeration(IEnumerable<TItem> items, int page, int itemsPerPage = 9)
        {
            _items = items;
            ItemsPerPage = itemsPerPage;
            Page = page;
        }

        private readonly IEnumerable<TItem> _items;
        public IEnumerable<TItem> Items { get => _items.Skip(Page * ItemsPerPage).Take(ItemsPerPage); }
        
        private int _page;
        public int Page 
        {
            get => _page;
            set
            {
                if( value < 0 )
                {
                    throw new ArgumentOutOfRangeException("Page value cannot be negative number.");
                }

                if( value >= PageCount )
                {
                    throw new ArgumentOutOfRangeException("Page value cannot be bigger than total page count.");
                }
                
                _page = value;
            }
        }
        public int ItemsPerPage { get; private set; }
        public bool HasPreviousPage { get => Page > 0; }
        public bool HasNextPage { get => Page < PageCount - 1; }
        public int PageCount
        {
            get
            {
                if (_items.Count() == 0)
                {
                    return 1;
                }

                return (int) Math.Ceiling(_items.Count() / (double) ItemsPerPage);
            }
        } 
    }
}
