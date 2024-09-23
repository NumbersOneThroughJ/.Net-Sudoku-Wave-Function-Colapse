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
        //Variables
        private Point loc;
        public Point LocationOfChange { get { return loc; } }
        public int X { get {  return loc.X; } }
        public int Y { get { return loc.Y; } }
        private List<int> impossibleValues;
        public List<int> IMPOSSIBLE_VALUES { get { return impossibleValues; } }

        /// <summary>
        /// Creates a branch at the point with an empty list of values
        /// </summary>
        /// <param name="loc">Point to branch from</param>
        public WFC_Branch(Point loc)
        {
            this.loc = loc;
            impossibleValues = new List<int>();
        }
        /// <summary>
        /// Creates a branch at the point with a list with the only value being the provided value
        /// </summary>
        /// <param name="loc">Point to branch from</param>
        /// <param name="value">Value to set as impossible</param>
        public WFC_Branch(Point loc, int value)
        {
            this.loc = loc;
            impossibleValues = new List<int>();
            impossibleValues.Add(value);
        }
        /// <summary>
        /// Creates a branch at the provided x y cordinates with an empty list of values
        /// </summary>
        /// <param name="x">x cordinate</param>
        /// <param name="y">y cordinate</param>
        public WFC_Branch(int x, int y)
        {
            this.loc = new Point(x, y);
            impossibleValues = new List<int>();
        }
        /// <summary>
        /// Creates a branch at the provided x y cordinates with an empty list of values
        /// </summary>
        /// <param name="x">x cordinate</param>
        /// <param name="y">y cordinate</param>
        /// <param name="value">Value to set as impossible</param>
        public WFC_Branch(int x, int y, int value)
        {
            this.loc = new Point(x, y);
            impossibleValues = new List<int>();
            impossibleValues.Add(value);
        }

        //Functions
        /// <summary>
        /// Used to quickly check if a value is blacklisted or not
        /// </summary>
        /// <param name="value">Integer value to check</param>
        /// <returns>true if dissalowed, false if allowed</returns>
        public bool validateValue(int value)
        {
            return !impossibleValues.Contains(value);
        }
        /// <summary>
        /// Used to add a value to the blacklist
        /// </summary>
        /// <param name="value">integer to add</param>
        public void addCollapsedValue(int value)
        {
            if (!impossibleValues.Contains(value))
            {
                IMPOSSIBLE_VALUES.Add(value);
            }
        }
        /// <summary>
        /// Shorthanded combined functions for cehcking value and
        /// adding said value to the list
        /// This assumes that if the value is not dissalowed, that
        ///  you are using it immediately afterwards.
        /// </summary>
        /// <param name="value">Integer to check</param>
        /// <returns>true if the value was not blacklisted, false if not allowed</returns>
        public bool collapse(int value)
        {
            if (validateValue(value))
            {
                addCollapsedValue(value);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns a string of the current location point, and the list of impossible values
        /// </summary>
        /// <returns>String of the point and value</returns>
        public override String ToString()
        {
            String s = "";
            s += loc + " :\n   ";
            foreach(int i in impossibleValues)
            {
                s += ", " + i;
            }
            return s;
        }

    }
}
