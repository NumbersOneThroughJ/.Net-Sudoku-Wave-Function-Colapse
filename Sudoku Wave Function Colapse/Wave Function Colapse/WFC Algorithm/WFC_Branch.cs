using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    /*WFC_Change
     * This represents a single spot that was changed
     * Contains a list of impossible values for a location
     */
    internal class WFC_Branch
    {
        private Point loc;
        public Point LocationOfChange { get { return loc; } }
        public int X { get {  return loc.X; } }
        public int Y { get { return loc.Y; } }

        private List<int> impossibleValues;
        public List<int> IMPOSSIBLE_VALUES { get { return impossibleValues; } }

        //Constructors
        public WFC_Branch(Point loc)
        {
            this.loc = loc;
            impossibleValues = new List<int>();
        }
        public WFC_Branch(Point loc, int value)
        {
            this.loc = loc;
            impossibleValues = new List<int>();
            impossibleValues.Add(value);
        }
        public WFC_Branch(int x, int y)
        {
            this.loc = new Point(x, y);
            impossibleValues = new List<int>();
        }
        public WFC_Branch(int x, int y, int value)
        {
            this.loc = new Point(x, y);
            impossibleValues = new List<int>();
            impossibleValues.Add(value);
        }

        //Functions

        /*validateValue
         * Desc:
         *  Used to quickly check if a value is blacklisted or not
         * Parameters:
         *  value : int to check
         * Returns:
         *  bool : true if not dissalowed, false if otherwise
         */
        public bool validateValue(int value)
        {
            return !impossibleValues.Contains(value);
        }

        /*addCollapsedValue
         * Desc:
         *  Used to add a value to the blacklist
         * Parameters:
         *  value : int to add
         * Returns:
         *  None
         */
        public void addCollapsedValue(int value)
        {
            if (!impossibleValues.Contains(value))
            {
                IMPOSSIBLE_VALUES.Add(value);
            }
        }

        /*collapse
         * Desc:
         *  Shorthanded combined functions for cehcking value and
         *  adding said value to the list
         *  This assumes that if the value is not dissalowed, that
         *  you are using it immediately afterwards.
         * Parameters:
         *  value : int in question
         * Returns:
         *  bool : true if the value was not blacklisted, false if otherwise
         */
        public bool collapse(int value)
        {
            if (validateValue(value))
            {
                addCollapsedValue(value);
                return true;
            }
            return false;
        }

    }
}
