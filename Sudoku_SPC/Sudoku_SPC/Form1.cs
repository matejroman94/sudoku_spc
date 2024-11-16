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

namespace Sudoku_SPC
{
    public partial class Form1 : Form
    {
        private int lengthOfSquare = 3;
        private int widthField = 60;
        private int heightField = 50;
        private int padding = 2;

        private SudokuSolver sudokuSolver;
        public Form1()
        {
            InitializeComponent();
            InitializeSudokuMatrix();
            this.CenterToScreen();

            sudokuSolver = new SudokuSolver(lengthOfSquare*lengthOfSquare);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CenterPanel(Panel panel)
        {
            // Calculate the center position
            int x = (this.ClientSize.Width - panel.Width) / 2;
            int y = (this.ClientSize.Height - panel.Height) / 2;

            // Set the panel's location
            panel.Location = new Point(x, y);
        }

        private void InitializeSudokuMatrix()
        {
            panelSudoku.Size = new Size();
            panelSudoku.Margin = new Padding(100);
            panelSudoku.Padding = new Padding();

            int offset = lengthOfSquare;
            int borderRow = 0;
            for (int i = 0; i< lengthOfSquare*lengthOfSquare;i+=offset)
            {
                borderRow += i % lengthOfSquare == 0 ? (i == 0 ? 2 : 1) * padding : 0;
                CreateSquare(i, borderRow);
            }
            CenterPanel(panelSudoku);
        }



        private void CreateSquare(int offset,int borderRow)
        {
            for (int i = offset; i < lengthOfSquare+offset; i++)
            {
                int borderColumn = 0;
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
                    this.panelSudoku.Controls.Add(richTextBox);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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
                        string fileContent = File.ReadAllText(filePath);
                        contentTextBox.Text = fileContent; // Display contents in the TextBox
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading file: " + ex.Message);
                    }
                }
            }
        }
    }
}
