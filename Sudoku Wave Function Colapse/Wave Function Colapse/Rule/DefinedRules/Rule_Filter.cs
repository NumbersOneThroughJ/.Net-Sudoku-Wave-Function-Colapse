using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm;
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
    internal class Rule_Filter : IRuleBase, ICloneable
    {



        #region Interface Functions
        //As long as the values are not blacklisted, will return true
        public bool evaluate(int target)
        {
            return isAllowed(target);
        }

        //target is not necessary for this function
        //Generally pass 0 to this function
        //Hash of possible values
        public WFC_MUX_Hash evaluateReturnPossibleHash(int target)
        {
            return softWhiteList;
        }
        public WFC_MUX_Hash evaluateReturnNegativeHash(int target)
        {
            return hardBlackList;
        }

        public void reset() { }
        #endregion

        #region Local variables
        private WFC_MUX_Hash softWhiteList;
        private WFC_MUX_Hash hardBlackList;
        public static WFC_MUX_Hash emptyHash;
        #endregion

        #region Constructors
        public Rule_Filter()
        {
            softWhiteList = new WFC_MUX_Hash(~emptyHash);
            hardBlackList = new WFC_MUX_Hash(emptyHash);
        }
        public Rule_Filter(WFC_MUX_Hash softWhiteList, WFC_MUX_Hash hardBlackList)
        {
            this.softWhiteList = softWhiteList;
            this.hardBlackList = hardBlackList;
        }
        public Rule_Filter(WFC_MUX_Hash softWhiteList, WFC_MUX_Hash hardBlackList, WFC_MUX_Hash emptyHash)
        {
            this.softWhiteList = softWhiteList;
            this.hardBlackList = hardBlackList;
            Rule_Filter.emptyHash = emptyHash;
        }
        public Rule_Filter(Rule_Filter filter)
        {
            if (filter == null)
            {
                softWhiteList = new WFC_MUX_Hash(~emptyHash);
                hardBlackList = new WFC_MUX_Hash(emptyHash);
                return;
            }
            this.softWhiteList = filter.softWhiteList;
            this.hardBlackList = filter.hardBlackList;
        }
        #endregion

        private bool isAllowed(int index)
        {
            return hardBlackList.getHash(index) == 1;
        }

        Rule_Filter IRuleBase.evaluateReturnRuleFilter(int target)
        {
            return this;
        }


        //These should be used over the static operators. They will be faster.
        /*Combines all lists within the two rule_Filters
         * Adds all whitelisted items, then the blacklist takes the precident over the whitelist
         * AND
         */
        public void And(Rule_Filter rule2)
        {
            if (rule2 == null) return;
            softWhiteList &= rule2.softWhiteList;
            hardBlackList &= rule2.hardBlackList;
            softWhiteList &= ~hardBlackList;
        }
        public void  Deny(WFC_MUX_Hash blacklistHash)
        {
            hardBlackList &= blacklistHash;
            softWhiteList &= ~hardBlackList;
        }
        /* Combines all lists within the two rule_Filters
         *  Adds all blacklisted items, then the whiteListed items. WhiteList takes precident over blacklist
         *  OR
         */
        public void  Or(Rule_Filter rule2)
        {
            if (rule2 == null) return;
            softWhiteList &= rule2.softWhiteList;
            hardBlackList &= rule2.hardBlackList;
            hardBlackList &= ~softWhiteList;
        }
        public void  Allow(WFC_MUX_Hash whiteListHash)
        {
            whiteListHash &= whiteListHash;
            hardBlackList &= ~softWhiteList;
        }

        public object Clone()
        {
            return new Rule_Filter(this);
        }

        //quick functions

        /*Combines all lists within the two rule_Filters
         * Adds all whitelisted items, then the blacklist takes the precident over the whitelist
         * AND
         */
        public static Rule_Filter operator & (Rule_Filter rule1, Rule_Filter rule2)
        {
            if ((rule2 == null) && (rule1 == null)) return new Rule_Filter();
            if (rule2 == null) return new Rule_Filter(rule1);
            if (rule1 == null) return new Rule_Filter(rule2);
            Rule_Filter returnRule = new Rule_Filter(rule1.softWhiteList & rule2.softWhiteList,rule1.hardBlackList & rule2.hardBlackList);
            returnRule.softWhiteList &= ~returnRule.hardBlackList;
            return returnRule;
        }
        public static Rule_Filter operator -(Rule_Filter rule1, WFC_MUX_Hash blacklistHash)
        {
            Rule_Filter retrule = new Rule_Filter(rule1);
            retrule.hardBlackList &= blacklistHash;
            retrule.softWhiteList &= ~retrule.hardBlackList;
            return retrule;
        }
        /* Combines all lists within the two rule_Filters
         *  Adds all blacklisted items, then the whiteListed items. WhiteList takes precident over blacklist
         *  OR
         */
        public static Rule_Filter operator |(Rule_Filter rule1, Rule_Filter rule2)
        {
            if ((rule2 == null) && (rule1 == null)) return new Rule_Filter();
            if (rule2 == null) return new Rule_Filter(rule1);
            if (rule1 == null) return new Rule_Filter(rule2);
            Rule_Filter returnRule = new Rule_Filter(rule1.softWhiteList & rule2.softWhiteList, rule1.hardBlackList & rule2.hardBlackList);
            returnRule.hardBlackList &= ~returnRule.softWhiteList;
            return returnRule;
        }
        public static Rule_Filter operator + (Rule_Filter rule1, WFC_MUX_Hash whiteListHash)
        {
            Rule_Filter retrule = new Rule_Filter(rule1);
            retrule.softWhiteList &= whiteListHash;
            retrule.hardBlackList &= ~retrule.softWhiteList;
            return retrule;
        }
    }
}
