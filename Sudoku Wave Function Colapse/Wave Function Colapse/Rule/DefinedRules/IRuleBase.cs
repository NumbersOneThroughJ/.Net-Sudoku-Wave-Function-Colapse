using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule
{
    internal interface IRuleBase
    {
        public void reset();
        public bool evaluate(int target);
        public List<int> evaluateReturnPossibleValues(int target);
        public List<int> evaluateReturnNegativeValues(int target);
        public Rule_Filter evaluateReturnRuleFilter(int target);

    }
}
