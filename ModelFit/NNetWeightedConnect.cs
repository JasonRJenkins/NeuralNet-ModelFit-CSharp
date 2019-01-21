/////////////////////////////////////////////////////////////////////
//
// Implements the NNetWeightedConnect class
//
// Author: Jason Jenkins
//
// This class is used by the neural network class (NeuralNet) and
// represents the weighted connections that link the layers of a 
// neural network together.
//
// The layers of a neural network are connected by a system of
// weighted connections. Each unit in a given layer of the network
// (excluding the output layer) has a single connection to every 
// unit in the next layer. These connections are initially given
// a random value which is then updated when the neural network
// is trained.
//
// In this class the weighted connections between two layers 
// consist of a given number of input and output nodes. If the
// first layer of a network contains three units and the second
// layer contains four to connect these layers together we require
// a weighted connection with three input nodes and four output
// nodes.
//
// Each input node is connected to every output node. The input 
// nodes have their values set by the setInputs method and these
// values represent the activated output of a particular layer
// within the network. The value of a specific output node is the
// result of applying the weighted connections between that output
// node and all the connected input nodes - each input node value is
// multiplied by the weighted connection between the input node and 
// the given output node in turn and the results are then summed to 
// give the output node value. The output node values can be obtained
// via the getOutputs method and these values can then be used by the
// network as the input values to the activation functions of the
// next layer.
//
// The weights of the connections between a given output node and
// the input nodes can be retrieved via the getWeightVector method
// and set via the setWeightVector method. These two methods are 
// typically called by the network training process.
//
/////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

/////////////////////////////////////////////////////////////////////

namespace NeuralNetwork.WeightConnect
{
    /// <summary>
    /// This class is used by the neural network class (NeuralNet) and represents
    /// the weighted connections that link the layers of a neural network together.
    /// 
    /// The layers of a neural network are connected by a system of weighted 
    /// connections. Each unit in a given layer of the network (excluding the 
    /// output layer) has a single connection to every unit in the next layer. 
    /// These connections are initially given a random value which is then updated
    /// when the neural network is trained.
    /// </summary>
    /// 
    public class NNetWeightedConnect
    {
        /////////////////////////////////////////////////////////////////////
        // Private Data Members
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// he number of input nodes
        /// </summary>
        private int mNumInNodes = -1;

        /// <summary>
        /// the number of output nodes
        /// </summary>
        private int mNumOutNodes = -1;

        /// <summary>
        /// the input values
        /// </summary>
        private List<double> mInputs = new List<double>();

        /// <summary>
        /// the output values
        /// </summary>
        private List<double> mOutputs = new List<double>();

        /// <summary>
        /// the weighted connection values
        /// </summary>
        private List<List<double>> mWeights = new List<List<double>>();

        /////////////////////////////////////////////////////////////////////
        // Constructors
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// 
        public NNetWeightedConnect() {}

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="original">the NNetWeightedConnect object to be copied</param>
        /// 
        public NNetWeightedConnect(NNetWeightedConnect original)
        {
            // the number of input nodes
            mNumInNodes = original.mNumInNodes;

            // the number of input nodes
            mNumOutNodes = original.mNumOutNodes;

            // the input values
            for (int i = 0; i < original.mInputs.Count; i++)
            {
                mInputs.Add(original.mInputs[i]);
            }

            // the output values
            for (int j = 0; j < original.mOutputs.Count; j++)
            {
                mOutputs.Add(original.mOutputs[j]);
            }

            // the weighted connection values
            for (int row = 0; row < mNumOutNodes; row++)
            {
                List<double> vec = new List<double>();

                for (int col = 0; col < mNumInNodes; col++)
                {
                    vec.Add(original.mWeights[row][col]);
                }

                mWeights.Add(vec);
            }
        }

        /// <summary>
        /// Constructs a connection between the given number of nodes.
        /// </summary>
        /// <param name="numInNodes">the number of input nodes</param>
        /// <param name="numOutNodes">the number of output nodes</param>
        /// 
        public NNetWeightedConnect(int numInNodes, int numOutNodes)
        {
            // ignore invalid data
            if (numInNodes > 0 && numOutNodes > 0)
            {
                mNumInNodes = numInNodes;
                mNumOutNodes = numOutNodes;

                InitialiseWeights();
            }
        }

        /////////////////////////////////////////////////////////////////////
        // Properties
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The number of input nodes.
        /// </summary>
        /// 
        public int NumInputNodes
        {
            get
            {
                return mNumInNodes;
            }
        }

        /// <summary>
        /// The number of output nodes.
        /// </summary>
        /// 
        public int NumOutputNodes
        {
            get
            {
                return mNumOutNodes;
            }
        }

