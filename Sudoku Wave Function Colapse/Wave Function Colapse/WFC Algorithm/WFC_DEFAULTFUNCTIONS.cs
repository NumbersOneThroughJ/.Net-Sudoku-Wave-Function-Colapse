using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    internal class WFC_DEFAULTFUNCTIONS
    {
        /// <summary>
        /// The default reset location function
        /// This function will reset the map at point p to the value defined in WFC_Constants as Default Value
        /// Default Value is -1
        /// </summary>
        /// <param name="currentValuesMap">Map to modify</param>
        /// <param name="p">Point to reset at</param>
        public static void DEFAULT_RESETLOCATION(int[,] currentValuesMap, Point p)
        {
            currentValuesMap[p.Y, p.X] = WFC_Constants.DEFAULT_VALUE;
        }

        /// <summary>
        /// The default set map value function
        /// This function will just set the provided map at location p to the value provided
        /// </summary>
        /// <param name="map">2D map of values to change</param>
        /// <param name="p">Point to change at</param>
        /// <param name="value">Value to set</param>
        public static void DEFAULT_SETMAPVALUE(int[,] map, Point p, int value)
        {
            map[p.Y, p.X] = value;
        }
    }
}
