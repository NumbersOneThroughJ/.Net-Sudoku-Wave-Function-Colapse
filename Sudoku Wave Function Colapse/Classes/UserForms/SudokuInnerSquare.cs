using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sudoku_Wave_Function_Colapse.Classes;



namespace Sudoku_Wave_Function_Colapse
{
    public partial class SudokuInnerSquare : UserControl
    {
        //--------------------------------Variables

        //This squares sudoku number
        private int number = -1;
        public int Number { get { return number; } }
        private Color defaultColor;

        public event EventHandler<bool> NeedUpdate;



        //Font Autosizer Instance
        FontAutoSizer<RichTextBox> FontSizer;

        //-------------------------------- Constructors

        /* default Constructor
         * Desc :
         *      Initializes compenents, center aligns text, sets current font size, and sets number to -1
         */
        public SudokuInnerSquare()
        {
            InitializeComponent();

            NumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            FontSizer = new FontAutoSizer<RichTextBox>();
            FontSizer.addTarget(NumberTextBox);

            number = -1;
            NumberPanel.Visible = true;
            defaultColor = BackColor;
        }

        //-------------------------------- Events
        /* SudokuInnerSquare_Resize
         * Desc :
         *      Upon the resize of this, the numberTextBox.Text's font will be resized to fill the text
         */
        private void SudokuInnerSquare_Resize(object sender, EventArgs e)
        {
            FontSizer.resizeTargetsFont();
        }

        //-------------------------------- Private Functions

        /* setTextTool
         * Parameters :
         *      text : string 
         *          text to change the NumberTextBox.Text to
         * Returns : None
         * Desc :
         *      Changes teh NumberTextBox.Text to the supplied string
         */
        private void setTextTool(string text)
        {
            NumberTextBox.Text = text;
            NumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }


        /* setTextFromNumber
         * Parameters : None
         * Retruns : None
         * Desc :
         *      Sets the current Text to the value of the number integer
         *      Best called only after changing the value of the number
         */
        private void setTextFromNumber()
        {
            setTextTool(number.ToString());
        }

        /* resetText
         * Parameters : None
         * Returns : None
         * Desc :
         *      Private function to set text back to empty string
         */
        private void resetText()
        {
            setTextTool("");
        }

        //-------------------------------- Public Functions
        /* setNumber
         * Parameters :
         *      number : int
         *          Number to change to
         * Returns : None
         * Desc :
         *      Sets the number and changes the text displayed in the box. 
         *      Does nothing if number is not within range of -1  to 9
         *      Passing -1 as the number will reset the text
         */
        public void setNumber(int number)
        {
            if (number == -1)
            {
                resetNumber();
            }
            if (number >= 0 && number <= 9)
            {
                this.number = number;
                setTextFromNumber();
                NumberPanel.Visible = false;
            }
        }

        /* resetNumber
         * Parameters : none
         * Returns : None
         * Desc :
         *      Resets the current number of this small sudoku square to an empty string
         */
        public void resetNumber()
        {
            number = -1;
            resetText();
            NumberPanel.Visible=true;
        }

        private void NumberPanel_PanelClicked(object sender, int e)
        {
            NumberSelectPanel panel = (NumberSelectPanel)sender;

            panel.Visible = false;

            setNumber(e);

            NeedUpdate.Invoke(this, true);
        }

        private void NumberTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*
            int size = Math.Min(this.Size.Width, this.Size.Height);
            NumberPanel.Size = new Size(1000, 1000);
            NumberPanel.BringToFront();
            */
            resetNumber();
            NeedUpdate.Invoke(this, true);
            NumberPanel.Visible = true;
        }
        /// <summary>
        /// Pass int[] of available numbers
        /// </summary>
        /// <param name="availableNums"></param>
        public void setAvailableNumbers(int[] availableNums)
        {
            if ((!availableNums.Contains(number)) && (number != -1))
            {
                availableNums = availableNums.Concat([number]).ToArray();
                //MessageBox.Show(string.Join(", ", availableNums));
            }
            NumberPanel.setPossibleNumbers(availableNums);
        }

        public void setPriority(bool highlight)
        {
            BackColor = highlight ? Color.LightGoldenrodYellow : defaultColor;
        }
    }
}
