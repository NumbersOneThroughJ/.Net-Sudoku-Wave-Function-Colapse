using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm;
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
        public WFC_MUX_Hash evaluateReturnPossibleHash(int target);
        public WFC_MUX_Hash evaluateReturnNegativeHash(int target);
        public Rule_Filter evaluateReturnRuleFilter(int target);
    }
}
