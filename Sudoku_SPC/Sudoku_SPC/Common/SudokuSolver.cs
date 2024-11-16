using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Sudoku_SPC.Common
{
    public class SudokuSolver
    {
        public event EventHandler<SudokuCell> GridChanged;
        public event EventHandler SudokuSolved;
        public event EventHandler<Exception> ExceptionThrown;

        private int size = 0;
        private int[][] grid;

        /// <summary>
        /// Initializes a new instance of the SudokuSolver class with the specified grid size.
        /// </summary>
        /// <param name="size">The size of the Sudoku grid (typically 9 for a standard Sudoku puzzle).</param>
        public SudokuSolver(int size)
        {
            this.size = size;
            InitializeGrid(size);
        }

        public int GetValue(int row, int column)
        {
            return grid[row][column];
        }

        public void FillGridFromFile(string gameFilePath)
        {
            if (File.Exists(gameFilePath))
            {
                string[] lines = File.ReadAllLines(gameFilePath);
                if(lines.Length != size) { throw new Exception($"Expected {size} lines, but found {lines.Length} lines in the file: {gameFilePath}."); }

                int i = 0, j = 0;
                foreach (var line in lines)
                {
                    j = 0;
                    string[] chars = line.Split(',');
                    if(chars.Length != size) { throw new Exception($"Expected { size } values per line, but found { chars.Length} in line: \"{line}\"."); }
                    foreach(string c in chars)
                    {
                        grid[i][j] = int.Parse(c);
                        ++j;
                    }
                    ++i;
                }
            }
            else
            {
                throw new Exception("Game file does not exist!");
            }
        }

        public async Task SolveSudokuAsync()
        {
            await Task.Run(() =>
            {
                solving();
                SudokuSolved?.Invoke(this, EventArgs.Empty);
            });
        }
        
        private void InitializeGrid(int size)
        {
            grid = new int[size][];
            for (int i = 0; i < size; i++)
            {
                grid[i] = new int[size];
            }
        }

        private bool solving()
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] != 0) continue;
                    if (SetCell(i, j) is false) return false;
                }
            }
            return true;
        }

        private bool SetCell(int row, int column)
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int attempt = 0; attempt < numbers.Length; attempt++)
            {
                if (CheckGrid(row,column, numbers[attempt]))
                {
                    SetCellValue(row, column, numbers[attempt]);
                    solving();
                    break;
                }
            }
        }

        private bool CheckGrid(int row, int column, int newValue)
        {
            // check row
            if (grid[row].Contains(newValue)) return false;
            if (grid[column].Contains(newValue)) return false;

            (int row, int column) box = (row / 3, column / 3);
            for (int i = 3*box.row; i < 3 * box.row + 3; i++)
            {
                for (int j = 3 * box.column; j < 3 * box.column + 3; j++)
                {
                    if (newValue == grid[i][j]) return false;
                }
            }
            return true;
        }

        private void SetCellValue(int row, int column, int value)
        {
            grid[row][column] = value;
            GridChanged?.Invoke(this, new SudokuCell { Row = row, Column = column, Value = value });
        }
    }

    public class SudokuCell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
    }
}
