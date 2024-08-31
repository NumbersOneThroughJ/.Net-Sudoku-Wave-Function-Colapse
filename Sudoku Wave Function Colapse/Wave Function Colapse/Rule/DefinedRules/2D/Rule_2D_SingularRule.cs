using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D
{
    internal class Rule_2D_SingularRule : IRule_2D_Base
    {
        //Local Variables
        IRuleBase rule;

        //Constructors
        public Rule_2D_SingularRule(IRuleBase rule)
        {
            this.rule = rule;
        }

        //Interface Functions
        public bool evaluatePoint(Point p, int[,] data)
        {
            return rule.evaluate(data[p.Y, p.X]);
        }

        public PossibleValuesMap getPossibleDataAboutPoint(Point p, int[,] data, PossibleValuesMap currentValues)
        {

            currentValues.or(p, rule.evaluateReturnRuleFilter(data[p.Y, p.X]));

            return currentValues;
        }
    }
}
