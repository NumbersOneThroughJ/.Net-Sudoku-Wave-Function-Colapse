using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules
{
    /*Rule_Filter
     * Backbone class of the entire WFC algorithm
     * inthis class basically serves as to given a value, see if that value is allowed or not.
     * inthe Filter functions on the idea of a soft suggestive white list and a hard deny blacklist.
     * When given a value, if that value is not on the deny list, it is allowed.
     * When asking for a potential value, it will return its suggestive whitelist
     */
    internal class Rule_Filter : IRuleBase
    {

        //Interface Methods
        //As long as the values are not blacklisted, will return true
        public bool evaluate(int target)
        {
            return isAllowed(target);
        }

        //target is not necessary for this function
        //Generally pass 0 to this function
        public List<int> evaluateReturnPossibleValues(int target)
        {
            return PossibleValues();
        }
        public List<int> evaluateReturnNegativeValues(int target)
        {
            return NegativeValues();
        }

        public void reset() { }

        //Local variables
        private List<int> softWhiteList;
        private List<int> hardBlackList;

        //Constructors
        public Rule_Filter()
        {
            softWhiteList = new List<int>();
            hardBlackList = new List<int>();
        }
        public Rule_Filter(List<int> softWhiteList, List<int> hardBlackList)
        {
            this.softWhiteList = softWhiteList;
            this.hardBlackList = hardBlackList;
        }
        public Rule_Filter(Rule_Filter filter)
        {
            this.softWhiteList = filter.softWhiteList;
            this.hardBlackList = filter.hardBlackList;
        }


        //Functions
        private void allow(int item)
        {
            if(hardBlackList.Contains(item))
                hardBlackList.Remove(item);
            if(!softWhiteList.Contains(item))
                softWhiteList.Add(item);
        }
        private void deny(int item)
        {
            if(softWhiteList.Contains(item))
                softWhiteList.Remove(item);
            if(!hardBlackList.Contains(item))
                hardBlackList.Add(item);
        }
        private bool isAllowed(int item)
        {
            return !hardBlackList.Contains(item);
        }
        private List<int> PossibleValues()
        {
            return new List<int>(softWhiteList);
        }
        private List<int> NegativeValues()
        {
            return new List<int>(hardBlackList);
        }

        Rule_Filter IRuleBase.evaluateReturnRuleFilter(int target)
        {
            return this;
        }

        //quick functions

        /*Combines all lists within the two rule_Filters
         * Adds all whitelisted items, then the blacklist takes the precident over the whitelist
         * AND
         */
        public static Rule_Filter operator - (Rule_Filter rule1, Rule_Filter rule2)
        {
            Rule_Filter returnRule = new Rule_Filter();
            if (rule1 != null)
                foreach (var item in rule1.softWhiteList)
                returnRule.allow(item);
            if (rule2 != null)
                foreach (var item in rule2.softWhiteList)
                returnRule.allow(item);
            if (rule1 != null)
                foreach (var item in rule1.hardBlackList)
                returnRule.deny(item);
            if (rule2 != null)
                foreach (var item in rule2.hardBlackList)
                returnRule.deny(item);
            return returnRule;
        }
        public static Rule_Filter operator -(Rule_Filter rule1, List<int> BlackList)
        {
            return rule1 - new Rule_Filter(new List<int>(), BlackList);
        }
        /* Combines all lists within the two rule_Filters
         *  Adds all blacklisted items, then the whiteListed items. WhiteList takes precident over blacklist
         *  OR
         */
        public static Rule_Filter operator + (Rule_Filter rule1, Rule_Filter rule2)
        {
            Rule_Filter returnRule = new Rule_Filter();
            if (rule1 != null)
                foreach (var item in rule1.hardBlackList)
                returnRule.deny(item);
            if (rule2 != null)
                foreach (var item in rule2.hardBlackList)
                returnRule.deny(item);
            if (rule1 != null)
                foreach (var item in rule1.softWhiteList)
                returnRule.allow(item);
            if (rule2 != null)
                foreach (var item in rule2.softWhiteList)
                returnRule.allow(item);
            return returnRule;
        }
        public static Rule_Filter operator + (Rule_Filter rule1, List<int> WhiteList)
        {
            return rule1 + new Rule_Filter(WhiteList, new List<int>());
        }
    }
}
