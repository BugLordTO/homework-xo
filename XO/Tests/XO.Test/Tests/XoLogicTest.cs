using FluentAssertions;
using System.Collections.Generic;
using XO.Logics;
using Xunit;

namespace XO.Test
{
    public class XoLogicTest
    {
        public static IEnumerable<object[]> IsGameOver_Scearios = new List<object[]>
        {
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, false },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', 'o' },
            }, true },
            new object[] { new char[,] {
                { 'o', 'o', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, true },
            new object[] { new char[,] {
                { 'x', 'o', 'o' },
                { 'x', 'o', 'o' },
                { 'x', 'x', ' ' },
            }, true },
        };

        [Theory]
        [MemberData(nameof(IsGameOver_Scearios))]
        public void IsGameOver_Test(char[,] board, bool expected)
        {
            var sut = new XoLogic();
            var actual = sut.IsGameOver(board);
            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> GetGameState_Scearios = new List<object[]>
        {
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, XoStateEnum.Invalid },
            new object[] { new char[,] {
                { 'a', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, XoStateEnum.Invalid },
            new object[] { new char[,] {
                { 'x', 'o', 'o' },
                { 'x', 'o', 'o' },
                { 'x', 'x', ' ' },
            }, XoStateEnum.XWin },
            new object[] { new char[,] {
                { 'o', 'o', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, XoStateEnum.OWin },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'o', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, XoStateEnum.Incomplete },
            new object[] { new char[,] {
                { 'o', 'o', 'x' },
                { 'x', 'x', 'o' },
                { 'o', 'x', 'x' },
            }, XoStateEnum.Draw },
        };

        [Theory]
        [MemberData(nameof(GetGameState_Scearios))]
        public void GetGameState_Test(char[,] board, XoStateEnum expected)
        {
            var sut = new XoLogic();
            var actual = sut.GetGameState(board);
            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> IsInputValid_Scearios = new List<object[]>
        {
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'o', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, XoLogic.X, 2, 2, true },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'o', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, XoLogic.X, 2, 3, false },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'o', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, XoLogic.X, 3, 2, false },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'o', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, XoLogic.X, 2, 1, false },
            new object[] { new char[,] {
                { 'o', ' ', 'o' },
                { 'o', ' ', 'x' },
                { 'x', ' ', ' ' },
            }, XoLogic.X, 2, 2, true },
            new object[] { new char[,] {
                { 'o', ' ', 'o' },
                { 'o', ' ', 'x' },
                { 'x', ' ', ' ' },
            }, XoLogic.O, 2, 2, false },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'o', ' ', 'x' },
                { 'x', ' ', ' ' },
            }, XoLogic.O, 2, 2, true },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'o', ' ', 'x' },
                { 'x', ' ', ' ' },
            }, XoLogic.O, 2, 2, true },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'o', ' ' },
            }, 'a', 2, 2, false },
        };

        [Theory]
        [MemberData(nameof(IsInputValid_Scearios))]
        public void IsInputValid_Test(char[,] board, char player, int rowIndex, int columnIndex, bool expected)
        {
            var sut = new XoLogic();
            var actual = sut.IsInputValid(board, player, rowIndex, columnIndex);
            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> GetHorizontalSlot_Scearios = new List<object[]>
        {
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, 0, new char[] { 'o', 'x', 'o' } },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, 1, new char[] { 'x', 'o', 'x' } },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, 2, new char[] { 'x', 'x', ' ' } },
        };

        [Theory]
        [MemberData(nameof(GetHorizontalSlot_Scearios))]
        public void GetHorizontalSlot_Test(char[,] board, int column, char[] expected)
        {
            var sut = new XoLogic();
            var actual = sut.GetHorizontalSlot(board, column);
            actual.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetVerticalSlot_Scearios = new List<object[]>
        {
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, 0, new char[] { 'o', 'x', 'x' } },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, 1, new char[] { 'x', 'o', 'x' } },
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, 2, new char[] { 'o', 'x', ' ' } },
        };

        [Theory]
        [MemberData(nameof(GetVerticalSlot_Scearios))]
        public void GetVerticalSlot_Test(char[,] board, int column, char[] expected)
        {
            var sut = new XoLogic();
            var actual = sut.GetVerticalSlot(board, column);
            actual.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetDiagonalFromRight_Scearios = new List<object[]>
        {
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, new char[] { 'o', 'o', ' ' } },
        };

        [Theory]
        [MemberData(nameof(GetDiagonalFromRight_Scearios))]
        public void GetDiagonalFromRight_Test(char[,] board, char[] expected)
        {
            var sut = new XoLogic();
            var actual = sut.GetDiagonalFromRight(board);
            actual.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetDiagonalFromLeft_Scearios = new List<object[]>
        {
            new object[] { new char[,] {
                { 'o', 'x', 'o' },
                { 'x', 'o', 'x' },
                { 'x', 'x', ' ' },
            }, new char[] { 'o', 'o', 'x' } },
        };

        [Theory]
        [MemberData(nameof(GetDiagonalFromLeft_Scearios))]
        public void GetDiagonalFromLeft_Test(char[,] board, char[] expected)
        {
            var sut = new XoLogic();
            var actual = sut.GetDiagonalFromLeft(board);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
