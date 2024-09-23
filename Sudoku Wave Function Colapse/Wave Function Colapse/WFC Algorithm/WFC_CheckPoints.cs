using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    /// <summary>
    /// A checkpoint is what is made when a branching choice is detected
    ///  in the possible values the WFC can choose from
    /// In this event, the checkpointClass will store that cell's location
    /// In the event that a branching path does not lead to a proper solution,
    ///  the checkpoint will be used to take a step back until there are no
    ///  other valid options, or a solution is found
    /// 
    /// Easy to use, use the functions 
    ///  validateToLastCheckpoint to handle checking values and checkpoint recording
    ///  resetToPreviousCheckPoint to reset back one checkpoint
    ///  resetBackAmountOfCheckPoints to reset back a set amount of checkpoints
    /// </summary>
    internal class WFC_CheckPoints
    {
        //Variabls
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

        /// <summary>
        /// Default constructor
        /// Adds a default checkpoint to the start of the array, do not use this value
        ///  the added WFC_Branch is at point (-1,-1) value of -999
        /// </summary>
        public WFC_CheckPoints()
        {
            checkPoints = new Stack<WFC_Branch>();
            addBranch(new WFC_Branch(new Point(-1, -1), -999));
        }

        /// <summary>
        /// Resets the map based on the latest checkpoint using the provided resetAction
        /// </summary>
        /// <param name="currentValuesMap">currentValuesMap : int[,] the source map of the values</param>
        /// <param name="resetAction">resetAction : Action<int[,], Point> the function that returns nothing and edits the provided map</param>
        /// <returns>Point of the change</returns>
        public Point resetToLatestCheckPoint(int[,] currentValuesMap, Action<int[,], Point> resetAction)
        {
            resetAction(currentValuesMap, LATEST_LOC);   
            return LAST_BRANCH.LocationOfChange;
        }

        /// <summary>
        /// Resets the map based on the pevious checkpoint using the provided resetAction
        /// removes the latest checkpoint
        /// </summary>
        /// <param name="currentValuesMap">the source map of the values</param>
        /// <param name="resetAction">the function that returns nothing and edits the provided map</param>
        /// <returns>Point of the new latest checkpoint, use this to continue checking from</returns>
        public Point resetToPreviousCheckPoint(int[,] currentValuesMap, Action<int[,], Point> resetAction)
        {
            resetToLatestCheckPoint(currentValuesMap, resetAction);
            checkPoints.Pop();
            return LATEST_LOC;
        }

        /// <summary>
        /// Resets the map based on the pevious checkpoint using the default resetAction
        /// removes the latest checkpoint
        /// </summary>
        /// <param name="currentValuesMap">The source map of the values</param>
        /// <returns>Point of the new latest checkpoint, use this to continue checking from</returns>
        public Point resetToPreviousCheckPoint(int[,] currentValuesMap)
        {
            return resetToPreviousCheckPoint(currentValuesMap, WFC_DEFAULTFUNCTIONS.DEFAULT_RESETLOCATION);
        }

        /// <summary>
        /// Resets back the provided number of checkpoints
        /// </summary>
        /// <param name="currentValuesMap">The source map of values to change</param>
        /// <param name="numberToUndo">The number of checkpoints to go back</param>
        /// <param name="resetAction">Function that returns nothing, but resets the provided map location</param>
        /// <returns>Point of the new latest checkpoint, use this to continue checking from</returns>
        public Point resetBackAmountOfCheckPoints(int[,] currentValuesMap, int numberToUndo, Action<int[,], Point> resetAction)
        {
            for(int i = Math.Min(numberToUndo, NUMBER_OF_BRANCHES); i>0; i--)
            {
                resetToLatestCheckPoint(currentValuesMap, resetAction);
                checkPoints.Pop();
            }
            return LATEST_LOC;
        }
        /// <summary>
        /// Resets back the provided number of checkpoints using the default reset action
        /// </summary>
        /// <param name="currentValuesMap">The source map of values to change</param>
        /// <param name="numberToUndo">The number of checkpoints to go back</param>
        /// <returns>Point of the new latest checkpoint, use this to continue checking from</returns>
        public Point resetBackAmountOfCheckPoints(int[,] currentValuesMap, int numberToUndo)
        {
            return resetBackAmountOfCheckPoints(currentValuesMap, numberToUndo);
        }
        /// <summary>
        /// Adds a branch to the checkpoints
        /// </summary>
        /// <param name="branch">Branch to add</param>
        public void addBranch(WFC_Branch branch)
        {
            checkPoints.Push(branch);
        }
        /// <summary>
        /// Returns a true value if based on the latest checkpoint, the val is allowed
        ///  returns false if the point is not the same as the latest, or if the val is not allowed
        /// If the val is allowed, the function will add it to the checkpoints
        /// This function assumes that if it returns true, you will use that value
        /// </summary>
        /// <param name="p"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool validateToLastCheckPoint(Point p, int val)
        {
            WFC_Branch branch = LAST_BRANCH;
            bool samePoint = p.Equals(branch.LocationOfChange);
            if (samePoint)
            {
                return branch.collapse(val);
            }
            addBranch(new WFC_Branch(p, val));
            return false;
        }

        /// <summary>
        /// Takes a list of ints to remove the impossible values based on the latest checkpoint
        /// </summary>
        /// <param name="values">List to modify</param>
        /// <returns>a modified list</returns>
        public List<int> removeImpossibleValues(List<int> values, Point p)
        {
            if (LATEST_LOC.Equals(p))
            {
                List<int> impossible = LAST_BRANCH.IMPOSSIBLE_VALUES;
                foreach (int i in impossible)
                {
                    values.Remove(i);
                }
            }
            return values;
        }
        /// <summary>
        /// Takes an array of ints and removes impossible values based ont he latest checkpoint
        /// </summary>
        /// <param name="values">Array to modify</param>
        /// <returns>Modified array</returns>
        public int[] removeImpossibleValues(int[] values, Point p)
        {
            return removeImpossibleValues(values.ToList<int>(), p).ToArray<int>();
        }
        /// <summary>
        /// Returns a string of the summary of all checkpoints
        /// </summary>
        /// <returns>returns String of summary of checkpoints</returns>
        public override String ToString()
        {
            String s = "";
            s += "CheckPoint Length : " + NUMBER_OF_BRANCHES+"\n";
            foreach(WFC_Branch b in checkPoints)
            {
                s += b.ToString();
                s += "\n";
            }
            return s;
        }
    }
}
