using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChat
{
    public class MineSweeper
    {
        public string[] GetMarkedMineField(string[] mineField)
        {


            var resultRow = new string[mineField.Length];
            for (var rowIndex = 0; rowIndex < mineField.Length; rowIndex++)
            {
                for (var colIndex = 0; colIndex < mineField[0].Length; colIndex++)
                {
                    resultRow[rowIndex] += GetCellValue(rowIndex, colIndex, mineField);
                }

            }
            return resultRow;
        }

        private static string GetCellValue(int rowIndex, int colIndex, string[] mineField)
        {

            if (IsMine(rowIndex, colIndex, mineField))
            {
                return "*";
            }
            else
            {
                return GetNumberOfMinesAround(rowIndex, colIndex, mineField);
            }
        }

        private static string GetNumberOfMinesAround(int rowIndex, int colIndex, string[] mineField)
        {
            var numberOfMinesAround = 0;
            if (IsMine(rowIndex, colIndex + 1, mineField)) numberOfMinesAround++;
            if (IsMine(rowIndex + 1, colIndex, mineField)) numberOfMinesAround++;
            if (IsMine(rowIndex, colIndex - 1, mineField)) numberOfMinesAround++;
            if (IsMine(rowIndex - 1, colIndex, mineField)) numberOfMinesAround++;
            if (IsMine(rowIndex - 1, colIndex - 1, mineField)) numberOfMinesAround++;
            if (IsMine(rowIndex + 1, colIndex - 1, mineField)) numberOfMinesAround++;
            if (IsMine(rowIndex + 1, colIndex + 1, mineField)) numberOfMinesAround++;
            if (IsMine(rowIndex - 1, colIndex + 1, mineField)) numberOfMinesAround++;
            
            return numberOfMinesAround.ToString();
        }

        private static bool IsMine(int rowNumber, int columnNumber, string[] mineField)
        {
            if (rowNumber < 0 || columnNumber < 0 || rowNumber > mineField.Length - 1 ||
                columnNumber > mineField[rowNumber].Length - 1) return false;
            return mineField[rowNumber][columnNumber] == '*';
        }
    }
}
