using Microsoft.VisualBasic;
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
    /// Currently I do not have the ability to dynamically update the obect
    ///  With the list of hashes, I might be able to have it recursively update each hash, however that would take a loooot of processing time (relatively a lot)
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
        private E[] outputVariables;
        private int[] weight;
        private List<WFC_MUX_Hash> hashsMade;
        private Dictionary<E, int> outProbDict;
        //Need a dictionary hash of E to index. This will speed up drastically.
        private int hashesCount;

        public Boolean weighted;

        public int COUNT => outputVariables.Length;
        public int HashCount => hashesCount;

        private static readonly int[] binaryLocation = new int[] {
            0b00000000000000000000000000000001,
            0b00000000000000000000000000000010,
            0b00000000000000000000000000000100,
            0b00000000000000000000000000001000,
            0b00000000000000000000000000010000,
            0b00000000000000000000000000100000,
            0b00000000000000000000000001000000,
            0b00000000000000000000000010000000,
            0b00000000000000000000000100000000,
            0b00000000000000000000001000000000,
            0b00000000000000000000010000000000,
            0b00000000000000000000100000000000,
            0b00000000000000000001000000000000,
            0b00000000000000000010000000000000,
            0b00000000000000000100000000000000,
            0b00000000000000001000000000000000,
            0b00000000000000010000000000000000,
            0b00000000000000100000000000000000,
            0b00000000000001000000000000000000,
            0b00000000000010000000000000000000,
            0b00000000000100000000000000000000,
            0b00000000001000000000000000000000,
            0b00000000010000000000000000000000,
            0b00000000100000000000000000000000,
            0b00000001000000000000000000000000,
            0b00000010000000000000000000000000,
            0b00000100000000000000000000000000,
            0b00001000000000000000000000000000,
            0b00010000000000000000000000000000,
            0b00100000000000000000000000000000,
            0b01000000000000000000000000000000,
            0b1000000000000000000000000000000,
        };
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a multiplexer with the provided lists
        ///    This zips the two lists together, make sure the variable and weight
        ///    that you want to associate together are at the same index in each list
        /// </summary>
        /// <param name="outputVariabls">Variables to output from a given input</param>
        /// <param name="weight">Integer weight of variable at the same index</param>
        public WFC_MUX(E[] outputVariabls, int[] weight)
        {
            this.outputVariables = outputVariabls;
            this.weight = weight;
            outProbDict = outputVariabls.Zip(weight).ToDictionary(x => x.First, x => x.Second);
            this.hashesCount = (int)Math.Ceiling(((double)outputVariables.Length)/32);
        }

        /// <summary>
        /// Constructs a mutltiplexer with the provided dictionary
        /// </summary>
        /// <param name="outProbDict">Dictionary of values and probabilities</param>
        public WFC_MUX(Dictionary<E, int> outProbDict, bool weighted)
        {
            this.outProbDict = outProbDict;
            this.outputVariables = outProbDict.Keys.ToArray<E>();
            this.weight = outProbDict.Values.ToArray<int>();
            this.weighted = weighted;
            this.hashesCount = (int)Math.Ceiling(((double)outputVariables.Length) / 32);
        }
        #endregion

        #region functions

        /// <summary>
        /// Creates a multiplexer object based on the source you gave it
        /// Error logging requires .ToString method of E be defined for better tracing.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private WFC_MUX_Hash listToHash(E[] source, bool weighted)
        {
            int index = -1;


            foreach(E e in source)
            {
                index = bitIndexOf(e);
                if (index == -1) throw new Exception
                    ("WFC_MUX_Hash does not contain a reference for the specified value: " + e.ToString());
                //Stopping here, should the hash be calculated in the mux or in the hash object?
                // I think the MUX should output hash objects. And that would prevent a hash object from being made outside of the mux.
            }

            return new WFC_MUX_Hash();
        }

        private List<E> HashToList()
        {

        }

        public int bitIndexOf(E e)
        {
            return Array.IndexOf(outputVariables, e);
        }

        #endregion
    }
}
