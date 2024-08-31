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
        //Interface Functions
        public bool evaluate(int target)
        {
            return ruleLedger[target].evaluate(target);
        }

        public List<int> evaluateReturnPossibleValues(int target)
        {
            return ruleLedger[target].evaluateReturnPossibleValues(target);
        }

        public List<int> evaluateReturnNegativeValues(int target)
        {
            return ruleLedger[target].evaluateReturnNegativeValues(target);
        }
        Rule_Filter IRuleBase.evaluateReturnRuleFilter(int target)
        {
            return ruleLedger[target].evaluateReturnRuleFilter(target);
        }

        public void reset() { }


        //Local Variables
        Dictionary<int, IRuleBase> ruleLedger;

        //Constructors
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
    }
}
