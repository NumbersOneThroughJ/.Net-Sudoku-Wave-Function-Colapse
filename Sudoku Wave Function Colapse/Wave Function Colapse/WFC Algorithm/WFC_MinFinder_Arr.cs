using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    internal class WFC_MinFinder_Arr
    {
        public static List<Point> findMinimums(int[][][] possibleValuesArr,int[,] currentVals, int[] endValues)
        {
            List<Point> mins = new List<Point>();
            int minimumCount = int.MaxValue;

            for(int y = 0; y< possibleValuesArr.Length; y++)
            {
                for (int x = 1; x< possibleValuesArr[y].Length; x++)
                {
                    int ArrLen = possibleValuesArr[y][x].Length;
                    if (!(endValues.Contains(currentVals[y,x])))
                    {
                        if (ArrLen < minimumCount)
                        {
                            mins.Clear();
                            mins.Add(new Point(x, y));
                            minimumCount = possibleValuesArr[y][x].Length;
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
