using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace ConsoleChat.Tests
{
    public class MineSweeperTests
    {
        [Fact]
        public void GetMarkedMineFieldReturnsCorrectResult()
        {
            var sut = new MineSweeper();
            var mineField = new[]
            {
                "...*.*",
                "*....*",
                "..*.*.",
                "*....*",
                "...*..",
                "......"
            };

            var expected = new[]
            {
                "111*3*",
                "*2234*",
                "23*2*3",
                "*2233*",
                "111*21",
                "001110"
            };

            var actual = sut.GetMarkedMineField(mineField);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EmptyMineFieldReturnsZero()
        {
            var sut = new MineSweeper();
            var mineField = new[]
            {
                "."
            };

            var expected = new[]
            {
                "0"
            };

            var actual = sut.GetMarkedMineField(mineField);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FullMineFieldReturnsMine()
        {
            var sut = new MineSweeper();
            var mineField = new[]
            {
                "*"
            };

            var expected = new[]
            {
                "*"
            };

            var actual = sut.GetMarkedMineField(mineField);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("..", "00")]
        [InlineData("...", "000")]
        [InlineData("....", "0000")]
        [InlineData(".*", "1*")]
        [InlineData("*.", "*1")]
        [InlineData("*.*", "*2*")]
        public void OneRowMineFieldReturnsCorrectResult(string source, string result)
        {
            var sut = new MineSweeper();
            var mineField = new[]
            {
                source
            };

            var expected = new[]
            {
                result
            };

            var actual = sut.GetMarkedMineField(mineField);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("..", "00")]
        [InlineData("...", "000")]
        [InlineData("....", "0000")]
        [InlineData(".*", "1*")]
        [InlineData("*.", "*1")]
        [InlineData("*.*", "*2*")]
        public void OneColumnMineFieldReturnsCorrectResult(string source, string result)
        {
            var sut = new MineSweeper();
            var mineField = TransposeMineField(new []
            {
                source
            });

            var expected = TransposeMineField(new[]
            {
                result
            });

            var actual = sut.GetMarkedMineField(mineField);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new[] { "*.", ".." }, new[] { "*1", "11" })]
        [InlineData(new[] { ".*", ".." }, new[] { "1*", "11" })]
        [InlineData(new[] { "..", "*." }, new[] { "11", "*1" })]
        [InlineData(new[] { "..", ".*" }, new[] { "11", "1*" })]
        public void MineFieldHandlesDiagonalMines(string[] mineField, string[] expected)
        {
            var sut = new MineSweeper();

            var actual = sut.GetMarkedMineField(mineField);

            Assert.Equal(expected, actual);
        }

        private static string[] TransposeMineField(string[] mineField)
        {
            var rows = new List<string>();

            for (var col = 0; col < mineField[0].Length; col++)
            {
                var rowResult = "";
                for (var row = 0; row < mineField.Length; row++)
                {
                    rowResult += mineField[row][col];
                }

                rows.Add(rowResult);
            }

            return rows.ToArray();
        }

    }
}
