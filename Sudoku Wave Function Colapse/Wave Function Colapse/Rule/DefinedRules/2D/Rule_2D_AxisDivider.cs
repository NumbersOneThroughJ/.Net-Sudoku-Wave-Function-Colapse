using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D
{
    internal class Rule_2D_AxisDivider : IRule_2D_Base
    {

        //Local Variables
        int axisDividerLength;
        int axisIndex;//0 is row, 1 is collum
        IRule_2D_Base rule;

        //Constructors
        public Rule_2D_AxisDivider(int divisionLength, bool divideOnCollum, IRule_2D_Base rule)
        {
            axisDividerLength = divisionLength;
            axisIndex = divideOnCollum ? 1 : 0;
            this.rule = rule;
        }


        //Interface Functions
        public bool evaluatePoint(Point p, int[,] data)
        {
            //[y,x]
            int y = p.Y;
            int x = p.X;
            int subsection = -1;
            switch(axisIndex)
            {
                case 0:
                    subsection = y / axisDividerLength;
                    y = y % axisDividerLength;
                    break;
                case 1:
                    subsection = x / axisDividerLength;
                    x = x % axisDividerLength;
                    break;
            }
            return rule.evaluatePoint(x, y, getSubSection(subsection, data));
        }

        public PossibleValuesMap getPossibleDataAboutPoint(Point p, int[,] data, PossibleValuesMap currentValues)
        {
            //[y,x]
            int y = p.Y;
            int x = p.X;
            int subsection = -1;
            switch (axisIndex)
            {
                case 0:
                    subsection = y / axisDividerLength;
                    y = y % axisDividerLength;
                    break;
                case 1:
                    subsection = x / axisDividerLength;
                    x = x % axisDividerLength;
                    break;
            }
            PossibleValuesMap temp = rule.getPossibleDataAboutPoint(x, y, getSubSection(subsection, data));
            for(int tempY = 0; tempY < temp.map.GetLength(0); tempY++)
                for (int tempX = 0; tempX < temp.map.GetLength(1); tempX++)
                {
                    switch (axisIndex)
                    {
                        case 0:
                            currentValues.or(tempX, (subsection * axisDividerLength) + tempY, temp.getValuesAsRule(tempX,tempY));
                            break;
                        case 1:
                            currentValues.or((subsection * axisDividerLength) + tempX, tempY, temp.getValuesAsRule(tempX, tempY));
                            break;
                    }
                }
            return currentValues;
        }

        //Private Functions
        /* getSubSection
         * Parameters:
         *  subSectionIndex : int - Which subsection you want to get from starting at 0
         *  data : int[,] - to split into smaller subsections
         *  Description:
         *   This function splits the provided data into a smaller array. 
         *   How it splits is determined by the parameters in constructor.
         *   It splits along the x or y axis only, at lengths determined by the axisDividerLength.
         *   Using subsection to determine which section you want. It will
         *   not create overlap.
         *   Finally, if the subsection is at the end of the array and is too small to
         *   provide the requested array length by axisDivider Length, the function
         *   will return a smaller array with the remaining possible Values.
         */
        private int[,] getSubSection(int subSectionIndex, int[,] data)
        {
            int[,] returnArr = new int[0,0];
            int ArrLength = (subSectionIndex+1)*axisDividerLength>data.GetLength(axisIndex)?
                data.GetLength(axisIndex)%axisDividerLength:
                axisDividerLength;
            int dataArrOffset = axisDividerLength * (subSectionIndex);
            switch (axisIndex)
            {
                case 0://Divide on y e.g getLength(1)
                    returnArr = new int[ArrLength, data.GetLength(1)];
                    for(int row = 0; row<ArrLength; row++)
                        for(int col = 0; col<data.GetLength(1); col++)
                        {
                            returnArr[row, col] = data[row +dataArrOffset, col];
                        }
                    break;
                case 1://Divide on x e.g getLength(0)
                    returnArr = new int[data.GetLength(0), ArrLength];
                    for(int row = 0; row<data.GetLength(0); row++)
                        for(int col = 0; col<ArrLength; col++)
                        {
                            returnArr[row, col] = data[row, col +dataArrOffset];
                        }
                    break;
            }
            return returnArr;
        }
    }
}
