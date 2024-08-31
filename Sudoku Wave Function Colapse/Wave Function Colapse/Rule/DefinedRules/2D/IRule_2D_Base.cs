using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D
{
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
        public PossibleValuesMap getPossibleDataAboutPoint(Point p, int[,] data, PossibleValuesMap currentValues);
        public PossibleValuesMap getPossibleDataAboutPoint(int x, int y, int[,] data, PossibleValuesMap currentValues) { return getPossibleDataAboutPoint(new Point(x, y), data, currentValues); }
        //Fixxed issue here,
        //The issue was the new PossibleValuesMap had the indexes flipped
        public PossibleValuesMap getPossibleDataAboutPoint(Point p, int[,] data)
        {
            PossibleValuesMap map = new PossibleValuesMap(data.GetLength(1), data.GetLength(0));
            return getPossibleDataAboutPoint(p, data, map);
        }
        public PossibleValuesMap getPossibleDataAboutPoint(int x, int y, int[,] data)
        {
            PossibleValuesMap map = new PossibleValuesMap(data.GetLength(1), data.GetLength(0));
            return getPossibleDataAboutPoint(x, y, data, map);
        }
        public PossibleValuesMap getFullTablePossibleData(int[,] data)
        {
            PossibleValuesMap returnTable = new PossibleValuesMap(data.GetLength(1), data.GetLength(0));
            for (int y = 0; y < data.GetLength(0); y++)
                for (int x = 0; x < data.GetLength(1); x++)
                {
                    returnTable-=getPossibleDataAboutPoint(x, y, data);
                }
            return returnTable;
        }
    }
}
