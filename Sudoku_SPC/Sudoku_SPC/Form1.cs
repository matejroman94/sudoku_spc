using Sudoku_SPC.Common;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_SPC
{
    public partial class Form1 : Form
    {
        private int lengthOfBox = 3;
        private int widthField = 60;
        private int heightField = 50;
        private int padding = 2;

        private SudokuSolver sudokuSolver;
        private Task sudokuSolverTask = null;
        private CancellationTokenSource cancellationTokenSource;

        private RichTextBox[][] cells = null;
        public Form1()
        {
            InitializeComponent();

            InitializeSudokuGrid();
            CenterToScreen();

            sudokuSolver = new SudokuSolver(lengthOfBox*lengthOfBox);
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

            int offset = lengthOfBox;
            int borderRow = 0;
            cells = new RichTextBox[lengthOfBox * lengthOfBox][];
            for (int i = 0; i< lengthOfBox*lengthOfBox;i+=offset)
            {
                borderRow += i % lengthOfBox == 0 ? (i == 0 ? 2 : 1) * padding : 0;
                CreateBox(i, borderRow);
            }
            CenterPanel(panelSudoku);
        }



        private void CreateBox(int offset,int borderRow)
        {
            for (int i = offset; i < lengthOfBox+offset; i++)
            {
                int borderColumn = 0;
                cells[i] = new RichTextBox[lengthOfBox * lengthOfBox];
                for (int j = 0; j < lengthOfBox* lengthOfBox; j++)
                {
                    borderColumn += j% lengthOfBox == 0 ? (j==0?2:1)*padding : 0;
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
                ChangeSolverButtonText("Stop");
                cancellationTokenSource = new CancellationTokenSource();
                CancellationToken token = cancellationTokenSource.Token;
                sudokuSolverTask = sudokuSolver.SolveSudokuAsync(token);
            }
            else
            {
                cancellationTokenSource?.Cancel();
                ChangeSolverButtonText("Solve puzzle");
            }
        }

        private void SudokuSolver_GridChanged(object sender, SudokuCell e)
        {
            BeginInvoke(new Action(() => UpdateCell(e)));
        }

        private void SudokuSolver_SudokuSolved(object sender, EventArgs e)
        {
            ChangeSolverButtonText("Solve puzzle");
            MessageBox.Show("Sudoku solved :-)", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateCell(SudokuCell updatedCell)
        {
            cells[updatedCell.Row][updatedCell.Column].Text = updatedCell.Value.ToString();
        }

        private void SudokuSolver_ExceptionThrown(object sender, Exception e)
        {
            ChangeSolverButtonText("Solve puzzle");
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cbVisualization_CheckedChanged(object sender, EventArgs e)
        {
            sudokuSolver.VisualizationEnabled = cbVisualization.Checked;
        }

        private void ChangeSolverButtonText(string text)
        {
            BeginInvoke(new Action(() =>
            {
                btnSolvePuzzle.Text = text;
            }));
        }
    }
}
