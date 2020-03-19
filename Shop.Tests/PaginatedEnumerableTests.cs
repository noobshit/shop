using Shop.Web.ViewModels;
using System;
using System.Linq;
using Xunit;

namespace Shop.Tests
{
    public class PaginatedEnumerableTests
    {
        private readonly int[] _data13Ints = new int[]
            {
                0, 1, 2, 3, 4,
                5, 6, 7, 8, 9,
                10, 11, 12
            };
        
        [Theory]
        [InlineData(0, 0, 5)]
        [InlineData(1, 5, 10)]
        [InlineData(2, 10, 13)]
        public void GetEnumerator_PageChanges_ReturnCorrectItems(int page, int start, int end)
        {
            var paginatedEnumeration = new PaginatedEnumerable<int>(_data13Ints, page, 5);

            var pe = paginatedEnumeration.ToArray();

            Assert.Equal(_data13Ints[start..end], pe);
        }

        [Fact]
        public void Constructor_WithNegativePage_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new PaginatedEnumerable<int>(_data13Ints, -1, 5)
                );
        }

        [Fact]
        public void Constructor_WithPageBiggerThanPageCount_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new PaginatedEnumerable<int>(_data13Ints, 3, 5)
                );
        }

        [Fact]
        public void SetCurrentPage_WithNegativePage_ThrowsException()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 0, 5);
            Assert.Throws<ArgumentOutOfRangeException>(
                () => pe.SetCurrentPage(-1)
                );
        }

        [Fact]
        public void SetCurrentPage_WithPageBiggerThanPageCount_ThrowsException()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 0, 5);
            Assert.Throws<ArgumentOutOfRangeException>(
                () => pe.SetCurrentPage(3)
                );
        }

        [Theory]
        [InlineData(3, 5)]
        [InlineData(5, 3)]
        [InlineData(9, 2)]
        [InlineData(13, 1)]
        public void PageCount_ChangingItemsPerPage_AffectsResult(int itemsPerPage, int expected)
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 0, itemsPerPage);
            var result = pe.PageCount;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PageCount_EmptyEnumerable_ReturnsOne()
        {
            var data = new int[] { };

            var pe = new PaginatedEnumerable<int>(data, 0, 5);

            var count = pe.PageCount;
            Assert.Equal(1, count);
        }

        [Fact]
        public void HasNextPage_IsNotOnLastPage_ReturnsTrue()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 0, 5);
            Assert.True(pe.HasNextPage);
        }

        [Fact]
        public void HasNextPage_IsOnLastPage_ReturnsFalse()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 2, 5);
            Assert.False(pe.HasNextPage);
        }

        [Fact]
        public void HasPreviousPage_IsOnFirstPage_ReturnsFalse()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 0, 5);
            Assert.False(pe.HasPreviousPage);
        }

        [Fact]
        public void HasPreviousPage_IsOnNotFirstPage_ReturnsTrue()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 2, 5);
            Assert.True(pe.HasPreviousPage);
        }

        [Fact]
        public void GetPreviousPages_IsOnFirstPage_ReturnsEmptyEnumerable()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 0, 3);
            var expected = new int[] { };
            Assert.Equal(expected, pe.GetPreviousPages(1).ToArray());
        }

        [Fact]
        public void GetPreviousPages_LimitIsNotExhaustive_ReturnsLimitedNumberOfPages()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 2, 5);
            var expected = new int[] { 1 };
            Assert.Equal(expected, pe.GetPreviousPages(1).ToArray());
        }

        [Fact]
        public void GetPreviousPages_LimitIsExhaustive_ReturnsAllPreviousPages()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 2, 5);
            var expected = new int[] { 0, 1 };
            Assert.Equal(expected, pe.GetPreviousPages(3).ToArray());
        }

        [Fact]
        public void GetFollowingPages_IsOnLastPage_ReturnsEmptyEnumerable()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 4, 3);
            var expected = new int[] { };
            Assert.Equal(expected, pe.GetFollowingPages(10).ToArray());
        }

        [Fact]
        public void GetFollowingPages_LimitIsNotExhaustive_ReturnsLimitedNumberOfPages()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 0, 3);
            var expected = new int[] { 1, 2, 3 };
            Assert.Equal(expected, pe.GetFollowingPages(3).ToArray());
        }

        [Fact]
        public void GetFollowingPages_LimitIsExhaustive_ReturnsAllFollowingPages()
        {
            var pe = new PaginatedEnumerable<int>(_data13Ints, 2, 3);
            var expected = new int[] { 3, 4 };
            Assert.Equal(expected, pe.GetFollowingPages(3).ToArray());
        }
    }
}
