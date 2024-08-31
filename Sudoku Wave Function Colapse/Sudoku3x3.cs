using Sudoku_Wave_Function_Colapse.Classes;
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
    public partial class Sudoku3x3 : UserControl
    {
        private Point _localLocation;
        [Browsable(true)] // This makes the property visible in the designer
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Point LocalLocation
        {
            get { return _localLocation; }
            set
            {
                _localLocation = value;
                // Perform any additional actions when the property is set if needed
            }
        }

        public SudokuInnerSquare[][] innerSquares;

        public Sudoku3x3()
        {
            InitializeComponent();
            innerSquares = 
                [
                    [sudokuInnerSquare1, sudokuInnerSquare2, sudokuInnerSquare3],
                    [sudokuInnerSquare4, sudokuInnerSquare5, sudokuInnerSquare6],
                    [sudokuInnerSquare7, sudokuInnerSquare8, sudokuInnerSquare9]
                ];
        }
        
        public int[] getRow(int row)
        {
            row %= 3;
            int[] nums = new int[0];
            switch(row)
            {
                case 0:
                    nums = [sudokuInnerSquare1.Number, sudokuInnerSquare2.Number, sudokuInnerSquare3.Number];
                    break;
                case 1:
                    nums = [sudokuInnerSquare4.Number, sudokuInnerSquare5.Number, sudokuInnerSquare6.Number];
                    break;
                case 2:
                    nums = [sudokuInnerSquare7.Number, sudokuInnerSquare8.Number, sudokuInnerSquare9.Number];
                    break;
            }

            return nums;
        }

        public void setAvailables(int[][][] possibles)
        {
            for(int x = 0; x<3; x++)
            {
                int xI = _localLocation.X * 3 + x;
                for (int y = 0; y<3; y++)
                {
                    int yI = _localLocation.Y * 3 + y;
                    innerSquares[y][x].setAvailableNumbers(possibles[yI][xI]);
                }
            }
        }

        public void reset()
        {
            foreach(SudokuInnerSquare[] sqs  in innerSquares)
            {
                foreach(SudokuInnerSquare sq in sqs)
                {
                    sq.setNumber(-1);
                }
            }
        }

    }
}
