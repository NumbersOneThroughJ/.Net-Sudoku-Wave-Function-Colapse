using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D
{
    internal class Rule_2D_Set : IRule_2D_Base
    {

        //Local Values
        bool andGate;//Determines wether to And or Or the entire Set
        List<IRule_2D_Base> rules;

        //Constructors
        public Rule_2D_Set(List<IRule_2D_Base> listOfRules, bool doAnd)
        {
            rules = listOfRules;
            andGate = doAnd;
        }

        //Interface Functions
        public bool evaluatePoint(Point p, int[,] data)
        {
            if(andGate)
            {
                foreach(IRule_2D_Base rule in rules)
                {
                    if (!rule.evaluatePoint(p, data)) return false;
                }
                return true;
            } else
            {
                foreach(IRule_2D_Base rule in rules)
                {
                    if(rule.evaluatePoint(p, data)) return true;
                }
                return false;
            }
        }
        public PossibleValuesMap getPossibleDataAboutPoint(Point p, int[,] data, PossibleValuesMap currentValues)
        {
            if (andGate)
            {
                foreach (IRule_2D_Base rule in rules)
                {
                    currentValues-=
                        rule.getPossibleDataAboutPoint(p, data);
                }
            } else
            {
                foreach (IRule_2D_Base rule in rules)
                {
                    currentValues +=
                        rule.getPossibleDataAboutPoint(p, data);
                }
            }
            return currentValues;
        }
    }
}
