using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_SPC.Common
{
    public static class Validator
    {
        public static bool IsValidNumber(string input)
        {
            foreach(char c in input)
            {
                if (char.IsDigit(c) is false) return false;
            }
            return true;
        }

        public static void ProcessNewInput(RichTextBox rtb)
        {
            if (rtb.Text.Length > 1)
            {
                if (rtb.SelectionStart == 2)
                {
                    string newInput = rtb.Text.Substring(1, 1);
                    if (IsValidNumber(newInput))
                    {
                        rtb.Text = newInput;
                    }
                    else { rtb.Text = rtb.Text.Substring(0, 1); }
                }
                else
                {
                    string newInput = rtb.Text.Substring(0, 1);
                    if (IsValidNumber(newInput))
                    {
                        rtb.Text = newInput;
                    }
                    else { rtb.Text = rtb.Text.Substring(1, 1); }
                }
            }
            else
            {
                if (IsValidNumber(rtb.Text) is false)
                {
                    rtb.Text = string.Empty;
                }
            }
            rtb.SelectionAlignment = HorizontalAlignment.Center;
            rtb.SelectionStart = 1; // Ensure cursor remains after the character
        }
    }
}
