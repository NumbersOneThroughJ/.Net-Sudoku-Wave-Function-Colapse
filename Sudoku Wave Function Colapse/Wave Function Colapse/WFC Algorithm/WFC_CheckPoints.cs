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

        public Point resetToLatestCheckPoint(int[][] currentValuesMap)
        {
            currentValuesMap[LAST_BRANCH.Y][LAST_BRANCH.X] = WFC_Constants.DEFAULT_VALUE;
            return LAST_BRANCH.LocationOfChange;
        }

        public Point resetToPreviousCheckPoint(int[][] currentValuesMap)
        {
            resetToLatestCheckPoint(currentValuesMap);
            checkPoints.Pop();
            if(checkPoints.Count > 0)
            {
                resetToLatestCheckPoint(currentValuesMap);
                return LAST_BRANCH.LocationOfChange;
            }
            return new Point(-1, -1);
        }

        public Point resetBackAmountOfCheckPoints(int[][] currentValuesMap, int numberToUndo)
        {
            for(int i = Math.Min(numberToUndo, NUMBER_OF_BRANCHES); i>0; i--)
            {
                resetToLatestCheckPoint(currentValuesMap);
                checkPoints.Pop();
            }
            return LATEST_LOC;
        }

        public void addBranch(WFC_Branch branch)
        {
            checkPoints.Push(branch);
        }



    }
}
