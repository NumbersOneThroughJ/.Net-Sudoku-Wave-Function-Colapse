using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D
{
    /// <summary>
    /// PLAN TO REPLACE THIS
    /// Will need to use hashes instead of using lists of ints
    /// </summary>

    internal class PossibleValuesMap : ICloneable
    {
        //Local Variables
        public Rule_Filter[,] map;

        #region Constructors
        public PossibleValuesMap(int x, int y, Boolean init = true) 
        {
            map = new Rule_Filter[y, x];
            if (init)
            {
                for (int x1 = 0; x1 < x; x1++)
                    for (int y1 = 0; y1 < y; y1++)
                    {
                        map[y1, x1] = new Rule_Filter();
                    }
            }
        }

        public PossibleValuesMap(PossibleValuesMap m)
        {
            map = new Rule_Filter[m.map.GetLength(0), m.map.GetLength(1)];
            for (int x1 = 0; x1 < m.map.GetLength(1); x1++)
                for (int y1 = 0; y1 < m.map.GetLength(0); y1++)
                {
                    map[y1, x1] = new Rule_Filter(m.map[y1, x1]);
                }
        }

        protected PossibleValuesMap()
        {
            map = null;
        }
        #endregion

        #region Public Functions
        public virtual bool checkBounds(int x, int y)
        {
            return (x>0) && (y>0) && (x<map.GetLength(1)) && (y<map.GetLength(0));
        }
        #region getRuleFilter
        public Rule_Filter getValuesAsRule(int x, int y)
        {
            return map[y,x];
        }
        public Rule_Filter getValuesAsRule(Point point) { return getValuesAsRule(point.X, point.Y); }
        #endregion
        #region getHashes
        public WFC_MUX_Hash getPossibleHashForPoint(int x, int y) { return getValuesAsRule(x,y).evaluateReturnPossibleHash(0); }
        //public List<int>[,] getPossibleValuesAs2DArrofLists()
        //{
        //    List<int>[,] returnArr = new List<int>[map.GetLength(0), map.GetLength(1)];
        //    for(int y = 0; y<map.GetLength(0); y++)
        //        for(int x = 0; x<map.GetLength(1); x++)
        //        {
        //            returnArr[y, x] = getPossibleValuesForPoint(x, y);
        //        }
        //    return returnArr;
        //}
        public WFC_MUX_Hash[,] getPossibleValuesAs2DArrOfHash()
        {
            WFC_MUX_Hash[,] returnArr = new WFC_MUX_Hash[map.GetLength(0),map.GetLength(1)];
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    returnArr[y, x] = getPossibleHashForPoint(x, y);
                }
            }
            return returnArr;
        }
        //public int[][][] getPossibleValuesAsArrOfArrOfArrs()
        //{
        //    int[][][] returnArr = new int[map.GetLength(0)][][];
        //    for (int y = 0; y < map.GetLength(0); y++)
        //    {
        //        returnArr[y] = new int[map.GetLength(1)][];
        //        for (int x = 0; x < map.GetLength(1); x++)
        //        {
        //            returnArr[y][x] = getPossibleValuesForPoint(x, y).ToArray();
        //        }
        //    }
        //    return returnArr;
        //}
        #endregion
        #region Logical And
        //Combines with BlackList Priority
        //public void and(Point p, Rule_Filter r) { map[p.Y,p.X].And(r); }
        public void and(int x, int y, Rule_Filter r) { map[y, x].And(r); }
        //public void and(Point p, WFC_MUX_Hash negatives) { map[p.Y, p.X].Deny(negatives); }
        public void and(int x, int y, WFC_MUX_Hash negatives) { map[y,x].Deny(negatives); }
        public void and(WFC_MUX_Hash[,] negatives)
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
        #endregion
        #region logical Or
        //Combines with WhiteList Priority
        //public void or(Point p, Rule_Filter r) { map[p.Y, p.X].Or(r); }
        public void or(int x, int y, Rule_Filter r) { map[y, x].Or(r); }
        //public void or(Point p, WFC_MUX_Hash positives) { map[p.Y, p.X].Allow(positives); }
        public void or(int x, int y, WFC_MUX_Hash positives) { map[y, x].Allow(positives); }
        public void or(WFC_MUX_Hash[,] positives)
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
        public void or(PossibleValuesMap otherMap) { or(otherMap.map); }
        #endregion
        public object Clone()
        {
            Rule_Filter[,] r = new Rule_Filter[map.GetLength(0), map.GetLength(1)];
            for (int y = 0; y < map.GetLength(1); y++)
                for (int x = 0; x<map.GetLength(0); x++)
                {
                    r[y,x] = (Rule_Filter)map[y,x].Clone();
                }
            return r;
        }
        #endregion
        #region operators
        //Assumes two maps are of same length
        public static PossibleValuesMap operator |(PossibleValuesMap map1,  PossibleValuesMap map2)
        {
            PossibleValuesMap result = new PossibleValuesMap(map1.map.GetLength(1), map1.map.GetLength(0));

            result.and(map1.map);
            result.and(map2.map);

            return result;
        }
        public static PossibleValuesMap operator &(PossibleValuesMap map1, PossibleValuesMap map2)
        {
            PossibleValuesMap result = new PossibleValuesMap(map1.map.GetLength(1), map1.map.GetLength(0));

            result.or(map1.map);
            result.or(map2.map);

            return result;
        }
        #endregion
    }

    /// <summary>
    /// Defines a subsection of the possibleValuesMap
    /// The start and end are inclusive.
    /// </summary>
    internal class PossibleValuesMap_SubSection : PossibleValuesMap
    {
        #region variables
        #region private
        private int 
            colStart, colEnd, numCol,
            rowStart, rowEnd, numRow,
            subSectionXCount, subSectionYCount,
            offsetX, offsetY,
            absoluteOffsetX, absoluteOffsetY,
            maxCol, maxRow;
        #endregion
        #region readonly Access Variables
        public int MAX_X => subSectionXCount;
        public int MAX_Y => subSectionYCount;
        #endregion
        #endregion

        #region Contstructors
        public PossibleValuesMap_SubSection(PossibleValuesMap original, int numCol, int numRow)//, int offsetX, int offsetY)
        {
            map = original.map;
            this.maxCol = map.GetLength(1);
            this.maxRow = map.GetLength(0);
            this.numCol = (numCol == -1) ? map.GetLength(1) : Math.Clamp(numCol, 0, maxCol);
            this.numRow = (numRow == -1) ? map.GetLength(0) : Math.Clamp(numRow, 0, maxRow);
            //This is for the offset, it is not implemented yet
            //this.offsetX = Math.Clamp(offsetX, -numCol, numCol);
            //this.offsetY = Math.Clamp(offsetY, -numRow, numRow);
            subSectionXCount = (int)Math.Ceiling((double)(map.GetLength(1)/* - this.offsetX*/) / (double)this.numCol);
            subSectionYCount = (int)Math.Ceiling((double)(map.GetLength(0)/* - this.offsetY*/) / (double)this.numRow);
            setSubsectionIndex(0, 0);
        }
        /// <summary>
        /// Creates a subsection within bounds of another subsection
        /// Will need offset (Fffffffffffffffffffffk)
        /// Will need absoulute offset and a max collumn variable
        /// Will also need to think about reusing these objects instead of creating new rules...
        /// </summary>
        /// <param name="macroSection"></param>
        /// <param name="numCol"></param>
        /// <param name="numRow"></param>
        public PossibleValuesMap_SubSection(PossibleValuesMap_SubSection macroSection, int numCol, int numRow)
        {
            map = macroSection.map;
            this.numCol = (numCol == -1) ? macroSection.numCol : Math.Clamp(numCol, 0, macroSection.numCol);
            this.numRow = (numRow == -1) ? macroSection.numRow : Math.Clamp(numRow, 0, macroSection.numRow);
            //This is for the offset, it is not implemented yet
            //this.offsetX = Math.Clamp(offsetX, -numCol, numCol);
            //this.offsetY = Math.Clamp(offsetY, -numRow, numRow);
            subSectionXCount = (int)Math.Ceiling((double)(macroSection.numCol/* - this.offsetX*/) / (double)numCol);
            subSectionYCount = (int)Math.Ceiling((double)(macroSection.numRow/* - this.offsetY*/) / (double)numRow);
            setSubsectionIndex(0, 0);
        }
        #endregion

        #region public functions
        /// <summary>
        /// Returns true if the selected location is within bounds
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override bool checkBounds(int x, int y)
        {
            return !((x>colEnd)|(x<colStart)|(y>rowEnd)|(y<rowStart));
        }

        public void setSubsectionIndex(int subsectionX, int subsectionY)
        {
            //Need bounds checking for subsection...
            //for now it will just rewrap
            subsectionX %= subSectionXCount;
            subsectionY %= subSectionYCount;
            colStart = 0;
            colEnd = Math.Clamp(numCol * subsectionX,0, map.GetLength(1));
            rowStart = 0;
            rowEnd = Math.Clamp(numRow * subsectionY, 0, map.GetLength(0));
        }
        #endregion
    }
}