        /////////////////////////////////////////////////////////////////////
        // Public Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Sets the number of input and output nodes.
        /// 
        /// The weighted connections are randomly initialised over the range
        /// -(initRange/2) to +(initRange/2).
        /// </summary>
        /// <param name="numInNodes">the number of input nodes</param>
        /// <param name="numOutNodes">the number of output nodes</param>
        /// <param name="initRange">the range used for random initialisation</param>
        /// 
        public void SetNumNodes(int numInNodes, int numOutNodes, double initRange)
        {
            // ignore invalid data
            if (numInNodes > 0 && numOutNodes > 0 && initRange > 0)
            {
                mNumInNodes = numInNodes;
                mNumOutNodes = numOutNodes;

                InitialiseWeights(initRange);
            }
        }

        /// <summary>
        /// Sets the input values for the weighted connection.
        /// 
        /// The input value for the first input node is the first value stored
        /// in the list and the input value for the second input node is the
        /// second value stored in the list and so on.
        /// </summary>
        /// <param name="inputs">a list of input values</param>
        /// 
        public void SetInputs(List<double> inputs)
        {
	        // make sure the size of the input list corresponds to the number of input nodes
	        if (inputs.Count == mNumInNodes)
	        {
		        if (mInputs.Count != 0)
		        {
                    mInputs.Clear();
		        }

                mInputs.AddRange(inputs);
            }
        }

        /// <summary>
        /// Gets the output values for the weighted connection.
        /// 
        /// The output values are calculated by applying the weighted
        /// connections to the input node values.
        /// </summary>
        /// <param name="outputs">the output values</param>
        /// 
        public void GetOutputs(List<double> outputs)
        {
            // clear the output list if necessary
            if (outputs.Count != 0)
            {
                outputs.Clear();
            }

            CalculateOutput();

            outputs.AddRange(mOutputs);
        }

        /// <summary>
        /// Gets the weighted connections vector for a given output node.
        /// 
        /// This method is typically called when training the network.
        /// </summary>
        /// <param name="node">the index of the output node</param>
        /// <param name="weights">the weighted connections vector (stored in a list)</param>
        /// 
        public void GetWeightVector(int node, List<double> weights)
        {
            if (node < mWeights.Count && node >= 0)
            {
                List<double> vecWeights = mWeights[node];

                if (weights.Count != 0)
                {
                    weights.Clear();
                }

                weights.AddRange(vecWeights);
            }
        }

        /// <summary>
        /// Sets the weighted connections vector for a given output node.
        /// 
        /// This method is typically called by the training process to update the weighted connections.
        /// </summary>
        /// <param name="node">the index of the output node</param>
        /// <param name="weights">the weighted connections vector (stored in a list)</param>
        /// 
        public void SetWeightVector(int node, List<double> weights)
        {
	        if (node < mWeights.Count && node >= 0)
	        {
		        if (mWeights[node].Count == weights.Count)
		        {
			        for (int i = 0; i < weights.Count; i++)
			        {
				        mWeights[node][i] = weights[i];
			        }
                }
	        }
        }

        /////////////////////////////////////////////////////////////////////
        // Private Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Randomly initialises the weighted connections.
        /// 
        /// The weighted connections are randomly initialised over the range
        /// -(initRange/2) to +(initRange/2).
        /// </summary>
        /// <param name="initRange">the range used for random initialisation</param>
        /// 
        private void InitialiseWeights(double initRange = 2.0)
        {
            Random rand = new Random((int)initRange);   // use a fixed seed for the time being

            // initialise a weight list for each of the output nodes
            for (int i = 0; i < mNumOutNodes; i++)
            {
                List<double> initVec = new List<double>();

                // the size of the vector is equal to the number of input nodes
                for (int j = 0; j < mNumInNodes; j++)
                {
                    double initVal = (double)rand.Next(100001);

                    // randomly iniialise a vector component
                    initVal = initRange * (initVal / 100000) - (initRange / 2);

                    initVec.Add(initVal);
                }

                mWeights.Add(initVec);
            }
        }

        /// <summary>
        /// Calculates the output values for all the output nodes.
        /// </summary>
        /// 
        private void CalculateOutput()
        {
            if (mOutputs.Count > 0)
            {
                mOutputs.Clear();
            }

            for (int i = 0; i < mNumOutNodes; i++)
            {
                double component = GetNodeValue(i);

                mOutputs.Add(component);
            }
        }

        /// <summary>
        /// Calculates the output value for the given output node.
        /// </summary>
        /// <param name="node">the index of the output node</param>
        /// <returns>the value of the output node</returns>
        /// 
        private double GetNodeValue(int node)
        {
            double value = 0;
            List<double> nodeVec = new List<double>();
            nodeVec = mWeights[node];

            if (mNumInNodes == nodeVec.Count)
            {
                for (int i = 0; i < mNumInNodes; i++)
                {
                    value += nodeVec[i] * mInputs[i];
                }
            }

            return value;
        }
    }
}

/////////////////////////////////////////////////////////////////////
