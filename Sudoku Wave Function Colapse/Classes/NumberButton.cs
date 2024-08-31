using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Classes
{
    internal class NumberButton : Button
    {
        private int number;

        [Browsable(true)] // This makes the property visible in the designer
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                // Perform any additional actions when the property is set if needed
            }
        }
        public NumberButton()
        {
            Number = -1;
        }


    }
}
