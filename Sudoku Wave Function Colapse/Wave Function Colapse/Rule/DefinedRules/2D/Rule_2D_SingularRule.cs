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
        public override bool evaluatePoint(int x, int y, int[,] data)
        {
            return rule.evaluate(data[y,x]);
        }

        public override void ApplyPossibleDataAboutPoint(int x, int y, int[,] data)
        {
            if (andMode) { loadedMapOfCurrentValues.and(x, y, rule.evaluateReturnRuleFilter(data[y,x])); }
            else loadedMapOfCurrentValues.or(x, y, rule.evaluateReturnRuleFilter(data[y, x]));

        }
    }
}
