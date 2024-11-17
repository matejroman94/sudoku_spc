using Sudoku_SPC.Common;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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
        private bool closingProcess = false;
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
            try
            {
                sudokuSolver.FillGridFromFile("SavedGames\\sudoku.txt");
                DisplayValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the saved game: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
        }

        #region Event handlers
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closingProcess) return;
            if (SaveTheGameAsk() is false)
            {
                e.Cancel = true; 
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveTheGameAsk() is false)
            {
                return; 
            }
            closingProcess = true;
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTheGame();
        }

        private void btnSolvePuzzle_Click(object sender, EventArgs e)
        {
            if (sudokuSolverTask?.IsCompleted ?? true)
            {
                UpdateSudokuValues();
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

        private void SudokuSolver_ExceptionThrown(object sender, Exception e)
        {
            ChangeSolverButtonText("Solve puzzle");
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cbVisualization_CheckedChanged(object sender, EventArgs e)
        {
            sudokuSolver.VisualizationEnabled = cbVisualization.Checked;
        }
        private void clearTheGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearFullGrid();
        }

        private void btnNextGame_Click(object sender, EventArgs e)
        {
            if (SaveTheGameAsk())
            {
                LoadNextRandomGame();
            }
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            OpenSaveDialog();
        }
        #endregion Event handlers

        #region Private layout methods
        private void CenterPanel(Panel panel)
        {
            // Calculate the center position
            int x = (this.ClientSize.Width - panel.Width) / 2;
            int y = (this.ClientSize.Height - panel.Height) / 2 + 50;

            // Set the panel's location
            panel.Location = new Point(x, y);
            InitializeLayout(x, y);
        }

        private void InitializeLayout(int x, int y)
        {
            btnSolvePuzzle.Location = new Point(x, y - 100);
            btnNextGame.Location = new Point(x + 280, y - 100);
            btnSaveGame.Location = new Point(x + 450, y - 100);
        }

        private void InitializeSudokuGrid()
        {
            panelSudoku.Size = new Size();
            panelSudoku.Margin = new Padding(100);
            panelSudoku.Padding = new Padding();

            int offset = lengthOfBox;
            int borderRow = 0;
            cells = new RichTextBox[lengthOfBox * lengthOfBox][];
            for (int i = 0; i < lengthOfBox * lengthOfBox; i += offset)
            {
                borderRow += i % lengthOfBox == 0 ? (i == 0 ? 2 : 1) * padding : 0;
                CreateBox(i, borderRow);
            }
            CenterPanel(panelSudoku);
        }



        private void CreateBox(int offset, int borderRow)
        {
            for (int i = offset; i < lengthOfBox + offset; i++)
            {
                int borderColumn = 0;
                cells[i] = new RichTextBox[lengthOfBox * lengthOfBox];
                for (int j = 0; j < lengthOfBox * lengthOfBox; j++)
                {
                    borderColumn += j % lengthOfBox == 0 ? (j == 0 ? 2 : 1) * padding : 0;
                    RichTextBox richTextBox = new RichTextBox
                    {
                        Margin = new Padding(2 * padding),
                        Padding = new Padding(),
                        Width = widthField,
                        Height = heightField,
                        Multiline = false,
                        Text = string.Empty,
                        Location = new System.Drawing.Point(
                            j * (widthField + padding) + (borderColumn),
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
        #endregion Private layout methods

        #region Private methods
        private void UpdateSudokuValues()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    if (string.IsNullOrWhiteSpace(cells[i][j].Text))
                    {
                        sudokuSolver.SetValue(i, j, 0);
                    }
                    else
                    {
                        if (int.TryParse(cells[i][j].Text, out int newValue))
                        {
                            sudokuSolver.SetValue(i, j, newValue);
                        }
                    }
                }
            }
        }

        private void UpdateCell(SudokuCell updatedCell)
        {
            cells[updatedCell.Row][updatedCell.Column].Text = updatedCell.Value.ToString();
        }

        private void ChangeSolverButtonText(string text)
        {
            BeginInvoke(new Action(() =>
            {
                btnSolvePuzzle.Text = text;
            }));
        }

        private void ClearFullGrid()
        {
            if (SaveTheGameAsk())
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    for (int j = 0; j < cells[i].Length; j++)
                    {
                        cells[i][j].Text = string.Empty;
                    }
                }
            }
        }
        #endregion Private methods

        #region Save game methods
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSaveDialog();
        }
        private bool SaveTheGameAsk()
        {
            if (MessageBox.Show("Do you want to save the current game before exiting?", "Save Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return OpenSaveDialog();
            }
            return true;
        }

        private bool OpenSaveDialog()
        {
            bool result = false;
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.Title = "Save Sudoku Game";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        SaveGridToFile(filePath);
                        result = true;
                        MessageBox.Show("Game saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving the game: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void SaveGridToFile(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    if (string.IsNullOrWhiteSpace(cells[i][j].Text)) { sb.Append(0); }
                    else { sb.Append(cells[i][j].Text); }

                    if (j < cells[i].Length - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }
            File.WriteAllText(filePath, sb.ToString());
        }
        #endregion Save game methods

        #region Open games methos
        private void OpenTheGame()
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

        private void LoadNextRandomGame()
        {
            try
            {
                string[] files = Directory.GetFiles(@"SavedGames", "*.txt");
                if (files.Length == 0)
                {
                    MessageBox.Show("No saved games found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                Random random = new Random();
                int randomIndex = random.Next(files.Length);
                string randomFile = files[randomIndex];

                sudokuSolver.FillGridFromFile(randomFile);
                DisplayValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the games: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Open games methos
    }
}
