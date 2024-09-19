using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{
    /*WFC_CheckPoints
     * A checkpoint is what is made when a branching choice is detected
     *  in the possible values the WFC can choose from
     * In this event, the checkpointClass will store that cell's location
     * In the event that a branching path does not lead to a proper solution,
     *  the checkpoint will be used to take a step back until there are no
     *  other valid options, or a solution is found
     * Indivually reverts the steps backwards
     */
    internal class WFC_CheckPoints
    {
        private List<WFC_CheckPoints> checkPoints;

        public PossibleValuesMap returnToCheckPoint(int i, PossibleValuesMap current)
        {
            throw new NotImplementedException();
        }
    }
}
