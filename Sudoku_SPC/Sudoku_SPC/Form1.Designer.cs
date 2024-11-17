namespace Sudoku_SPC
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSudoku = new System.Windows.Forms.Panel();
            this.btnSolvePuzzle = new System.Windows.Forms.Button();
            this.cbVisualization = new System.Windows.Forms.CheckBox();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTheGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNextGame = new System.Windows.Forms.Button();
            this.btnSaveGame = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(725, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // panelSudoku
            // 
            this.panelSudoku.AutoSize = true;
            this.panelSudoku.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelSudoku.Location = new System.Drawing.Point(39, 90);
            this.panelSudoku.Margin = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelSudoku.Name = "panelSudoku";
            this.panelSudoku.Size = new System.Drawing.Size(648, 465);
            this.panelSudoku.TabIndex = 1;
            // 
            // btnSolvePuzzle
            // 
            this.btnSolvePuzzle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolvePuzzle.Location = new System.Drawing.Point(64, 47);
            this.btnSolvePuzzle.Margin = new System.Windows.Forms.Padding(4);
            this.btnSolvePuzzle.Name = "btnSolvePuzzle";
            this.btnSolvePuzzle.Size = new System.Drawing.Size(200, 43);
            this.btnSolvePuzzle.TabIndex = 2;
            this.btnSolvePuzzle.Text = "Solve puzzle";
            this.btnSolvePuzzle.UseVisualStyleBackColor = true;
            this.btnSolvePuzzle.Click += new System.EventHandler(this.btnSolvePuzzle_Click);
            // 
            // cbVisualization
            // 
            this.cbVisualization.AutoSize = true;
            this.cbVisualization.Location = new System.Drawing.Point(315, 47);
            this.cbVisualization.Name = "cbVisualization";
            this.cbVisualization.Size = new System.Drawing.Size(104, 20);
            this.cbVisualization.TabIndex = 3;
            this.cbVisualization.Text = "Visualization";
            this.cbVisualization.UseVisualStyleBackColor = true;
            this.cbVisualization.CheckedChanged += new System.EventHandler(this.cbVisualization_CheckedChanged);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearTheGridToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // clearTheGridToolStripMenuItem
            // 
            this.clearTheGridToolStripMenuItem.Name = "clearTheGridToolStripMenuItem";
            this.clearTheGridToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.clearTheGridToolStripMenuItem.Text = "Clear the grid";
            this.clearTheGridToolStripMenuItem.Click += new System.EventHandler(this.clearTheGridToolStripMenuItem_Click);
            // 
            // btnNextGame
            // 
            this.btnNextGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextGame.Location = new System.Drawing.Point(426, 24);
            this.btnNextGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnNextGame.Name = "btnNextGame";
            this.btnNextGame.Size = new System.Drawing.Size(200, 43);
            this.btnNextGame.TabIndex = 4;
            this.btnNextGame.Text = "Next game";
            this.btnNextGame.UseVisualStyleBackColor = true;
            this.btnNextGame.Click += new System.EventHandler(this.btnNextGame_Click);
            // 
            // btnSaveGame
            // 
            this.btnSaveGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveGame.Location = new System.Drawing.Point(512, 32);
            this.btnSaveGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveGame.Name = "btnSaveGame";
            this.btnSaveGame.Size = new System.Drawing.Size(200, 43);
            this.btnSaveGame.TabIndex = 5;
            this.btnSaveGame.Text = "Save game";
            this.btnSaveGame.UseVisualStyleBackColor = true;
            this.btnSaveGame.Click += new System.EventHandler(this.btnSaveGame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(725, 599);
            this.Controls.Add(this.btnSaveGame);
            this.Controls.Add(this.cbVisualization);
            this.Controls.Add(this.btnNextGame);
            this.Controls.Add(this.btnSolvePuzzle);
            this.Controls.Add(this.panelSudoku);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Panel panelSudoku;
        private System.Windows.Forms.Button btnSolvePuzzle;
        private System.Windows.Forms.CheckBox cbVisualization;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTheGridToolStripMenuItem;
        private System.Windows.Forms.Button btnSaveGame;
        private System.Windows.Forms.Button btnNextGame;
    }
}

