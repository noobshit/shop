using System.Collections.Generic;

namespace Shop.Web.ViewModels
{
    public static class PaginatedEnumerableExtension
    {
        public static PaginatedEnumerable<TItem> SetItemsPerPage<TItem>(this IEnumerable<TItem> items, int itemsPerPage)
        {
            return new PaginatedEnumerable<TItem>(items, 0, itemsPerPage);
        }
    }
}
