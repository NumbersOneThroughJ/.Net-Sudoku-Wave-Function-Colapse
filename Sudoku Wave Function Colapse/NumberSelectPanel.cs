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
    public partial class NumberSelectPanel : UserControl
    {
        private FontAutoSizer<Button> FontSizer;
        //Custom event on click, sends out associated number that was clicked
        public event EventHandler<int> PanelClicked;

        public NumberSelectPanel()
        {
            InitializeComponent();

            FontSizer = new FontAutoSizer<Button>();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        /*registerButtonsToSizer
         * Description :
         *      Shorthand function to call to register all buttons to sizer
         */
        private void registerButtonsToSizer()
        {
            FontSizer.addTarget(numberButton9);
        }

        //Event to resize fonts
        private void NumberSelectPanel_Resize(object sender, EventArgs e)
        {
            FontSizer.resizeTargetsFont();
        }

        private NumberButton getButton(int index)
        {
            switch (index)
            {
                case 1:
                    return numberButton1;
                case 2:
                    return numberButton2;
                case 3:
                    return numberButton3;
                case 4:
                    return numberButton4;
                case 5:
                    return numberButton5;
                case 6:
                    return numberButton6;
                case 7:
                    return numberButton7;
                case 8:
                    return numberButton8;
                case 9:
                    return numberButton9;
            }
            return numberButton1;
        }

        private void numberButton_Click(object sender, EventArgs e)
        {
            NumberButton NB = (NumberButton)sender;
            PanelClicked.Invoke(this, NB.Number);
        }

        private void resetEnableButtons(bool state)
        {
            for(int i = 0; i<10; i++) 
            {
                getButton(i).Enabled = state;
            }
        }

        /// <summary>
        /// Pass an array of possible values for this square
        /// </summary>
        /// <param name="enableds"></param>
        public void setPossibleNumbers(int[] enableds)
        {
            //MessageBox.Show(string.Join(", ", enableds));
            resetEnableButtons(false);
            foreach (int i in enableds) 
            {
                getButton(i).Enabled=true;
            }
        }

    }
}
