using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    /// <summary>
    /// 
    /// </summary>
    internal class WFC_Manager
    {
        //Function References
        Func<int[,], int[,][]> getPossibleValues;
        Action<int[,], Point, int> setMapValue;
        Action<int[,], Point> resetMapAtLocation; 

        //Objects
        WFC_CheckPoints checkPoints;
        WFC_Brain brain;

        //Variables
        int[] validEndValues;

        //Constructors

        /// <summary>
        /// Creates a WFC_Manager with using the default functions
        /// </summary>
        /// <param name="getPossibleValues">Function to get possible data from current values</param>
        /// <param name="validEndValues">Array of valid end values</param>
        public WFC_Manager(Func<int[,], int[,][]> getPossibleValues, int[] validEndValues)
        {
            this.getPossibleValues = getPossibleValues;
            setMapValue = WFC_DEFAULTFUNCTIONS.DEFAULT_SETMAPVALUE;
            resetMapAtLocation = WFC_DEFAULTFUNCTIONS.DEFAULT_RESETLOCATION;
            this.validEndValues = validEndValues;
            checkPoints = new WFC_CheckPoints();
            brain = new WFC_Brain(validEndValues);
        }
        /// <summary>
        /// Creates a WFC_Manager using the provided functions
        /// </summary>
        /// <param name="getPossibleValues">Function to get possible data from current values</param>
        /// <param name="setMapValue">Function to update current values with new values</param>
        /// <param name="resetMapAtLocation">Function to reset a spot on the current values to an empty value</param>
        /// <param name="validEndValues">Array of valid end values</param>
        public WFC_Manager(Func<int[,], int[,][]> getPossibleValues, Action<int[,], Point, int> setMapValue, Action<int[,], Point> resetMapAtLocation, int[] validEndValues) : this(getPossibleValues,validEndValues)
        {
            this.setMapValue = setMapValue;
            this.resetMapAtLocation = resetMapAtLocation;
            this.validEndValues = validEndValues;
            checkPoints = new WFC_CheckPoints();
            brain = new WFC_Brain(validEndValues);
        }
        /// <summary>
        /// Collapses an entire 2D map into a solution valid to the provided getPossibleData
        /// </summary>
        /// <param name="valuesMap">2D map of current values</param>
        /// <returns>2D map of a possible solution</returns>
        public int[,] CollapseFullMap(int[,] valuesMap)
        {
            int i = 0;

            while (!verifyEnd(valuesMap))
            {
                brain.collapseAPoint(valuesMap, setMapValue, resetMapAtLocation, getPossibleValues, checkPoints);
            }
            return valuesMap;
        }
        /// <summary>
        /// Checks the provided map to see if all values are valid ending values
        /// </summary>
        /// <param name="map">2D array of values</param>
        /// <returns>TRUE if all values are valid end values, FALSE if not all values are valid end values</returns>
        public bool verifyEnd(int[,] map)
        {
            foreach(int i in map)
            {
                if (!validEndValues.Contains(i)) return false;
            }
            return true;
        }
    }
}
