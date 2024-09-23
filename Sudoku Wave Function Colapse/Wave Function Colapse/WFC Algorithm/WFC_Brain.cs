using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    /// <summary>
    /// The main deciding portion of the WFC algorithm, the brain
    /// This determines which values to choose and where to choose from
    /// </summary>
    internal class WFC_Brain
    {
        int[] finalVals;
        Random rand;

        /// <summary>
        /// Constructs an WFC_Brain with the valid finalVals set to finalVals
        /// </summary>
        /// <param name="finalVals">valid ending vals</param>
        public WFC_Brain(int[] finalVals)
        {
            this.finalVals = finalVals;
            rand = new Random();
        }

        /// <summary>
        /// Determines the best next Point to collapse based on the WFC_MinFinder Class
        /// </summary>
        /// <param name="possVals">Map of arrays of the possible values at each point</param>
        /// <param name="map">2D array of current values</param>
        /// <returns>Point of the next optimal collapse point</returns>
        protected Point getNextCollapsePoint(int[,][] possVals, int[,] map)
        {
            List<Point> minimumPoints =WFC_MinFinder_Arr.findMinimums(possVals, map, finalVals);
            int i = rand.Next(minimumPoints.Count);
            return minimumPoints.ElementAt(i);
        }
        /// <summary>
        /// Chooses a value from a point on the provided map of possible values array
        /// </summary>
        /// <param name="possVals">2D map of arrays of possible values</param>
        /// <param name="p">Point to choose a possible value from</param>
        /// <returns>the chosen value integer</returns>
        protected int chooseValueAtPoint(int[,][] possVals, Point p)
        {
            int i = rand.Next(possVals[p.Y, p.X].Length - 1);
            return possVals[p.Y, p.X][i];
        }
        /// <summary>
        /// Chooses a value from the provided array of possible values
        /// </summary>
        /// <param name="possVals">Array of possible values</param>
        /// <returns>the chosen value integer</returns>
        protected int chooseValue(int[] possVals)
        {
            int i = rand.Next(possVals.Length - 1);
            return possVals[i];
        }
        /// <summary>
        /// Chooses a point, then chooses the value
        /// Validates the point with the provided checkpoints
        /// if there are no valid points, it will step back a number of steps until it can collapse a point
        /// </summary>
        /// <param name="map">2D array of current values</param>
        /// <param name="updateMap">Function used to set a value to the output map</param>
        /// <param name="resetSquare">Function used to reset a square</param>
        /// <param name="getPossibles">Function used to get Possible Values</param>
        /// <param name="checkPoints">Checkpoints used to record branches and verify points validity</param>
        /// <returns></returns>
        public Point collapseAPoint(int[,] map, Action<int[,], Point, int> updateMap, Action<int[,], Point> resetSquare, Func<int[,],int[,][]> getPossibles, WFC_CheckPoints checkPoints)
        {
            int[,][] possVals = getPossibles(map);
            Point p = getNextCollapsePoint(possVals, map);
            int[] possibles = checkPoints.removeImpossibleValues(possVals[p.Y,p.X], p);
            
            while (possibles.Length == 0)
            {
                p = checkPoints.resetToPreviousCheckPoint(map, resetSquare);
                possVals = getPossibles(map);
                possibles = checkPoints.removeImpossibleValues(possVals[p.Y, p.X], p);
            }
            int choice;
            do
            {
                choice = chooseValue(possibles);
            } while (checkPoints.validateToLastCheckPoint(p, choice));
            updateMap(map, p, choice);
            
            return p;
        }        
    }
}
