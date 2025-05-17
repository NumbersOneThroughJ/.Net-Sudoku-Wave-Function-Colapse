using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public UInt32[] hashs;
        
        /// <summary>
        /// alocates a list of integers for enough space for the amount of variables in the multiplexer
        /// </summary>
        /// <param name="multiplexer"></param>
        public WFC_MUX_Hash(double COUNT)
        {
            hashs = new UInt32[(int)( Math.Ceiling( (COUNT)/(double)BITCOUNT))];
        }

        /// <summary>
        /// Allocates a Hash Directly using the number required
        /// Meant to be used only in this class
        /// USE AN MUX TO MAKE A HASH OTHERWISE
        /// </summary>
        /// <param name="numHashes"></param>
        public WFC_MUX_Hash(UInt32 numHashes)
        {
            hashs = new UInt32[numHashes];
        }

        /// <summary>
        /// Increases the hash count by the number requested
        /// </summary>
        /// <param name="numHashes"></param>
        public void allocateNewHashes(int numHashes)
        {
            Array.Resize(ref hashs, numHashes+hashs.Length);
        }

        /// <summary>
        /// Sets the given hash at index given to the new value given
        /// </summary>
        /// <param name="hashIndex"></param>
        /// <param name="hash"></param>
        public void setHash(int hashIndex, UInt32 hash)
        {
            hashs[hashIndex] = hash;
        }

        public UInt32 getHash(int hashIndex)
        {
            return hashs[hashIndex];
        }

        /// <summary>
        /// Binary ands the two hash filters together
        /// </summary>
        /// <param name="h1"></param>
        /// <param name="h2"></param>
        /// <returns></returns>
        public static WFC_MUX_Hash operator & (WFC_MUX_Hash h1, WFC_MUX_Hash h2)
        {
            if (h1.hashs.Length != h2.hashs.Length)
            {
                throw new ArgumentException("Hash Capacities are different. Something went wrong","h1 capacity != h2 capacity");
            }
            WFC_MUX_Hash resultHash = new WFC_MUX_Hash(h1.hashs.Length);

            for (int i = 0; i<h1.hashs.Length; i++)
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
            if (h1.hashs.Length != h2.hashs.Length)
            {
                throw new ArgumentException("Hash Capacities are different. Something went wrong", "h1 capacity != h2 capacity");
            }
            WFC_MUX_Hash resultHash = new WFC_MUX_Hash(h1.hashs.Length);

            for (int i = 0; i < h1.hashs.Length; i++)
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
        private Dictionary<E, int> valueIndexDict;
        //Need a dictionary hash of E to index. This will speed up drastically.
        private int hashesCount;

        //public Boolean weighted;
        private static readonly int BITCOUNT = 32;

        public int COUNT => outputVariables.Length;
        public int HashCount => hashesCount;
        public int TOTALBITS => hashesCount * BITCOUNT;

        private static readonly UInt32[] binaryLocation = new UInt32[] {
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
            this.outProbDict = outputVariabls.Zip(weight).ToDictionary(x => x.First, x => x.Second);
            this.valueIndexDict = outputVariables.Zip(Enumerable.Range(0, outputVariables.Length).ToArray()).ToDictionary(x => x.First, x => x.Second);
            this.hashesCount = (int)Math.Ceiling(((double)outputVariables.Length)/BITCOUNT);
        }

        /// <summary>
        /// Constructs a mutltiplexer with the provided dictionary
        /// </summary>
        /// <param name="outProbDict">Dictionary of values and probabilities</param>
        public WFC_MUX(Dictionary<E, int> outProbDict/*, bool weighted*/)
        {
            this.outProbDict = outProbDict;
            this.outputVariables = outProbDict.Keys.ToArray<E>();
            this.weight = outProbDict.Values.ToArray<int>();
            this.hashesCount = (int)Math.Ceiling(((double)outputVariables.Length) / BITCOUNT);
            this.valueIndexDict = outputVariables.Zip(Enumerable.Range(0, outputVariables.Length).ToArray()).ToDictionary(x => x.First, x => x.Second);
        }
        #endregion

        #region functions

        /// <summary>
        /// Creates a multiplexer object based on the source you gave it
        /// Error logging requires .ToString method of E be defined for better tracing.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public WFC_MUX_Hash listToHash(E[] source/*, bool weighted*/)
        {
            WFC_MUX_Hash returnHash = new WFC_MUX_Hash(hashesCount);
            int index = -1;
            int hashBitIndex = -1;
            int hashIndex = -1;

            foreach(E e in source)
            {
                index = bitIndexOf(e);
                //Add a enum for mode, should add the value? or should throw error?
                if (index == -1) throw new Exception
                    ("WFC_MUX_Hash does not contain a reference for the specified value: " + e.ToString());

                //Stopping here, should the hash be calculated in the mux or in the hash object?
                // I think the MUX should output hash objects. And that would prevent a hash object from being made outside of the mux.
                hashBitIndex = index % BITCOUNT;
                hashIndex = index / BITCOUNT;

                returnHash.hashs[hashIndex] |= binaryLocation[hashBitIndex];
            }

            return returnHash;
        }

        public List<E> HashToList(WFC_MUX_Hash h)
        {
            List<E> retList = new List<E>();

            //Loop through the hash
            //Add a value for each bit found
            //Honestly, prob just easiest to do an and statement BITCOUNT times
            //would be less processing power too on the for loop
            //  I have a coding eating disorder. The overhead from a for loop is negligable XD

            int hashBitIndex;
            int hashIndex;

            for (int i = 0; i<outputVariables.Length; i++)
            {
                hashBitIndex = i % BITCOUNT;
                hashIndex = i / BITCOUNT;
                UInt32 hash = h.getHash(hashIndex);

                if ((hash | binaryLocation[hashBitIndex]) == hash)
                {
                    retList.Add(outputVariables[i]);
                }
            }

            return retList;
        }

        /// <summary>
        /// Returns the bit index of the specificed E object
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int bitIndexOf(E e)
        {
            try
            {
                return valueIndexDict[e];
            } catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// Adds a new value and weight to the lists of values and weights
        /// returns the hash bit index of the newly registered value
        /// </summary>
        /// <param name="e"></param>
        /// <param name="newWeight"></param>
        /// <returns></returns>
        public int registerValue(E e, int newWeight)
        {
            // private E[] outputVariables;
            // private int[] weight;
            // private List<WFC_MUX_Hash> hashsMade;
            // private Dictionary<E, int> outProbDict;
            // private Dictionary<E, int> valueIndexDict;
            // //Need a dictionary hash of E to index. This will speed up drastically.
            // private int hashesCount;

            if (bitIndexOf(e) != -1)
            {
                // //Stores the variable e to its index in output variables
                valueIndexDict.Add(e, outputVariables.Length);
                //Store the variable e to the weights table
                Array.Resize<int>(ref weight, weight.Length + 1);
                Array.Resize<E>(ref outputVariables, outputVariables.Length + 1);
                fixHashCount();
            }
            outProbDict.Add(e, newWeight);
            return outputVariables.Length-1;
        }
        public int[] registerValues(E[] e, int[] Weights)
        {
            int[] indexes = new int[e.Length];
            for (int i = 0; i< e.Length; i++)
            {
                indexes[i] = registerValue(e[i], Weights[i]);
            }
            return indexes;
        }


        /// <summary>
        /// Returns the number of hashes needed to accomidate the number of variables
        /// </summary>
        /// <returns></returns>
        private int hashesNeeded()
        {
            return (int)Math.Ceiling(((double)outputVariables.Length) / BITCOUNT) - HashCount;
        }

        /// <summary>
        /// Increases the hash count on each of the allocated WFC_MUX_Hashs this mux has made
        /// </summary>
        private void increaseHashCount(int numHashIncrease)
        {
            foreach(WFC_MUX_Hash h in hashsMade)
            {
                h.allocateNewHashes(numHashIncrease);
            }
            hashesCount += numHashIncrease;
        }

        /// <summary>
        /// increases the hashcount of each mux object to mach what is needed
        /// </summary>
        private void fixHashCount()
        {
            int newHashes = hashesNeeded();
            increaseHashCount(newHashes);
        }

        #endregion
    }
}
