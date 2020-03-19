using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Web.ViewModels
{
    public class PaginatedEnumerable<TItem> : IEnumerable<TItem>
    {
        public PaginatedEnumerable(IEnumerable<TItem> items, int currentPage, int itemsPerPage)
        {
            _items = items;
            ItemsPerPage = itemsPerPage;
            SetCurrentPage(currentPage);
        }

        private readonly IEnumerable<TItem> _items;
        private IEnumerable<TItem> ItemsPaginated 
        { 
            get => _items.Skip(CurrentPage * ItemsPerPage).Take(ItemsPerPage);
        }
        public IEnumerator<TItem> GetEnumerator() => ItemsPaginated.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ItemsPaginated.GetEnumerator();

        public int CurrentPage { get; private set; }

        public PaginatedEnumerable<TItem> SetCurrentPage(int page)
        {
            if( page < 0 )
            {
                throw new ArgumentOutOfRangeException("Page value cannot be negative number.");
            }

            if( page >= PageCount )
            {
                throw new ArgumentOutOfRangeException("Page value cannot be bigger than total page count.");
            }

            CurrentPage = page;
            return this;
        }

        public int ItemsPerPage { get; private set; }
        public bool HasPreviousPage { get => CurrentPage > 0; }
        public bool HasNextPage { get => CurrentPage < PageCount - 1; }
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

        public int LastPage { get => PageCount - 1; }

        public IEnumerable<int> GetPreviousPages(int limit)
        {
            return Enumerable.Range(CurrentPage - limit, limit)
                .Where(i => i >= 0);
        }
        public IEnumerable<int> GetFollowingPages(int limit)
        {
            return Enumerable.Range(CurrentPage + 1, limit)
                .Where(i => i < PageCount);
        }
    }
}
