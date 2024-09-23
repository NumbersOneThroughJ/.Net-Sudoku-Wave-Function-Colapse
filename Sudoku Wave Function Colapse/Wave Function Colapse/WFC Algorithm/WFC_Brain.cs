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
            rand = new Random();

            p1 = new Point(-1, -1);
            doP1 = false;
        }

        public Point getNextCollapsePoint(int[][][] possVals, int[,] map)
        {
            List<Point> minimumPoints =WFC_MinFinder_Arr.findMinimums(possVals, map, finalVals);
            int i = rand.Next(minimumPoints.Count);
            return minimumPoints.ElementAt(i);
        }

        public int chooseValueAtPoint(int[][][] possVals, Point p)
        {
            int i = rand.Next(possVals[p.Y][p.X].Length - 1);
            return possVals[p.Y][p.X][i];
        }
        public int chooseValue(int[] possVals)
        {
            int i = rand.Next(possVals.Length - 1);
            return possVals[i];
        }

        Point p1;
        bool doP1;

        public Point collapseAPoint(int[,] map, Action<Point, int> updateMap, Action<int[,], Point> resetSquare, WFC_CheckPoints checkPoints, Func<int[][][]> getPossibles)
        {
            //in here needs to use checkpoints system, then we are golden!
            int[][][] possVals = getPossibles();
            Point p = getNextCollapsePoint(possVals, map);
            int[] possibles = checkPoints.removeImpossibleValues(possVals[p.Y][p.X]);
            /*
            if (doP1)
            {
                p = p1;
                doP1 = false;
            }
            if(possibles.Length == 0)
            {
                p = checkPoints.resetToPreviousCheckPoint(map, resetSquare);
                p1 = p;
                doP1 = true;
            } else
            {
                int choice = chooseValue(possibles);
                checkPoints.validateToLastCheckPoint(p, choice);
                updateMap(p, choice);
            }
            */
            
            while (possibles.Length == 0)
            {
                System.Diagnostics.Debug.WriteLine(p + " : \n ");
                foreach(int i in possVals[p.Y][p.X]) System.Diagnostics.Debug.Write(", " + i);
                p = checkPoints.resetToPreviousCheckPoint(map, resetSquare);
                possVals = getPossibles();
                possibles = checkPoints.removeImpossibleValues(possVals[p.Y][p.X]);
            }
            int choice;
            do
            {
                choice = chooseValue(possibles);
            } while (checkPoints.validateToLastCheckPoint(p, choice));
            updateMap(p, choice);
            
            return p;
        }        
    }
}
