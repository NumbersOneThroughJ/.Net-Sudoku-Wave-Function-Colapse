using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Classes
{
    /* Font AutoSizer
     * Disclaimer : As far as I know, there is no easy way to auto size fonts in this so I made a class to do it for me
     * 
     * Description : Keeps a record of objects that extends Control and sets there font size by container size
     */
    internal class FontAutoSizer<Sizeable> where Sizeable : Control
    {
        //Ratio of pixel height
        const float FontToPixelRatio = 12f / 16f;

        //Private record of the objects that extend Control
        private List<Sizeable> targets;

        //Initiates the list
        public FontAutoSizer()
        {
            targets = new List<Sizeable>();
        }

        /* addTarget
         * Parameters : Target - object to add to record
         * Description : Adds supplied object to internal record of objects to resize
         */
        public void addTarget(Sizeable Target)
        {
            targets.Add(Target);
        }

        /* resizeTargetsFont
         * Description : Function to be called when resize occurred. This will loop through each target and resize the font
         */
        public void resizeTargetsFont()
        {
            foreach (Sizeable Target in targets)
            {
                AutoSizeTarget(Target);
            }
        }

        /* AutoSizeTarget
         * Parameters : Target - Object to resize it's font
         * Description : Based on FontToPixelRatio, resizes the text to ensure it is in the correct size to fill target
         */
        private void AutoSizeTarget(Sizeable Target)
        {
            Font currentFont = Target.Font;
            float newFontSize = Math.Min(Target.Size.Height, Target.Size.Width) * FontToPixelRatio;
            Target.Font = new Font(currentFont.FontFamily, newFontSize);
        }
    }
}
