using Sudoku_Wave_Function_Colapse.Classes;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;

namespace Sudoku_Wave_Function_Colapse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            sudoku9x91.reset();
            IRule_2D_Base collumDivider = new Rule_2D_AxisDivider(3, false, Sudoku_Rules.makeDictionaryOfSquareMasks());
            IRule_2D_Base SudokuSingleSquareRule = new Rule_2D_AxisDivider(3, false, (IRule_2D_Base)collumDivider);
            IRule_2D_Base SudokuVerticalRule = Sudoku_Rules.makeVerticalDictionaryOfMasks();
            IRule_2D_Base SudokuHorizontalRule = Sudoku_Rules.makeHorizontalDictionaryOfMasks();

            IRule_2D_Base fullRuleSet = new Rule_2D_Set(new List<IRule_2D_Base> { SudokuSingleSquareRule, SudokuVerticalRule, SudokuHorizontalRule}, true);

            sudoku9x91.setAvailables(fullRuleSet.getFullTablePossibleData(sudoku9x91.getTableAlt()).getPossibleValuesAs2DArrOfArrs());
        }

        private String getLabelText()
        {
            int[,] map =
            {
                {0,0,0,0,0},
                {0,0,8,0,0},
                {0,4,5,6,0},
                {0,0,7,0,0},
                {0,0,0,0,0},
            };

            IRule_2D_Base yDivider = new Rule_2D_AxisDivider(2, false, Sudoku_Rules.make2DRuleMask(5));
            IRule_2D_Base xDivider = new Rule_2D_AxisDivider(2, true, yDivider);
            String returnString = xDivider.evaluatePoint(2,3, map).ToString();
            /*for(int row = 0; row<output.GetLength(0); row++)
            { 
                for(int col = 0; col<output.GetLength(1); col++)
                {
                    returnString += output[row,col]+" ";
                }
                returnString += "\n";
            }*/
            return returnString;
            //return string.Join(", ", test.evaluate());
            //return test.evaluate(1).ToString();
        }
    }
}
