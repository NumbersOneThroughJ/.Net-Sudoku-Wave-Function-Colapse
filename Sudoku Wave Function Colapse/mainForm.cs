using Sudoku_Wave_Function_Colapse.Classes;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D;
using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm;

namespace Sudoku_Wave_Function_Colapse
{
    public partial class mainForm : Form
    {
        private IRule_2D_Base collumDivider;
        private IRule_2D_Base SudokuSingleSquareRule;
        private IRule_2D_Base SudokuVerticalRule;
        private IRule_2D_Base SudokuHorizontalRule;

        private IRule_2D_Base fullRuleSet;
        WFC_Manager WaveFunctionCollapse;

        static int[] finalValues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private int[,] defaults;

        public mainForm()
        {
            InitializeComponent();

            collumDivider = new Rule_2D_AxisDivider(3, true, Sudoku_Rules.makeDictionaryOfSquareMasks());
            SudokuSingleSquareRule = new Rule_2D_AxisDivider(3, false, (IRule_2D_Base)collumDivider);
            SudokuVerticalRule = Sudoku_Rules.makeVerticalDictionaryOfMasks();
            SudokuHorizontalRule = Sudoku_Rules.makeHorizontalDictionaryOfMasks();
            fullRuleSet = new Rule_2D_Set(new List<IRule_2D_Base> { SudokuSingleSquareRule, SudokuVerticalRule, SudokuHorizontalRule }, true);

            WaveFunctionCollapse = new WFC_Manager(getPossibles, finalValues);

            sudoku9x91.reset();
            //Hardest sudoku Puzzle
            /*
            int[,] board =
            {
                { 8, -1, -1, -1, -1, -1, -1, -1, -1 },
                { -1, -1, 3, 6, -1, -1, -1, -1, -1 },
                { -1, 7, -1, -1, 9, -1, 2, -1, -1 },
                { -1, 5, -1, -1, -1, 7, -1, -1, -1 },
                { -1, -1, -1, -1, 4, 5, 7, -1, -1 },
                { -1, -1, -1, 1, -1, -1, -1, 3, -1 },
                { -1, -1, 1, -1, -1, -1, -1, 6, 8 },
                { -1, -1, 8, 5, -1, -1, -1, 1, -1 },
                { -1, 9, -1, -1, -1, -1, 4, -1, -1 },
            };
            sudoku9x91.setBoard(board
            */

            /*
            int[,] board =
            {
                {-1, -1, -1, 8, -1, -1, -1, 6, -1 },
                {9, 6, -1, -1, 2, -1, -1, -1, -1 },
                {-1, 5, 2, -1, -1, -1, 3, -1, 9 },
                {1, 9, -1, -1, 7, 2, 4, 3, 8 },
                {-1, -1, 3, -1, 8, 1, -1, -1, -1 },
                {5, 7, 8, -1, 4, 6, 9, 2, -1 },
                {-1, -1, -1, 7, 3, -1, 1, 9, -1 },
                {-1, -1, 5, -1, 9, -1, -1, -1, -1 },
                {7, -1, -1, 2, -1, -1, 8, -1, 6 },
            };
            sudoku9x91.setBoard(board);
            */
            defaults = sudoku9x91.getTableAlt();
        }

        private void highlightMinimums(int[,][] vals, int[,] currentVals)
        {
            List<Point> mins = WFC_MinFinder_Arr.findMinimums(vals, currentVals, finalValues);
            sudoku9x91.highlightSquares(mins);
        }

        private int[,][] getPossibles(int[,] map)
        {
            return fullRuleSet.getFullTablePossibleData(map).getPossibleValuesAs2DArrOfArrs();
        }

        public void InvokeUpdate(object sender, bool e)
        {
            int[,][] vals = getPossibles(sudoku9x91.getTableAlt());
            sudoku9x91.setAvailables(vals);
            highlightMinimums(vals, sudoku9x91.getTableAlt());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            defaults = sudoku9x91.getTableAlt();
            Start_Time_TxtBox.Text = System.DateTime.Now.ToString();
            int[,] mapValues = sudoku9x91.getTableAlt();
            mapValues = WaveFunctionCollapse.CollapseFullMap(mapValues);
            sudoku9x91.setBoard(mapValues);
            End_Time_TxtBox.Text = System.DateTime.Now.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sudoku9x91.setBoard(defaults);
        }
    }
}
