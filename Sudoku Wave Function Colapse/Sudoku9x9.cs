using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_Wave_Function_Colapse
{
    public partial class Sudoku9x9 : UserControl
    {
        Sudoku3x3[][] sudoku3x3_arr;

        public event EventHandler<bool> NeedUpdate;

        public Sudoku9x9()
        {
            InitializeComponent();
            sudoku3x3_arr =
            [
                [sudoku3x31, sudoku3x32, sudoku3x33],
                [sudoku3x34, sudoku3x35, sudoku3x36],
                [sudoku3x37, sudoku3x38, sudoku3x39]
            ];
        }

        public int[] getRow(int bigRow, int row)
        {
            row %= 3;
            switch (bigRow)
            {
                case 0:
                    //return [1, -1, -1, -1, -1, -1, -1, -1, -1];
                    return sudoku3x31.getRow(row).Concat(sudoku3x32.getRow(row)).Concat(sudoku3x33.getRow(row)).ToArray();
                case 1:
                    //return [1, -1, -1, -1, -1, -1, -1, -1, -1];
                    return sudoku3x34.getRow(row).Concat(sudoku3x35.getRow(row)).Concat(sudoku3x36.getRow(row)).ToArray();
                case 2:
                    //return [1, -1, -1, -1, -1, -1, -1, -1, -1];
                    return sudoku3x37.getRow(row).Concat(sudoku3x38.getRow(row)).Concat(sudoku3x39.getRow(row)).ToArray();
            }

            return new int[0];
        }

        public int[][] getTable()
        {
            int[][] table = new int[9][];
            for(int y = 0; y < 3; y++)
            {
                for(int x = 0; x < 3; x++)
                {
                    table[(y*3)+x] = getRow(y, x);
                }
            }
            return table;
        }
        public int[,] getTableAlt()
        {
            int[,] table = new int[9, 9];
            int[][] originalTable = getTable();
            for(int y = 0; y < 9; y++)
                for(int x = 0; x < 9; x++) 
                {
                    table[y,x] = originalTable[y][x];
                }
            return table;
        }

        public void setAvailables(int[][][] possibles)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    sudoku3x3_arr[x][y].setAvailables(possibles);
                    sudoku3x3_arr[x][y].resetAllPriorities();
                }
            }
        }

        public void highlightSquares(List<Point> points)
        {
            foreach(Point p in points)
            {
                int subX = (int)(p.X) / 3;
                int subY = (int)(p.Y) / 3;
                int x1 = p.X % 3;
                int y1 = p.Y % 3;
                Point subPoint = new Point(x1, y1);
                sudoku3x3_arr[subY][subX].setPriority(subPoint, true);
            }
        }

        public void setNumber(Point p, int val)
        {
            int subX = (int)(p.X) / 3;
            int subY = (int)(p.Y) / 3;
            int x1 = p.X % 3;
            int y1 = p.Y % 3;
            Point subPoint = new Point(x1, y1);
            sudoku3x3_arr[subY][subX].setNumber(subPoint, val);
        }

        public void reset()
        {
            foreach(Sudoku3x3[] sqs in sudoku3x3_arr)
            {
                foreach(Sudoku3x3 sq in sqs)
                {
                    sq.reset();
                }
            }
            sudoku3x3_arr[0][0].innerSquares[0][0].setNumber(9);
            sudoku3x3_arr[0][1].innerSquares[0][1].setNumber(8);
        }

        public void InvokeUpdate(object sender, bool e)
        {
            NeedUpdate.Invoke(this, true);
        }
    }
}
