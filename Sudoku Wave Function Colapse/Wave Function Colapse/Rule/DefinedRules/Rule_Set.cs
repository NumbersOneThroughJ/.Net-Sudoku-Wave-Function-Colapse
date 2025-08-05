using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules
{
    internal class Rule_Set : IRuleBase
    {
        public bool evaluate(int target)
        {
            throw new NotImplementedException();
        }

        public WFC_MUX_Hash evaluateReturnNegativeHash(int target, WFC_MUX_Hash retHash = null)
        {
            throw new NotImplementedException();
        }

        public WFC_MUX_Hash evaluateReturnPossibleHash(int target, WFC_MUX_Hash retHash = null)
        {
            throw new NotImplementedException();
        }

        public Rule_Filter evaluateReturnRuleFilter(int target, Rule_Filter retRule = null)
        {
            throw new NotImplementedException();
        }

        public void reset()
        {
            throw new NotImplementedException();
        }
    }
}
