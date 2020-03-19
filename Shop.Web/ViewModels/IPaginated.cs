using System.Collections.Generic;

namespace Shop.Web.ViewModels
{
    public interface IPaginated
    {
        int CurrentPage { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        int ItemsPerPage { get; }
        int LastPage { get; }
        int PageCount { get; }

        IEnumerable<int> GetFollowingPages(int limit);
        IEnumerable<int> GetPreviousPages(int limit);
    }
}