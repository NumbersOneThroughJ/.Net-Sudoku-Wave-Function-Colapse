using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D
{


    /// <summary>
    /// JACOB UPDATE THIS:
    ///    REMOVE THE REFERENCES TO A NEW Table everywhere.
    ///    Have it edit the tables directly. That will help cut out a looooot of processing time for allocating arrays.
    ///    Array.copy is what is taking most of the time. If I can minimize it, I can do it a little bit at a time.
    /// </summary>
    internal interface IRule_2D_Base
    {
        //[Row, Collum]
        //Returns true if all conditions allow the current Value
        public bool evaluatePoint(Point p, int[,] data);
        public bool evaluatePoint(int x, int y, int[,] data) { return evaluatePoint(new Point(x, y), data); }
        public bool evaluateFullTable(int[,] data)
        {
            for(int y = 0; y< data.GetLength(0); y++)
                for(int x = 0; x<data.GetLength(1); x++)
                {
                    if (!evaluatePoint(x, y, data)) return false;
                }
            return true;
        }
        //Returns a int[,] data map with the available possible points to this rule
        public PossibleValuesMap getPossibleDataAboutPoint(Point p, int[,] data, PossibleValuesMap currentValues, bool blackListPriority = true);
        public PossibleValuesMap getPossibleDataAboutPoint(int x, int y, int[,] data, PossibleValuesMap currentValues, bool blackListPriority = true) { return getPossibleDataAboutPoint(new Point(x, y), data, currentValues, blackListPriority); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="blackListPriority"></param>
        /// <returns></returns>
        public PossibleValuesMap getFullTablePossibleData(int[,] data, bool blackListPriority =true)
        {
            PossibleValuesMap returnTable = new PossibleValuesMap(data.GetLength(1), data.GetLength(0));
            for (int y = 0; y < data.GetLength(0); y++)
                for (int x = 0; x < data.GetLength(1); x++)
                {
                    getPossibleDataAboutPoint(x, y, data,returnTable, blackListPriority);
                }
            return returnTable;
        }
    }
}
