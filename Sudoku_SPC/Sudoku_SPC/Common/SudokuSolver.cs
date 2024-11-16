using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Sudoku_SPC.Common
{
    public class SudokuSolver
    {
        private int size = 0;
        private int[][] matrix;

        /// <summary>
        /// Initializes a new instance of the SudokuSolver class with the specified grid size.
        /// </summary>
        /// <param name="size">The size of the Sudoku grid (typically 9 for a standard Sudoku puzzle).</param>
        public SudokuSolver(int size)
        {
            this.size = size;
            InitializeMatrix(size);
        }

        public int GetValue(int row, int column)
        {
            return matrix[row][column];
        }

        public void FillMatrixFromFile(string gameFilePath)
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
                        matrix[i][j] = int.Parse(c);
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

        private void InitializeMatrix(int size)
        {
            matrix = new int[size][];
            for (int i = 0; i < size; i++)
            {
                matrix[i] = new int[size];
            }
        }
    }
}
