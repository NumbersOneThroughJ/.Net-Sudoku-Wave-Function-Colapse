using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    internal class WFC_MinFinder_Arr
    {
        /// <summary>
        /// Searches the provided map for the smallest amount of possible values on the map
        /// </summary>
        /// <param name="possibleValuesArr">2D array of Arrays of integers representing possible allowed values</param>
        /// <param name="currentVals">2D array of the current values that have been set already</param>
        /// <param name="endValues">Array of allowed endstate values</param>
        /// <returns></returns>
        public static List<Point> findMinimums(int[,][] possibleValuesArr,int[,] currentVals, int[] endValues)
        {
            List<Point> mins = new List<Point>();
            int minimumCount = int.MaxValue;

            for(int y = 0; y< possibleValuesArr.GetLength(0); y++)
            {
                for (int x = 0; x< possibleValuesArr.GetLength(1); x++)
                {
                    int ArrLen = possibleValuesArr[y, x].Length;
                    if (!(endValues.Contains(currentVals[y,x])))
                    {
                        if (ArrLen < minimumCount)
                        {
                            mins.Clear();
                            mins.Add(new Point(x, y));
                            minimumCount = possibleValuesArr[y, x].Length;
                        }
                        else if (ArrLen == minimumCount)
                        {
                            mins.Add(new Point(x, y));
                        }
                    }
                }
            }

            return mins;
        }
    }
}
