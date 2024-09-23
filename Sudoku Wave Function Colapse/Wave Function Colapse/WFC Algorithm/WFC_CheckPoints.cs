using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    /*WFC_CheckPoints
     * A checkpoint is what is made when a branching choice is detected
     *  in the possible values the WFC can choose from
     * In this event, the checkpointClass will store that cell's location
     * In the event that a branching path does not lead to a proper solution,
     *  the checkpoint will be used to take a step back until there are no
     *  other valid options, or a solution is found
     * Indivually reverts the steps backwards
     */
    internal class WFC_CheckPoints
    {
        private Stack<WFC_Branch> checkPoints;
        public WFC_Branch LAST_BRANCH { get { return checkPoints.Peek(); } }
        public int NUMBER_OF_BRANCHES { get { return checkPoints.Count; } }
        public Point LATEST_LOC { get
            {
                if (checkPoints.Count > 0)
                {
                    return LAST_BRANCH.LocationOfChange;
                }
                return new Point(-1, -1);
            } }

        public WFC_CheckPoints()
        {
            checkPoints = new Stack<WFC_Branch>();
            addBranch(new WFC_Branch(new Point(-1, -1), -999));
        }

        public Point resetToLatestCheckPoint(int[,] currentValuesMap, Action<int[,], Point> resetAction)
        {
            resetAction(currentValuesMap, LATEST_LOC);   
            return LAST_BRANCH.LocationOfChange;
        }

        public Point resetToPreviousCheckPoint(int[,] currentValuesMap, Action<int[,], Point> resetAction)
        {
            resetToLatestCheckPoint(currentValuesMap, resetAction);
            System.Diagnostics.Debug.WriteLine("Resetting point:"+LATEST_LOC);
            checkPoints.Pop();
            //resetToLatestCheckPoint(currentValuesMap, resetAction);
            return LATEST_LOC;
        }
        public Point resetToPreviousCheckPoint(int[,] currentValuesMap)
        {
            return resetToPreviousCheckPoint(currentValuesMap, DEFAULT_RESETLOCATION);
        }

        public Point resetBackAmountOfCheckPoints(int[,] currentValuesMap, int numberToUndo, Action<int[,], Point> resetAction)
        {
            for(int i = Math.Min(numberToUndo, NUMBER_OF_BRANCHES); i>0; i--)
            {
                resetToLatestCheckPoint(currentValuesMap, resetAction);
                checkPoints.Pop();
            }
            return LATEST_LOC;
        }
        public Point resetBackAmountOfCheckPoints(int[,] currentValuesMap, int numberToUndo)
        {
            return resetBackAmountOfCheckPoints(currentValuesMap, numberToUndo);
        }

        public void addBranch(WFC_Branch branch)
        {
            System.Diagnostics.Debug.WriteLine("branching at " + branch.LocationOfChange);
            checkPoints.Push(branch);
            System.Diagnostics.Debug.WriteLine(" -Latest branch is at " + LATEST_LOC);
        }

        public bool validateToLastCheckPoint(Point p, int val)
        {
            System.Diagnostics.Debug.WriteLine("Validating To Last CheckPoint");
            WFC_Branch branch = LAST_BRANCH;
            bool samePoint = p.Equals(branch.LocationOfChange);
            if (samePoint)
            {
                return branch.collapse(val);
            }
            addBranch(new WFC_Branch(p, val));
            return false;
        }

        public List<int> removeImpossibleValues(List<int> values)
        {
            List<int> impossible = LAST_BRANCH.IMPOSSIBLE_VALUES;
            foreach(int i in impossible)
            {
                values.Remove(i);
            }
            return values;
        }
        public int[] removeImpossibleValues(int[] values)
        {
            return removeImpossibleValues(values.ToList<int>()).ToArray<int>();
        }

        public static void DEFAULT_RESETLOCATION(int[,] currentValuesMap, Point p)
        {
            currentValuesMap[p.Y,p.X] = WFC_Constants.DEFAULT_VALUE;
        }

        public String toString()
        {
            String s = "";
            s += "CheckPoint Length : " + NUMBER_OF_BRANCHES+"\n";
            foreach(WFC_Branch b in checkPoints)
            {
                s += b.toString();
                s += "\n";
            }
            return s;
        }
    }
}
