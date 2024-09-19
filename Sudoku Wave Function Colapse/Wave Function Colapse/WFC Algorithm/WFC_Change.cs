using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    /*WFC_Change
     * This represents a single spot that was changed
     * Is meant to be used with the WFC_CheckPoints class
     * Made so that just in case more is needed than a singular point of data
     * Only stores a Point location as of now
     */
    internal class WFC_Change
    {
        private Point loc;
        public Point LocationOfChange { get { return loc; } }

        public WFC_Change(Point loc)
        {
            this.loc = loc;
        }
        public WFC_Change(int x, int y)
        {
            this.loc = new Point(x, y);
        }
    }
}
