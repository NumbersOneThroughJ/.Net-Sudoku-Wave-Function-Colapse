using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D
{
    internal class Rule_2D_SingularRule : ARule_2D_Base
    {
        //Local Variables
        IRuleBase rule;

        //Constructors
        public Rule_2D_SingularRule(IRuleBase rule, bool andMode)
            : base(andMode)
        {
            this.rule = rule;
        }

        //Interface Functions
        public override bool evaluatePoint(Point p, int[,] data)
        {
            return rule.evaluate(data[p.Y, p.X]);
        }

        public override void ApplyPossibleDataAboutPoint(Point p, int[,] data, PossibleValuesMap currentValues)
        {
            if (andMode) { currentValues.and(p, rule.evaluateReturnRuleFilter(data[p.Y, p.X])); }
            else currentValues.or(p, rule.evaluateReturnRuleFilter(data[p.Y, p.X]));

        }
    }
}
