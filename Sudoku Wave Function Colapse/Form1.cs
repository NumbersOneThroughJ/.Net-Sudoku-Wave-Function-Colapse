using Sudoku_Wave_Function_Colapse.Classes;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm;

namespace Sudoku_Wave_Function_Colapse
{
    public partial class Form1 : Form
    {
        private IRule_2D_Base collumDivider;
        private IRule_2D_Base SudokuSingleSquareRule;
        private IRule_2D_Base SudokuVerticalRule;
        private IRule_2D_Base SudokuHorizontalRule;

        private IRule_2D_Base fullRuleSet;

        static int[] finalValues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public Form1()
        {
            collumDivider = new Rule_2D_AxisDivider(3, true, Sudoku_Rules.makeDictionaryOfSquareMasks());
            SudokuSingleSquareRule = new Rule_2D_AxisDivider(3, false, (IRule_2D_Base)collumDivider);
            SudokuVerticalRule = Sudoku_Rules.makeVerticalDictionaryOfMasks();
            SudokuHorizontalRule = Sudoku_Rules.makeHorizontalDictionaryOfMasks();

            fullRuleSet = new Rule_2D_Set(new List<IRule_2D_Base> { SudokuSingleSquareRule, SudokuVerticalRule, SudokuHorizontalRule }, true);

            InitializeComponent();
            sudoku9x91.reset();

            sudoku9x91.setAvailables(fullRuleSet.getFullTablePossibleData(sudoku9x91.getTableAlt()).getPossibleValuesAs2DArrOfArrs());
        }

        private void highlightMinimums(int[][][] vals, int[,] currentVals)
        {
            List<Point> mins = WFC_MinFinder_Arr.findMinimums(vals, currentVals, finalValues);
            sudoku9x91.highlightSquares(mins);
        }

        private String getLabelText(int[][][] vals, int[,] currentVals)
        {
            String returnString = "";
            List<Point> mins = WFC_MinFinder_Arr.findMinimums(vals, currentVals, finalValues);
            foreach(Point p in mins)
            {
                returnString += p.X + " " + p.Y + " length: " + vals[p.Y][p.X].Length + "\n";
            }
            return returnString;
        }

        public void InvokeUpdate(object sender, bool e)
        {
            int[][][] vals = fullRuleSet.getFullTablePossibleData(sudoku9x91.getTableAlt()).getPossibleValuesAs2DArrOfArrs();
            sudoku9x91.setAvailables(vals);
            label1.Text = getLabelText(vals, sudoku9x91.getTableAlt());
            highlightMinimums(vals, sudoku9x91.getTableAlt());
        }
    }
}
