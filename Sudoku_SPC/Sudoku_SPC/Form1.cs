using Sudoku_SPC.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sudoku_SPC
{
    public partial class Form1 : Form
    {
        private int lengthOfSquare = 3;
        private int widthField = 60;
        private int heightField = 50;
        private int padding = 2;

        private SudokuSolver sudokuSolver;
        private Task sudokuSolverTask = null;

        private RichTextBox[][] cells = null;
        public Form1()
        {
            InitializeComponent();

            InitializeSudokuGrid();
            CenterToScreen();

            sudokuSolver = new SudokuSolver(lengthOfSquare*lengthOfSquare);
            sudokuSolver.GridChanged += SudokuSolver_GridChanged;
            sudokuSolver.SudokuSolved += SudokuSolver_SudokuSolved;
            sudokuSolver.ExceptionThrown += SudokuSolver_ExceptionThrown;

#if DEBUG
            sudokuSolver.FillGridFromFile("sudoku.txt");
            DisplayValues();
#endif
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CenterPanel(Panel panel)
        {
            // Calculate the center position
            int x = (this.ClientSize.Width - panel.Width) / 2;
            int y = (this.ClientSize.Height - panel.Height) / 2 + 50;

            // Set the panel's location
            panel.Location = new Point(x, y);
            InitializeLayout(x,y);
        }

        private void InitializeLayout(int x, int y)
        {
            btnSolvePuzzle.Location = new Point(x, y-100);
        }

        private void InitializeSudokuGrid()
        {
            panelSudoku.Size = new Size();
            panelSudoku.Margin = new Padding(100);
            panelSudoku.Padding = new Padding();

            int offset = lengthOfSquare;
            int borderRow = 0;
            cells = new RichTextBox[lengthOfSquare * lengthOfSquare][];
            for (int i = 0; i< lengthOfSquare*lengthOfSquare;i+=offset)
            {
                borderRow += i % lengthOfSquare == 0 ? (i == 0 ? 2 : 1) * padding : 0;
                CreateBox(i, borderRow);
            }
            CenterPanel(panelSudoku);
        }



        private void CreateBox(int offset,int borderRow)
        {
            for (int i = offset; i < lengthOfSquare+offset; i++)
            {
                int borderColumn = 0;
                cells[i] = new RichTextBox[lengthOfSquare * lengthOfSquare];
                for (int j = 0; j < lengthOfSquare* lengthOfSquare; j++)
                {
                    borderColumn += j% lengthOfSquare == 0 ? (j==0?2:1)*padding : 0;
                    RichTextBox richTextBox = new RichTextBox
                    {
                        Margin = new Padding(2*padding),
                        Padding = new Padding(),
                        Width = widthField,
                        Height = heightField,
                        Multiline = false,
                        Text = string.Empty,
                        Location = new System.Drawing.Point(
                            j * (widthField+ padding) + (borderColumn),
                            i * (heightField + padding) + (borderRow)),
                        BorderStyle = BorderStyle.None,
                        SelectionAlignment = HorizontalAlignment.Center,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 25.0f)
                    };
                    richTextBox.TextChanged += (sender, e) =>
                    {
                        RichTextBox rtb = (RichTextBox)sender;
                        Validator.ProcessNewInput(rtb);
                    };
                    cells[i][j] = richTextBox;
                    this.panelSudoku.Controls.Add(richTextBox);
                }
            }
        }

        private void DisplayValues()
        {
            try
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    for (int j = 0; j < cells[i].Length; j++)
                    {
                        cells[i][j].Text = sudokuSolver.GetValue(i, j).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while displaying values: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.Title = "Select a Text File";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        // Read the file contents
                        try
                        {
                            sudokuSolver.FillGridFromFile(filePath);
                            DisplayValues();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error reading file: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSolvePuzzle_Click(object sender, EventArgs e)
        {
            if(sudokuSolverTask?.IsCompleted ?? true)
            {
                sudokuSolverTask = sudokuSolver.SolveSudokuAsync();
            }
        }

        private void SudokuSolver_GridChanged(object sender, SudokuCell e)
        {
            BeginInvoke(new Action(() => UpdateCell(e)));
        }

        private void SudokuSolver_SudokuSolved(object sender, EventArgs e)
        {
            MessageBox.Show("Sudoku solved :-)", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateCell(SudokuCell updatedCell)
        {
            cells[updatedCell.Row][updatedCell.Column].Text = updatedCell.Value.ToString();
        }

        private void SudokuSolver_ExceptionThrown(object sender, Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
