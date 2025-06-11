using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules
{
    /*Rule_Dictionary
     * Functions as a Dictionary but in rule form
     * 
     */
    internal class Rule_Dictionary : IRuleBase
    {
        #region Interface Functions
        public bool evaluate(int target)
        {
            return ruleLedger[target].evaluate(target);
        }
        public WFC_MUX_Hash evaluateReturnPossibleHash(int target)
        {
            return ruleLedger[target].evaluateReturnPossibleHash(target);
        }

        public WFC_MUX_Hash evaluateReturnNegativeHash(int target)
        {
            return ruleLedger[target].evaluateReturnNegativeHash(target);
        }
        Rule_Filter IRuleBase.evaluateReturnRuleFilter(int target)
        {
            return ruleLedger[target].evaluateReturnRuleFilter(target);
        }

        public void reset() { }
        #endregion

        #region Local Variables
        Dictionary<int, IRuleBase> ruleLedger;
        #endregion
        #region Constructors
        public Rule_Dictionary()
        {
            ruleLedger = new Dictionary<int, IRuleBase>();
        }
        public Rule_Dictionary(List<int> IDs, List<IRuleBase> Rules) 
        {
            //Assumes IDs and Rules share same length
            //Assumes each ID is unique. Otherwise use a rule set
            ruleLedger = new Dictionary<int, IRuleBase>();
            ruleLedger = IDs.Zip(Rules).ToDictionary();
        }
        #endregion
    }
}
