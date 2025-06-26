using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D
{
    internal class Rule_2D_Set : ARule_2D_Base
    {

        //Local Values
        List<ARule_2D_Base> rules;

        //Constructors
        public Rule_2D_Set(List<ARule_2D_Base> listOfRules, bool andMode)
            : base(andMode)
        {
            rules = listOfRules;
        }

        //Interface Functions
        public bool evaluatePoint(Point p, int[,] data)
        {
            if(andMode)
            {
                foreach(ARule_2D_Base rule in rules)
                {
                    if (!rule.evaluatePoint(p, data)) return false;
                }
                return true;
            } else
            {
                foreach(ARule_2D_Base rule in rules)
                {
                    if(rule.evaluatePoint(p, data)) return true;
                }
                return false;
            }
        }
        public override void ApplyPossibleDataAboutPoint(Point p, int[,] data, PossibleValuesMap currentValues)
        {
            if (andMode)
            {
                foreach (ARule_2D_Base rule in rules)
                {
                    currentValues.and(
                        rule.ApplyPossibleDataAboutPoint(p, data, currentValues));
                }
            } else
            {
                foreach (ARule_2D_Base rule in rules)
                {
                    currentValues.or(
                        rule.ApplyPossibleDataAboutPoint(p, data, currentValues));
                }
            }
        }
    }
}
