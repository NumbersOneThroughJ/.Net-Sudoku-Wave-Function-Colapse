using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Classes
{
    internal class Sudoku_Rules
    {
        private static Dictionary<int, IRuleBase> ruleFilters = new Dictionary<int, IRuleBase>()
        {
            //Entry
            {
                //Group
                1,
                new Rule_Filter(
                    //WhiteList
                    new List<int> {2,3,4,5,6,7,8,9},
                    //BlackList
                    new List<int> {1})
            },
            //Entry
            {

                2,
                new Rule_Filter(
                        //WhiteList
                        new List<int> {1,3,4,5,6,7,8,9},
                        //BlackList
                        new List<int> {2}
                        )
                },
            //Entry
            {
                3,
                new Rule_Filter(
                        //WhiteList
                        new List<int> {1,2,4,5,6,7,8,9},
                        //BlackList
                        new List<int> {3}
                        )
                },
            //Entry
            {
                4,
                new Rule_Filter(
                        //WhiteList
                        new List<int> {1,2,3,5,6,7,8,9},
                        //BlackList
                        new List<int> {4}
                        )
                },
            //Entry
            {
                5,
                new Rule_Filter(
                        //WhiteList
                        new List<int> {1,2,3,4,6,7,8,9},
                        //BlackList
                        new List<int> {5}
                        )
            },
            //Entry
            {
                6,
                new Rule_Filter(
                        //WhiteList
                        new List<int> {1,2,3,4,5,7,8,9},
                        //BlackList
                        new List<int> {6}
                        )
            },
            //Entry
            {
                7,
                new Rule_Filter(
                        //WhiteList
                        new List<int> {1,2,3,4,5,6,8,9},
                        //BlackList
                        new List<int> {7}
                        )
            },
            //Entry
            {
                8,
                new Rule_Filter(
                        //WhiteList
                        new List<int> {1,2,3,4,5,6,7,9},
                        //BlackList
                        new List<int> {8}
                        )
            },
            //Entry
            {
                9,
                new Rule_Filter(
                        //WhiteList
                        new List<int> {1,2,3,4,5,6,7,8},
                        //BlackList
                        new List<int> {9}
                        )
            },
            //Entry
            {
                0,
                new Rule_Filter(
                    //WhiteList
                    new List<int> {1,2,3,4,5,6,7,8,9},
                    //BlackList
                    new List<int> {}
                    )
            }
            ,
            //Entry
            {
                -1,
                new Rule_Filter(
                    //WhiteList
                    new List<int> {1,2,3,4,5,6,7,8,9},
                    //BlackList
                    new List<int> {}
                    )
            }
        };
        
        //Rule Mask Makers
        public static Rule_2D_Mask make2DRuleMask(int id)
        {
            return new Rule_2D_Mask(new IRuleBase[,]{
                { ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id] },
                { ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id] },
                { ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id] },
                { ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id] },
                { ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id] }
            },
                new Point(2, 2));
        }
        public static Rule_2D_Mask makeHorizontalRuleMask(int id)
        {
            return new Rule_2D_Mask(new IRuleBase[,]
            {{
                ruleFilters[id],ruleFilters[id], ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id], ruleFilters[id],ruleFilters[id],ruleFilters[id],
                ruleFilters[id],
                ruleFilters[id],ruleFilters[id], ruleFilters[id],ruleFilters[id],ruleFilters[id],ruleFilters[id], ruleFilters[id],ruleFilters[id],ruleFilters[id],
            }},
                new Point(9,0));
        }
        public static Rule_2D_Mask makeVerticalRuleMask(int id)
        {
            return new Rule_2D_Mask(new IRuleBase[,]
            {
               {ruleFilters[id]},{ruleFilters[id]},{ ruleFilters[id]},{ruleFilters[id]},{ruleFilters[id]},{ruleFilters[id]},{ ruleFilters[id]},{ruleFilters[id]},{ruleFilters[id]},{
                ruleFilters[id]},{
                ruleFilters[id]},{ruleFilters[id]},{ ruleFilters[id]},{ruleFilters[id]},{ruleFilters[id]},{ruleFilters[id]},{ ruleFilters[id]},{ruleFilters[id]},{ruleFilters[id]}
            },
                new Point(0, 9));
        }
        public static IRule_2D_Base makeDictionaryOfSquareMasks()
        {
            Dictionary<int, IRule_2D_Base> dic = new Dictionary<int, IRule_2D_Base>();
            for(int i = -1; i<=9; i++)
            {
                dic[i]=make2DRuleMask(i);
            }
            return new Rule_2D_Dictionary(dic);
        }
        public static IRule_2D_Base makeHorizontalDictionaryOfMasks()
        {
            Dictionary<int, IRule_2D_Base> dic = new Dictionary<int, IRule_2D_Base>();
            for (int i = -1; i <= 9; i++)
            {
                dic[i] = makeHorizontalRuleMask(i);
            }
            return new Rule_2D_Dictionary(dic);
        }
        public static IRule_2D_Base makeVerticalDictionaryOfMasks()
        {
            Dictionary<int, IRule_2D_Base> dic = new Dictionary<int, IRule_2D_Base>();
            for (int i = -1; i <= 9; i++)
            {
                dic[i] = makeVerticalRuleMask(i);
            }
            return new Rule_2D_Dictionary(dic);
        }

    }
}
