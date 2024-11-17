using System.Windows.Forms;

namespace Sudoku_SPC.Common
{
    public static class Validator
    {

        public static bool IsValidNumberAndNotZero(string input)
        {
            if (int.TryParse(input, out int num))
            {
                if (num != 0) return true;
            }
            return false;
        }

        public static void ProcessNewInput(RichTextBox rtb)
        {
            if (rtb.Text.Length > 1)
            {
                if (rtb.SelectionStart == 2)
                {
                    string newInput = rtb.Text.Substring(1, 1);
                    if (IsValidNumberAndNotZero(newInput))
                    {
                        rtb.Text = newInput;
                    }
                    else { rtb.Text = rtb.Text.Substring(0, 1); }
                }
                else
                {
                    string newInput = rtb.Text.Substring(0, 1);
                    if (IsValidNumberAndNotZero(newInput))
                    {
                        rtb.Text = newInput;
                    }
                    else { rtb.Text = rtb.Text.Substring(1, 1); }
                }
            }
            else
            {
                if (IsValidNumberAndNotZero(rtb.Text) is false)
                {
                    rtb.Text = string.Empty;
                }
            }
            rtb.SelectionAlignment = HorizontalAlignment.Center;
            rtb.SelectionStart = 1; // Ensure cursor remains after the character
        }
    }
}
