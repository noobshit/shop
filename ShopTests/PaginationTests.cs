using Shop.ViewModels;
using System;
using System.Linq;
using Xunit;

namespace ShopTests
{
    public class PaginationTests
    {
        private readonly int[] _data12Ints = new int[]
            {
                0, 1, 2, 3, 4,
                5, 6, 7, 8, 9,
                10, 11, 12
            };

        [Fact]
        public void ReturnsCorrectItems()
        {
            var paginatedEnumeration = new PaginatedEnumeration<int>(_data12Ints, 0, 5);

            // act
            var page0 = paginatedEnumeration.Items.ToArray();

            paginatedEnumeration.Page = 1;
            var page1 = paginatedEnumeration.Items.ToArray();

            paginatedEnumeration.Page = 2;
            var page2 = paginatedEnumeration.Items;

            // assert
            Assert.Equal(_data12Ints[0..5], page0);
            Assert.Equal(_data12Ints[5..10], page1);
            Assert.Equal(_data12Ints[10..13], page2);
        }

        [Fact]
        public void InvalidPageThrowsException()
        {
            // arrange
            // act and assert
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new PaginatedEnumeration<int>(_data12Ints, -1, 5)
                );


            Assert.Throws<ArgumentOutOfRangeException>(
                () => new PaginatedEnumeration<int>(_data12Ints, 3, 5)
                );
        }

        [Fact]
        public void SetsCorrectPageCount()
        {
            var pe0 = new PaginatedEnumeration<int>(_data12Ints, 0, 5);
            var pe1 = new PaginatedEnumeration<int>(_data12Ints, 0, 3);

            // act
            var count0 = pe0.PageCount;
            var count1 = pe1.PageCount;

            // assert 
            Assert.Equal(3, count0);
            Assert.Equal(5, count1);
        }

        [Fact]
        public void SetsCorrectPageCountForEmptyEnumeration()
        {
            var data = new int[] { };

            var pe = new PaginatedEnumeration<int>(data, 0, 5);

            var count = pe.PageCount;
            Assert.Equal(1, count);
        }

        [Fact]
        public void SetCorrectHasNextPage_1()
        {
            var pe = new PaginatedEnumeration<int>(_data12Ints, 0, 5);
            Assert.True(pe.HasNextPage);
        }

        [Fact]
        public void SetCorrectHasNextPage_2()
        {
            var pe = new PaginatedEnumeration<int>(_data12Ints, 2, 5);
            Assert.False(pe.HasNextPage);
        }

        [Fact]
        public void SetCorrectHasPreviousPage_1()
        {
            var pe = new PaginatedEnumeration<int>(_data12Ints, 0, 5);
            Assert.False(pe.HasPreviousPage);
        }

        [Fact]
        public void SetCorrectHasPreviousPage_2()
        {
            var pe = new PaginatedEnumeration<int>(_data12Ints, 2, 5);
            Assert.True(pe.HasPreviousPage);
        }
    }
}
