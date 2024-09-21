using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    internal class WFC_Brain
    {
        int[] finalVals;
        Random rand;

        public WFC_Brain(int[] finalVals)
        {
            this.finalVals = finalVals;
            rand = new Random((int)System.DateTime.Now.Ticks);
        }
        public Point getNextCollapsePoint(int[][][] possVals, int[,] map)
        {
            List<Point> minimumPoints =WFC_MinFinder_Arr.findMinimums(possVals, map, finalVals);
            int i = rand.Next(minimumPoints.Count - 1);
            return minimumPoints.ElementAt(i);
        }

        public int chooseValueAtPoint(int[][][] possVals, Point p)
        {
            int i = rand.Next(possVals[p.Y][p.X].Length - 1);
            return possVals[p.Y][p.X][i];
        }

        public Point collapseAPoint(int[][][] possVals, int[,] map, Action<Point, int> updateMap)
        {
            //in here needs to use checkpoints system, then we are golden!
            Point p = getNextCollapsePoint(possVals, map);
            int choice = chooseValueAtPoint(possVals, p);
            updateMap(p, choice);
            return p;
        }
        

    }
}
