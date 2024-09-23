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
        int[][][] vals;
        WFC_Brain brain;
        WFC_CheckPoints checkPoints;


        public Form1()
        {
            collumDivider = new Rule_2D_AxisDivider(3, true, Sudoku_Rules.makeDictionaryOfSquareMasks());
            SudokuSingleSquareRule = new Rule_2D_AxisDivider(3, false, (IRule_2D_Base)collumDivider);
            SudokuVerticalRule = Sudoku_Rules.makeVerticalDictionaryOfMasks();
            SudokuHorizontalRule = Sudoku_Rules.makeHorizontalDictionaryOfMasks();

            fullRuleSet = new Rule_2D_Set(new List<IRule_2D_Base> { SudokuSingleSquareRule, SudokuVerticalRule, SudokuHorizontalRule }, true);
            brain = new WFC_Brain(finalValues);
            InitializeComponent();
            sudoku9x91.reset();
            
            int[][] board =
            {
                new int[] { 8, -1, -1, -1, -1, -1, -1, -1, -1 },
                new int[] { -1, -1, 3, 6, -1, -1, -1, -1, -1 },
                new int[] { -1, 7, -1, -1, 9, -1, 2, -1, -1 },
                new int[] { -1, 5, -1, -1, -1, 7, -1, -1, -1 },
                new int[] { -1, -1, -1, -1, 4, 5, 7, -1, -1 },
                new int[] { -1, -1, -1, 1, -1, -1, -1, 3, -1 },
                new int[] { -1, -1, 1, -1, -1, -1, -1, 6, 8 },
                new int[] { -1, -1, 8, 5, -1, -1, -1, 1, -1 },
                new int[] { -1, 9, -1, -1, -1, -1, 4, -1, -1 },

            };
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    sudoku9x91.setNumber(new Point(x, y), board[y][x]);
                }


            checkPoints = new WFC_CheckPoints();
            InvokeUpdate(this, true);
        }

        private void highlightMinimums(int[][][] vals, int[,] currentVals)
        {
            List<Point> mins = WFC_MinFinder_Arr.findMinimums(vals, currentVals, finalValues);
            sudoku9x91.highlightSquares(mins);
        }

        private void tryWFC_BRAIN()
        {
            Point p = brain.collapseAPoint(sudoku9x91.getTableAlt(), setMapLocation, resetPoint, checkPoints, getPossibles);
            InvokeUpdate(this, true);
        }

        private int[][][] getPossibles()
        {
            return fullRuleSet.getFullTablePossibleData(sudoku9x91.getTableAlt()).getPossibleValuesAs2DArrOfArrs();
        }

        private void updateMap(Point p, int val)
        {
            sudoku9x91.setNumber(p, val);
        }

        private void resetPoint(int[,] map, Point p)
        {
            sudoku9x91.resetPoint(p);
        }

        public void InvokeUpdate(object sender, bool e)
        {
            vals = fullRuleSet.getFullTablePossibleData(sudoku9x91.getTableAlt()).getPossibleValuesAs2DArrOfArrs();
            sudoku9x91.setAvailables(vals);
            highlightMinimums(vals, sudoku9x91.getTableAlt());
            label1.Text = checkPoints.toString();
        }

        public void setMapLocation(Point p, int val)
        {
            sudoku9x91.setNumber(p, val);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; !finished(); i++)
                try
                {
                    System.Diagnostics.Debug.WriteLine("\n\n\n\n\n");
                    tryWFC_BRAIN();
                    if ((i % 100) == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Heeeeey\n\n\n\n");
                    }
                } catch
                {

                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkPoints.resetToPreviousCheckPoint(sudoku9x91.getTableAlt(), resetPoint);
            label1.Text = checkPoints.toString();
        }

        private bool finished()
        {
            int[,] vals;
            vals = sudoku9x91.getTableAlt();
            foreach(int i in vals)
            {
                if (i == -1) return false;
            }
            return true;
        }
    }
}
