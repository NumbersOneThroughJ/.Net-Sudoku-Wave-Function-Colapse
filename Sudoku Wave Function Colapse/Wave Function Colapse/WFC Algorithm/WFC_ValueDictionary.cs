using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    internal class WFC_ValueDictionary<e> where e : notnull
    {
        private Stack<e> valueDic;
        private int latestIndex;
        public int SIZE { get { return latestIndex; } }

        public WFC_ValueDictionary()
        {
            latestIndex -= 1;
            valueDic = new Stack<e>();
        }

        /* addToDictionary
         * Desc:
         *   Adds an item to the end of the dictionary
         * Parameters:
         *   item : of type e to be added
         * Returns:
         *   Integer representing the index of the item added
         */
        public int addToDictionary(e item)
        {
            if(!valueDic.Contains(item)) 
            {
                latestIndex++;
                valueDic.Push(item);
                return latestIndex;
            }
            return -1;
        }
        /*convertToValue
         * Desc:
         *   converts an index to a value, assumes index exists
         * Parameters:
         *   i : int of index of item to get
         * Returns:
         *   e value of what was assigned via addToDictionary
         */
        public e convertToValue(int i)
        {
            return valueDic.ElementAt(i);
        }
    }
}
