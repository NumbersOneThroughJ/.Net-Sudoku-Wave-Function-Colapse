using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D
{
    internal class PossibleValuesMap
    {
        //Local Variables
        public Rule_Filter[,] map;

        //Constructors
        public PossibleValuesMap(int x, int y) 
        {
            map = new Rule_Filter[y, x];
        }

        //Public Functions
        public Rule_Filter getValuesAsRule(int x, int y)
        {
            return map[y,x];
        }
        public Rule_Filter getValuesAsRule(Point point) { return getValuesAsRule(point.X, point.Y); }
        public List<int> getPossibleValuesForPoint(int x, int y) { return getValuesAsRule(x,y).evaluateReturnPossibleValues(0); }
        public List<int>[,] getPossibleValuesAs2DArrofLists()
        {
            List<int>[,] returnArr = new List<int>[map.GetLength(0), map.GetLength(1)];
            for(int y = 0; y<map.GetLength(0); y++)
                for(int x = 0; x<map.GetLength(1); x++)
                {
                    returnArr[y, x] = getPossibleValuesForPoint(x, y);
                }
            return returnArr;
        }
        public int[,][] getPossibleValuesAs2DArrOfArrs()
        {
            int[,][] returnArr = new int[map.GetLength(0),map.GetLength(1)][];
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    returnArr[y, x] = getPossibleValuesForPoint(x, y).ToArray();
                }
            }
            return returnArr;
        }
        public int[][][] getPossibleValuesAsArrOfArrOfArrs()
        {
            int[][][] returnArr = new int[map.GetLength(0)][][];
            for (int y = 0; y < map.GetLength(0); y++)
            {
                returnArr[y] = new int[map.GetLength(1)][];
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    returnArr[y][x] = getPossibleValuesForPoint(x, y).ToArray();
                }
            }
            return returnArr;
        }

        //Combines with BlackList Priority
        public void and(Point p, Rule_Filter r) { map[p.Y,p.X] -= r; }
        public void and(int x, int y, Rule_Filter r) { map[y,x] -= r; }
        public void and(Point p, List<int> negatives) { map[p.Y, p.X] -= negatives; }
        public void and(int x, int y, List<int> negatives) { map[y,x] -= negatives; }
        public void and(List<int>[,] negatives)
        {
            for (int y = 0; y<negatives.GetLength(0); y++)
                for(int x = 0; x<negatives.GetLength(1); x++)
                {
                    and(x, y, negatives[y,x]);
                }
        }
        public void and(Rule_Filter[,] rules)
        {
            for (int y = 0; y < rules.GetLength(0); y++)
                for (int x = 0; x < rules.GetLength(1); x++)
                {
                    and(x, y, rules[y, x]);
                }
        }
        public void and(PossibleValuesMap otherMap) { and(otherMap.map); }

        //Combines with WhiteList Priority
        public void or(Point p, Rule_Filter r) { map[p.Y, p.X] += r; }
        public void or(int x, int y, Rule_Filter r) { map[y,x] += r; }
        public void or(Point p, List<int> positives) { map[p.Y, p.X] += positives; }
        public void or(int x, int y, List<int> positives) { map[y, x] += positives; }
        public void or(List<int>[,] positives)
        {
            for (int y = 0; y < positives.GetLength(0); y++)
                for (int x = 0; x < positives.GetLength(1); x++)
                {
                    or(x, y, positives[y, x]);
                }
        }
        public void or(Rule_Filter[,] rules)
        {
            for (int y = 0; y < rules.GetLength(0); y++)
                for (int x = 0; x < rules.GetLength(1); x++)
                {
                    or(x, y, rules[y, x]);
                }
        }

        //Operators
        //Assumes two maps are of same length
        public static PossibleValuesMap operator -(PossibleValuesMap map1,  PossibleValuesMap map2)
        {
            PossibleValuesMap result = new PossibleValuesMap(map1.map.GetLength(1), map1.map.GetLength(0));

            result.and(map1.map);
            result.and(map2.map);

            return result;
        }
        public static PossibleValuesMap operator +(PossibleValuesMap map1, PossibleValuesMap map2)
        {
            PossibleValuesMap result = new PossibleValuesMap(map1.map.GetLength(1), map1.map.GetLength(0));

            result.or(map1.map);
            result.or(map2.map);

            return result;
        }
    }
}
