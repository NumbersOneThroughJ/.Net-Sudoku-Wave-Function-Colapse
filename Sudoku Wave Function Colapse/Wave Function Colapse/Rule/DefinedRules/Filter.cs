using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules
{
    /// <summary>
    /// This class will use Bytes to track a filter of integers
    /// It will use & bitwise logic for faster value comparison
    /// That said, it will also use one bit to represent 1 value
    /// e.g for values -1 to 9, you will need 11 bits
    /// </summary>
    internal class Filter
    {
        Byte[] Vals;
        List<int> possVals;

        public Filter(int numPossVals)
        {
            Vals = new byte[NumBytes(numPossVals)];
        }

        public Filter()

        public static int NumBytes(int numPossVals)
        {
            return (numPossVals/8) + (((numPossVals%8)>0)?1:0);
        }
    }
}
