using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D
{
    internal class Rule_2D_Dictionary : ARule_2D_Base
    {

        //Variables
        private Dictionary<int, ARule_2D_Base> _ruleDic;

        //Interface Functions
        public override bool evaluatePoint(Point p, int[,] data)
        {
            int setValue = data[p.Y, p.X];
            if (_ruleDic.Keys.Contains(setValue)) {
                return _ruleDic[setValue].evaluatePoint(p, data);
            }
            return true;
        }

        public override void ApplyPossibleDataAboutPoint(Point p, int[,] data, PossibleValuesMap currentValues)
        {
            int setValue = data[p.Y, p.X];
            if (_ruleDic.Keys.Contains(setValue))
            {
                _ruleDic[setValue].ApplyPossibleDataAboutPoint(p, data, currentValues);
            }
        }

        //Constructors
        public Rule_2D_Dictionary()
            : base(false)
        {
            _ruleDic = new Dictionary<int, ARule_2D_Base> ();
        }
        public Rule_2D_Dictionary(Dictionary<int, ARule_2D_Base> ruleDic)
            : base(false)
        {
            _ruleDic = ruleDic;
        }

        //Functions
        public void setRule(int index, ARule_2D_Base rule)
        {
            _ruleDic[index] = rule;
        }
    }
}
