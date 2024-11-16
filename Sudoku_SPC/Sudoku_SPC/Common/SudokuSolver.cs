using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
