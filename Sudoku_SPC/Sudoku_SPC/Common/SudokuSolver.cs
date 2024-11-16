using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Sudoku_SPC.Common
{
    public class SudokuSolver
    {
        public event EventHandler<Point> GridChanged;

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

        private void solving()
        {

        }
    }
}
