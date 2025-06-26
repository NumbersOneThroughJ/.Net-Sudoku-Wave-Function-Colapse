using Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.Rule.DefinedRules.ArrayRules._2D
{


    /// <summary>
    /// JACOB UPDATE THIS:
    ///    REMOVE THE REFERENCES TO A NEW Table everywhere. ( :) mostly done)
    ///    Have it edit the tables directly. That will help cut out a looooot of processing time for allocating arrays. (See next update request note)
    ///    Array.copy is what is taking most of the time. If I can minimize it, I can do it a little bit at a time.
    ///    
    /// JACOB UPDATE THIS TOO:
    ///     Combine Data into Possible Values Map. Maybe just full values map?
    ///     I don't like the idea of a raw int[,] that is required to be made seperately from possible values map.
    ///     
    ///     Add a bounds somehow to the -- ignore that
    ///     instead have the possible values map subsection operate to have a function to take values
    ///     Also, if there are any references to such, rules should not directly interact with the possible Values map array
    ///     Like have the possible Values map determine how to operate on a point. Do this to implement boundaries.
    ///     Make possible values map subsection extend from possibleValuesMap.
    /// 
    ///     Also
    ///         COMMENT YOUR SHIT
    ///         Can't figure this out without comments man
    ///         Sincerely - Jacob (Insanity achieved)
    /// </summary>
    internal abstract class ARule_2D_Base
    {
        #region Variables
        protected bool andMode;
        protected PossibleValuesMap loadedMapOfCurrentValues;
        protected int[,] data;
        #endregion

        #region Constructors
        public ARule_2D_Base(bool andMode)
        {
            this.andMode = andMode;
        }
        #endregion

        #region Abstract Functions

        //[Row, Collum]
        //Returns true if all conditions allow the current Value
        public abstract bool evaluatePoint(Point p, int[,] data);
        
        /// <summary>
        /// This will edit the possible values map stored within this object by the rules provided.
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="data"></param>
        public abstract void ApplyPossibleDataAboutPoint(Point p, int[,] data);

        #endregion

        #region rule Functions

        public bool evaluatePoint(int x, int y, int[,] data) { return evaluatePoint(new Point(x, y), data); }
        public bool evaluateFullTable(int[,] data)
        {
            for(int y = 0; y< data.GetLength(0); y++)
                for(int x = 0; x<data.GetLength(1); x++)
                {
                    if (!evaluatePoint(x, y, data)) return false;
                }
            return true;
        }
        public void ApplyPossibleDataAboutPoint(int x, int y, int[,] data) 
        {
            ApplyPossibleDataAboutPoint(new Point(x, y), data); 
        }

        /// <summary>
        /// Returns a reference to this' loaded table of rules
        /// </summary>
        /// <param name="data"></param>
        /// <param name="blackListPriority"></param>
        /// <returns></returns>
        public PossibleValuesMap getFullTablePossibleData()
        {
            for (int y = 0; y < data.GetLength(0); y++)
                for (int x = 0; x < data.GetLength(1); x++)
                {
                    ApplyPossibleDataAboutPoint(x, y, data);
                }
            return loadedMapOfCurrentValues;
        }
        #endregion

        #region RuleBase Functions

        /// <summary>
        /// This is a function used only for other rules to communicate the possible values map back and forth with each other
        /// </summary>
        /// <param name="map"></param>
        /// <param name="data"></param>
        protected void setDataRef(PossibleValuesMap map, int[,] data)
        {
            this.loadedMapOfCurrentValues = map;
            this.data = data;
        }

        /// <summary>
        /// Use this function to set the data of a rule. It will be given to any sub rules as well.
        /// This is heavy cost, use it sparingly as it will call clone of possibleValuesMap.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="data"></param>
        public void setCopyOfData(PossibleValuesMap map, int[,] data)
        {
            this.loadedMapOfCurrentValues = (PossibleValuesMap)map.Clone();
            this.data = (int[,])data.Clone();
        }

        #endregion
    }
}
