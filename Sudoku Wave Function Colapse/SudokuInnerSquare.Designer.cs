namespace Sudoku_Wave_Function_Colapse
{
    partial class SudokuInnerSquare
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            NumberTextBox = new RichTextBox();
            NumberPanel = new NumberSelectPanel();
            SuspendLayout();
            // 
            // NumberTextBox
            // 
            NumberTextBox.Dock = DockStyle.Fill;
            NumberTextBox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NumberTextBox.Location = new Point(0, 0);
            NumberTextBox.Name = "NumberTextBox";
            NumberTextBox.ReadOnly = true;
            NumberTextBox.ScrollBars = RichTextBoxScrollBars.None;
            NumberTextBox.Size = new Size(150, 150);
            NumberTextBox.TabIndex = 0;
            NumberTextBox.Text = "9";
            NumberTextBox.MouseDoubleClick += NumberTextBox_MouseDoubleClick;
            // 
            // NumberPanel
            // 
            NumberPanel.Dock = DockStyle.Fill;
            NumberPanel.Location = new Point(0, 0);
            NumberPanel.Margin = new Padding(0);
            NumberPanel.Name = "NumberPanel";
            NumberPanel.Size = new Size(150, 150);
            NumberPanel.TabIndex = 1;
            NumberPanel.Visible = false;
            NumberPanel.PanelClicked += NumberPanel_PanelClicked;
            // 
            // SudokuInnerSquare
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(NumberPanel);
            Controls.Add(NumberTextBox);
            Name = "SudokuInnerSquare";
            Resize += SudokuInnerSquare_Resize;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox NumberTextBox;
        private NumberSelectPanel NumberPanel;
    }
}
