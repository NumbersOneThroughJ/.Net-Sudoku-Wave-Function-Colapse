using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Wave_Function_Colapse.Wave_Function_Colapse.WFC_Algorithm
{

    /// <summary>
    /// This holds a list of integer 'hashes' that corresponds to the necessary hashes for the multiplexer
    /// </summary>
    /// <typeparam name="E"></typeparam>
    internal class WFC_MUX_Hash
    {
        private static readonly int BITCOUNT = 32;
        List<Int32> hashs;
        
        /// <summary>
        /// alocates a list of integers for enough space for the amount of variables in the multiplexer
        /// </summary>
        /// <param name="multiplexer"></param>
        public WFC_MUX_Hash(double COUNT)
        {
            hashs = new List<Int32>( (int)( Math.Ceiling( (COUNT)/(double)BITCOUNT)) );
        }

        /// <summary>
        /// Allocates a Hash Directly using the number required
        /// Meant to be used only in this class
        /// USE AN MUX TO MAKE A HASH OTHERWISE
        /// </summary>
        /// <param name="numHashes"></param>
        private WFC_MUX_Hash(int numHashes)
        {
            hashs = new List<Int32>(numHashes);
        }

        /// <summary>
        /// Binary ands the two hash filters together
        /// </summary>
        /// <param name="h1"></param>
        /// <param name="h2"></param>
        /// <returns></returns>
        public static WFC_MUX_Hash operator & (WFC_MUX_Hash h1, WFC_MUX_Hash h2)
        {
            if (h1.hashs.Capacity != h2.hashs.Capacity)
            {
                throw new ArgumentException("Hash Capacities are different. Something went wrong","h1 capacity != h2 capacity");
            }
            WFC_MUX_Hash resultHash = new WFC_MUX_Hash(h1.hashs.Capacity);

            for (int i = 0; i<h1.hashs.Capacity; i++)
            {
                resultHash.hashs[i] = h1.hashs[i] & h2.hashs[i];
            }

            return resultHash;
        }

        /// <summary>
        /// Binary ors the two hash filters together
        /// </summary>
        /// <param name="h1"></param>
        /// <param name="h2"></param>
        /// <returns></returns>
        public static WFC_MUX_Hash operator | (WFC_MUX_Hash h1, WFC_MUX_Hash h2)
        {
            if (h1.hashs.Capacity != h2.hashs.Capacity)
            {
                throw new ArgumentException("Hash Capacities are different. Something went wrong", "h1 capacity != h2 capacity");
            }
            WFC_MUX_Hash resultHash = new WFC_MUX_Hash(h1.hashs.Capacity);

            for (int i = 0; i < h1.hashs.Capacity; i++)
            {
                resultHash.hashs[i] = h1.hashs[i] | h2.hashs[i];
            }

            return resultHash;
        }
    }

    /// <summary>
    /// takes a hash object and converts it to a list of objects
    /// </summary>
    /// <typeparam name="E"></typeparam>
    internal class WFC_MUX<E> where E : notnull
    {
        #region VARIABLES
        /// <summary>
        /// Variables
        /// These will allow use of both the strength of lists
        /// and dictionaries as well
        /// </summary>
        private List<E> outputVariabls;
        private List<int> weight;
        private Dictionary<E, int> outProbDict;

        public int COUNT => outputVariabls.Count;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a multiplexer with the provided lists
        ///    This zips the two lists together, make sure the variable and weight
        ///    that you want to associate together are at the same index in each list
        /// </summary>
        /// <param name="outputVariabls">Variables to output from a given input</param>
        /// <param name="weight">Integer weight of variable at the same index</param>
        public WFC_MUX(List<E> outputVariabls, List<int> weight)
        {
            this.outputVariabls = outputVariabls;
            this.weight = weight;
            outProbDict = outputVariabls.Zip(weight).ToDictionary(x => x.First, x => x.Second);
        }

        /// <summary>
        /// Constructs a mutltiplexer with the provided dictionary
        /// </summary>
        /// <param name="outProbDict">Dictionary of values and probabilities</param>
        public WFC_MUX(Dictionary<E, int> outProbDict)
        {
            this.outProbDict = outProbDict;
            this.outputVariabls = outProbDict.Keys.ToList<E>();
            this.weight = outProbDict.Values.ToList<int>();
        }
        #endregion

        #region functions

        private WFC_MUX_Hash listToHash()
        {
            return new WFC_MUX_Hash();
        }

        private List<E> HashToList()
        {

        }

        #endregion
    }
}
