using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D
{

    //[Row, Collum]
    internal class Rule_2D_Mask : IRule_2D_Base
    {
        //Interface Functions
        /*evaluate
         * data is a large board of data, larger than just this mask size or smaller
         * assumes point is within the range of the data (why else evaluate?)
         * returns if that point is valid for this mask
         * mask would be like (a is any) (n is not set yet)
         * 1 1 a
         * 1 2 a
         * 1 a a
         * supplied data would be like
         * 1 3 1 n 2
         * 2 3 1 2 n
         * 1 1 1 3 3
         * 1 2 3 2 3
         * 1 2 3 3 3
         * when asked to evaluate point (3,1) from the top left, it would say this evaluates to true
         * If asked, (1,3) would also evaluate to true as would (0,1)
         * This is an individual mask alone by itself.
         */
        public bool evaluatePoint(Point p, int[,] data)
        {
            Point dataRelativePoint = new Point();
            IRuleBase maskRelativeRule;
            int dataRelativeData;
            //cycles about the mask around the point
            for (int yMaskRel = 0; yMaskRel < ruleMask.GetLength(0); yMaskRel++)
                for (int xMaskRel = 0; xMaskRel < ruleMask.GetLength(1); xMaskRel++)
                {
                    dataRelativePoint.X = xMaskRel + p.X + maskRelativeStart.X;
                    dataRelativePoint.Y = yMaskRel + p.Y + maskRelativeStart.Y;
                    if (inBounds(dataRelativePoint, data))
                    {
                        if (yMaskRel == maskAnchor.Y && xMaskRel == maskAnchor.X) continue;
                        dataRelativeData = data[dataRelativePoint.Y, dataRelativePoint.X];
                        maskRelativeRule = ruleMask[yMaskRel, xMaskRel];
                        if (!maskRelativeRule.evaluate(dataRelativeData)) return false;
                    }
                }
            return true;
        }

        /* getsPossibleData
         * 
         * 
         */
        public PossibleValuesMap getPossibleDataAboutPoint(Point p, int[,] data, PossibleValuesMap currentValues)
        {
            Point dataRelativePoint = new Point();
            IRuleBase maskRelativeRule;
            int dataRelativeData;
            //cycles about the mask around the point
            for (int yMaskRel = 0; yMaskRel < ruleMask.GetLength(0); yMaskRel++)
                for (int xMaskRel = 0; xMaskRel < ruleMask.GetLength(1); xMaskRel++)
                {
                    dataRelativePoint.X = xMaskRel + p.X + maskRelativeStart.X;
                    dataRelativePoint.Y = yMaskRel + p.Y + maskRelativeStart.Y;
                    if (inBounds(dataRelativePoint, data))
                    {
                        dataRelativeData = data[dataRelativePoint.Y, dataRelativePoint.X];
                        maskRelativeRule = ruleMask[yMaskRel, xMaskRel];
                        currentValues.or(dataRelativePoint.X, dataRelativePoint.Y, maskRelativeRule.evaluateReturnRuleFilter(dataRelativeData));
                    }
                }
            return currentValues;
        }

        //Local Variables
        IRuleBase[,] ruleMask;
        Point maskAnchor;
        Point maskRelativeStart, maskRelativeEnd;

        //constructors
        public Rule_2D_Mask()
        {
            ruleMask = new IRuleBase[0, 0];
            maskAnchor = new Point(0, 0);
            maskRelativeStart = new Point(0, 0);
            maskRelativeEnd = new Point(0, 0);
        }

        public Rule_2D_Mask(IRuleBase[,] ruleMask, Point maskAnchor)
        {
            this.ruleMask = ruleMask;
            this.maskAnchor = maskAnchor;
            this.maskRelativeStart = new Point(-maskAnchor.X, -maskAnchor.Y);
            this.maskRelativeEnd = new Point(ruleMask.GetLength(1)+ maskRelativeStart.X, ruleMask.GetLength(0)+maskRelativeStart.Y);
        }



        //Local Functions
        private bool checkPoint(int maskX, int maskY, int data) 
        {
            return ruleMask[maskX, maskY].evaluate(data);
        }
        private bool inBounds(Point p, int[,] data)
        {
            if(p.X<0 || p.Y<0) return false;
            if(p.X>=data.GetLength(1) || p.Y>=data.GetLength(0)) return false;
            return true;
        }
    }
}
