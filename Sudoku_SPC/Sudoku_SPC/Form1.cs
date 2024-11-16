using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_SPC
{
    public partial class Form1 : Form
    {
        private int widthField = 60;
        private int heightField = 50;
        private int padding = 2;
        private int lengthOfSquare = 2;

        public Form1()
        {
            InitializeComponent();

            InitializeSudokuMatrix();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void InitializeSudokuMatrix()
        {
            panelSudoku.Size = new Size();
            panelSudoku.Margin = new Padding();
            panelSudoku.Padding = new Padding();

            int offset = lengthOfSquare;
            for(int i = 0; i< lengthOfSquare*lengthOfSquare;i+=offset)
            {
                CreateSquare(i);
            }
        }

        private void CreateSquare(int offset)
        {
            for (int i = offset; i < lengthOfSquare+offset; i++)
            {
                for (int j = 0; j < lengthOfSquare* lengthOfSquare; j++)
                {
                    RichTextBox richTextBox = new RichTextBox
                    {
                        Margin = new Padding(2 * padding),
                        Padding = new Padding(),
                        Width = widthField,
                        Height = heightField,
                        Multiline = false,
                        Text = $"{i}",
                        Location = new System.Drawing.Point(
                            j * (widthField + padding) + (2 * padding),
                            i * (heightField + padding) + (2 * padding)),
                        BorderStyle = BorderStyle.None,
                        SelectionAlignment = HorizontalAlignment.Center,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 25.0f)
                    };
                    richTextBox.TextChanged += (sender, e) =>
                    {
                        RichTextBox rtb = (RichTextBox)sender;
                        if (rtb.Text.Length > 1)
                        {
                            if (rtb.SelectionStart == 2)
                            {
                                rtb.Text = rtb.Text.Substring(1, 1);
                            }
                            else
                            {
                                rtb.Text = rtb.Text.Substring(0, 1);
                            }
                            rtb.SelectionAlignment = HorizontalAlignment.Center;
                            rtb.SelectionStart = 1; // Ensure cursor remains after the character
                        }
                    };
                    this.panelSudoku.Controls.Add(richTextBox);
                }
            }
        }
    }
}
