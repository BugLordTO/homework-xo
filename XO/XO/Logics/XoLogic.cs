using System;
using System.Linq;

namespace XO.Logics
{
    public class XoLogic
    {
        public const char X = 'x';
        public const char O = 'o';
        public const char EMPTY = ' ';

        /// <summary>
        /// Check is game over
        /// 1. The game is not over yet.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool IsGameOver(char[,] board)
        {
            var rowCount = board.GetLength(1);
            var columnCount = board.GetLength(1);

            var allSlots = Enumerable.Empty<char>().ToList();
            for (int i = 0; i < rowCount; i++)
            {
                var slots = GetHorizontalSlot(board, i);
                allSlots.AddRange(slots);
            }
            var isFullMarkedBoard = allSlots.All(s => s != EMPTY);
            if (isFullMarkedBoard)
            {
                return true;
            }

            for (int i = 0; i < rowCount; i++)
            {
                var slots = GetHorizontalSlot(board, i);
                var isWin = slots.All(s => s == X) || slots.All(s => s == O);
                if (isWin) return true;
            }

            for (int i = 0; i < columnCount; i++)
            {
                var slots = GetVerticalSlot(board, i);
                var isWin = slots.All(s => s == X) || slots.All(s => s == O);
                if (isWin) return true;
            }

            var diagonalFromRightSlots = GetDiagonalFromRight(board);
            var isDiagonalFromRightWin = diagonalFromRightSlots.All(s => s == X) || diagonalFromRightSlots.All(s => s == O);
            if (isDiagonalFromRightWin) return true;

            var diagonalFromLeftSlots = GetDiagonalFromLeft(board);
            var isDiagonalFromLeftWin = diagonalFromLeftSlots.All(s => s == X) || diagonalFromLeftSlots.All(s => s == O);
            if (isDiagonalFromLeftWin) return true;

            return false;
        }

        /// <summary>
        /// Get game state
        /// 2. The game is over and O is a winner.
        /// 3. The game is over and X is a winner.
        /// 4. The game is over and they both draw.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public XoStateEnum GetGameState(char[,] board)
        {
            var rowCount = board.GetLength(1);
            var columnCount = board.GetLength(1);

            var allSlots = Enumerable.Empty<char>().ToList();
            for (int i = 0; i < rowCount; i++)
            {
                var slots = GetHorizontalSlot(board, i);
                allSlots.AddRange(slots);
            }

            var isContainValidChar = allSlots.All(x => x == X || x == O || x == EMPTY);
            if (!isContainValidChar)
            {
                return XoStateEnum.Invalid;
            }
            var XCount = allSlots.Where(x => x == X).Count();
            var OCount = allSlots.Where(x => x == O).Count();
            var isUnreasonableCount = Math.Abs(XCount - OCount) > 1;
            if (isUnreasonableCount)
            {
                return XoStateEnum.Invalid;
            }

            for (int i = 0; i < rowCount; i++)
            {
                var slots = GetHorizontalSlot(board, i);
                var isXWin = slots.All(s => s == X);
                if (isXWin) return XoStateEnum.XWin;
                var isOWin = slots.All(s => s == O);
                if (isOWin) return XoStateEnum.OWin;
            }

            for (int i = 0; i < columnCount; i++)
            {
                var slots = GetVerticalSlot(board, i);
                var isXWin = slots.All(s => s == X);
                if (isXWin) return XoStateEnum.XWin;
                var isOWin = slots.All(s => s == O);
                if (isOWin) return XoStateEnum.OWin;
            }

            var diagonalFromRightSlots = GetDiagonalFromRight(board);
            var isXWinDiagonalFromRight = diagonalFromRightSlots.All(s => s == X);
            if (isXWinDiagonalFromRight) return XoStateEnum.XWin;
            var isOWinDiagonalFromRight = diagonalFromRightSlots.All(s => s == O);
            if (isOWinDiagonalFromRight) return XoStateEnum.OWin;

            var diagonalFromLeftSlots = GetDiagonalFromLeft(board);
            var isXWinDiagonalFromLeft = diagonalFromLeftSlots.All(s => s == X);
            if (isXWinDiagonalFromLeft) return XoStateEnum.XWin;
            var isOWinDiagonalFromLeft = diagonalFromLeftSlots.All(s => s == O);
            if (isOWinDiagonalFromLeft) return XoStateEnum.OWin;

            var isFullMarkedBoard = allSlots.Any(s => s == EMPTY);
            if (isFullMarkedBoard)
            {
                return XoStateEnum.Incomplete;
            }
            else
            {
                return XoStateEnum.Draw;
            }
        }

        /// <summary>
        /// Check is input valid
        /// 5. The game input state is invalid.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool IsInputValid(char[,] board, char player, int rowIndex, int columnIndex)
        {
            var rowCount = board.GetLength(1);
            var columnCount = board.GetLength(1);

            var allSlots = Enumerable.Empty<char>().ToList();
            for (int i = 0; i < rowCount; i++)
            {
                var slots = GetHorizontalSlot(board, i);
                allSlots.AddRange(slots);
            }

            var isRowIndexOutOfBound = rowIndex >= rowCount;
            if (isRowIndexOutOfBound)
            {
                return false;
            }

            var isColumnIndexOutOfBound = columnIndex >= columnCount;
            if (isColumnIndexOutOfBound)
            {
                return false;
            }

            var isEmptySlot = board[rowIndex, columnIndex] == ' ';
            if (!isEmptySlot)
            {
                return false;
            }

            var isValidPlayerChar = player == X || player == O;
            if (!isValidPlayerChar)
            {
                return false;
            }

            var XCount = allSlots.Where(x => x == X).Count();
            var OCount = allSlots.Where(x => x == O).Count();
            var isValidTurn = true;
            if (XCount > OCount)
            {
                isValidTurn = player == O;
            }
            else if (XCount < OCount)
            {
                isValidTurn = player == X;
            }
            else
            {
                isValidTurn = player == X || player == O;
            }
            if (!isValidTurn)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get one vertical slots by spectify column index
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public char[] GetHorizontalSlot(char[,] board, int row)
        {
            var columnCount = board.GetLength(0);
            var slots = Enumerable.Empty<char>().ToList();

            for (int i = 0; i < columnCount; i++)
            {
                slots.Add(board[row, i]);
            }

            return slots.ToArray();
        }

        /// <summary>
        /// Get one vertical slots by spectify column index
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public char[] GetVerticalSlot(char[,] board, int column)
        {
            var rowCount = board.GetLength(1);
            var slots = Enumerable.Empty<char>().ToList();

            for (int i = 0; i < rowCount; i++)
            {
                slots.Add(board[i, column]);
            }

            return slots.ToArray();
        }

        /// <summary>
        /// Get diagonal slots from top right to bottom left
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public char[] GetDiagonalFromRight(char[,] board)
        {
            var rowCount = board.GetLength(1);
            var slots = Enumerable.Empty<char>().ToList();

            var columnIndex = 0;
            for (int i = 0; i < rowCount; i++)
            {
                slots.Add(board[i, columnIndex]);
                columnIndex++;
            }

            return slots.ToArray();
        }

        /// <summary>
        /// Get diagonal slots from top left to bottom right
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public char[] GetDiagonalFromLeft(char[,] board)
        {
            var rowCount = board.GetLength(1);
            var slots = Enumerable.Empty<char>().ToList();

            var columnIndex = 0;
            for (int i = rowCount - 1; i >= 0; i--)
            {
                slots.Add(board[i, columnIndex]);
                columnIndex++;
            }

            return slots.ToArray();
        }
    }
}
