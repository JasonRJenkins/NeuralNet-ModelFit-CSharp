/////////////////////////////////////////////////////////////////////
//
// Implements the NeuralNet class
//
// Author: Jason Jenkins
//
// This class is a representation of a feed forward neural network.
//
// This class enables a neural network to be built comprising single 
// or multiple input and output values along with one or more hidden
// layers.
//
// The output and hidden layers can consist of any number of units
// or neurons and each layer can be given their own activation 
// function, to be used by all the units in that layer, from a 
// selection of available types:
//
// Threshold, Unipolar, Bipolar, Tanh, Gaussian, Arctan, Sine,
// Cosine, Sinc, Elliot, Linear, ISRU, SoftSign and SoftPlus.
//
// The activation function values can also be modified using two
// parameters: slope and amplify (see NNetUnit.cpp for details). 
//
// A NeuralNet object can be serialized to and de-serialized from
// a string representation which can be written to or read from a
// file. This allows a neural network to be used once training is
// complete or to continue training if required.
//
// The following code creates a neural network with 2 input units, 3
// output units and 2 hidden layers with 4 and 6 units respectively.
// The output units will use unipolar activation functions and the
// the hidden layer units will both use bipolar activation functions.
/*		
		NeuralNet net = new NeuralNet();
		net.NumInputs = 2;
		net.NumOutputs = 3;
		net.OutputUnitType = ActiveT.kUnipolar;
		net.AddLayer(4, ActiveT.kBipolar);		// the first hidden layer
		net.AddLayer(6, ActiveT.kBipolar);		// the second hidden layer
*/
// To use the neural network, once it has been trained, populate a
// List with the desired input values and call the getResponse
// method to populate another List with the output values.
/*
		List<double> inputs = new List<double>();
		List<double> outputs = new List<double>();
		inputs.Add(0.5);
		inputs.Add(0.2);
		net.GetResponse(inputs, outputs);

		double outputValue1 = outputs[0];
		double outputValue2 = outputs[1];
		double outputValue3 = outputs[2];
*/
//
/////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/////////////////////////////////////////////////////////////////////

using NeuralNetwork.NetUnit;
using NeuralNetwork.WeightConnect;

/////////////////////////////////////////////////////////////////////

namespace NeuralNetwork
{
    /// <summary>
    /// This class is a representation of a feed forward neural network.
    /// 
    /// This class enables a neural network to be built comprising single 
    /// or multiple inputs and output values along with one or more hidden
    /// layers.
    ///
    /// The output and hidden layers can consist of any number of units
    /// or neurons and each layer can be given their own activation 
    /// function, to be used by all the units in that layer.
    /// </summary>
    /// 
    public class NeuralNet
    {
        /////////////////////////////////////////////////////////////////////
        // Private Data Members
        /////////////////////////////////////////////////////////////////////

        /// <summary>the number of input units</summary>
        private int mNumInputs = 0;

        /// <summary>the number of output units</summary>
        private int mNumOutputs = 0;

        /// <summary>the number of hidden layers</summary>
        private int mNumLayers = 0;

        /// <summary>the output layer units activation function type</summary>
        private ActiveT mOutUnitType = ActiveT.kThreshold;

        /// <summary>the output layer units activation function slope value</summary>
        private double mOutUnitSlope = 1;

        /// <summary>the output layer units activation function amplify value</summary>
        private double mOutUnitAmplify = 1;

        /// <summary>the weighted connections linking the network layers</summary>
        private List<NNetWeightedConnect> mLayers = new List<NNetWeightedConnect>();

        /// <summary>the activation values for each of the network layers</summary>
        private List<List<double>> mActivations = new List<List<double>>();

        /// <summary>the input values for the layer activation functions</summary>
        private List<List<double>> mUnitInputs = new List<List<double>>();

        /// <summary>the hidden layer units activation function types</summary>
        private List<ActiveT> mActiveUnits = new List<ActiveT>();

        /// <summary>the hidden layer units activation function slope values</summary>
        private List<double> mActiveSlope = new List<double>();

        /// <summary>the hidden layer units activation function amplify values</summary>
        private List<double> mActiveAmplify = new List<double>();

        /////////////////////////////////////////////////////////////////////
        // Constructors
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// 
        public NeuralNet() {}

        /// <summary>
        /// Constructs a NeuralNet object from a file containing a network in serialized form.
        /// </summary>
        /// <param name="fname">the file containing the serialized data</param>
        /// 
        public NeuralNet(string fname)
        {
            InitializeFromFile(fname);
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="original">the NeuralNet object to be copied</param>
        /// 
        public NeuralNet(NeuralNet original)
        {
            CopyNeuralNet(original);
        }

        /// <summary>
        /// Copies the contents of the source neural network into this neural network.
        /// </summary>
        /// <param name="src">the source net, to be copied</param>
        /// 
        public void CopyNeuralNet(NeuralNet src)
        {
            mLayers.Clear();
            mActivations.Clear();
            mUnitInputs.Clear();
            mActiveUnits.Clear();
            mActiveSlope.Clear();
            mActiveAmplify.Clear();

            mNumInputs = src.mNumInputs;
            mNumOutputs = src.mNumOutputs;
            mNumLayers = src.mNumLayers;
            mOutUnitType = src.mOutUnitType;
            mOutUnitSlope = src.mOutUnitSlope;
            mOutUnitAmplify = src.mOutUnitAmplify;

            // the weighted connections linking the network layers
            for (int i = 0; i < src.mLayers.Count; i++)
            {
                NNetWeightedConnect wCnct = new NNetWeightedConnect(src.mLayers[i]);
                mLayers.Add(wCnct);
            }

            // the activation values for each of the network layers
            int rowLen = src.mActivations.Count;

            for (int row = 0; row < rowLen; row++)
            {
                List<double> vec = new List<double>();
                int colLen = src.mActivations[row].Count;

                for (int col = 0; col < colLen; col++)
                {
                    vec.Add(src.mActivations[row][col]);
                }

                mActivations.Add(vec);
            }

            // the input values for the layer activation functions
            rowLen = src.mUnitInputs.Count;

            for (int row = 0; row < rowLen; row++)
            {
                List<double> vec = new List<double>();
                int colLen = src.mUnitInputs[row].Count;

                for (int col = 0; col < colLen; col++)
                {
                    vec.Add(src.mUnitInputs[row][col]);
                }

                mUnitInputs.Add(vec);
            }

            // the hidden layer unit activation function types
            for (int i = 0; i < src.mActiveUnits.Count; i++)
            {
                mActiveUnits.Add(src.mActiveUnits[i]);
            }

            // the hidden layer unit activation function slope values
            for (int i = 0; i < src.mActiveSlope.Count; i++)
            {
                mActiveSlope.Add(src.mActiveSlope[i]);
            }

            // the hidden layer unit activation function amplify values
            for (int i = 0; i < src.mActiveAmplify.Count; i++)
            {
                mActiveAmplify.Add(src.mActiveAmplify[i]);
            }
        }

        /////////////////////////////////////////////////////////////////////
        // Properties
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The number of input units.
        /// </summary>
        /// 
        public int NumInputs
        {
            get
            {
                return mNumInputs;
            }
            set
            {
                // ignore invalid values
                if (value > 0)
                {
                    mNumInputs = value;
                }
            }
        }

        /// <summary>
        /// The number of output units.
        /// </summary>
        /// 
        public int NumOutputs
        {
            get
            {
                return mNumOutputs;
            }
            set
            {
                // ignore invalid values
                if (value > 0)
                {
                    mNumOutputs = value;
                }
            }
        }

        /// <summary>
        /// The number of hidden layers.
        /// </summary>
        /// 
        public int NumLayers
        {
            get
            {
                return mNumLayers;
            }
        }

        /// <summary>
        /// The output units activation type.
        /// </summary>
        /// 
        public ActiveT OutputUnitType
        {
            get
            {
                return mOutUnitType;
            }
            set
            {
                mOutUnitType = value;
            }
        }

        /// <summary>
        /// The output layer units activation function slope value.
        /// </summary>
        /// 
        public double OutputUnitSlope
        {
            get
            {
                return mOutUnitSlope;
            }
            set
            {
                // ignore invalid values
                if (value > 0)
                {
                    mOutUnitSlope = value;
                }
            }
        }

        /// <summary>
        /// The output layer units activation function amplify value.
        /// </summary>
        /// 
        public double OutputUnitAmplify
        {
            get
            {
                return mOutUnitAmplify;
            }
            set
            {
                // ignore invalid values
                if (value > 0)
                {
                    mOutUnitAmplify = value;
                }
            }
        }

        /////////////////////////////////////////////////////////////////////
        // Public Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Clears a NeuralNetwork object ready for re-use.
        /// </summary>
        /// 
        public void ClearNeuralNetwork()
        {
            mNumInputs = 0;
            NumOutputs = 0;
            mNumLayers = 0;
            mOutUnitType = ActiveT.kThreshold;
            mOutUnitSlope = 1;
            mOutUnitAmplify = 1;

            mLayers.Clear();
            mActivations.Clear();
            mUnitInputs.Clear();
            mActiveUnits.Clear();
            mActiveSlope.Clear();
            mActiveAmplify.Clear();
        }

        /// <summary>
        /// Adds a new hidden layer.
        /// 
        /// The hidden layers are stored in the order of calls to this method
        /// so the first call to AddLayer creates the first hidden layer, the
        /// second call creates the second layer and so on.
        /// </summary>
        /// <param name="numUnits">the number of units in the hidden layer</param>
        /// <param name="unitType">the layer unit activation function type (defaults to unipolar)</param>
        /// <param name="initRange">the range of the initial weighted connection values (defaults to 2 coressponding to -1 to +1)</param>
        /// <param name="slope">the layer unit activation function slope value (defaults to 1.0)</param>
        /// <param name="amplify">the layer unit activation function amplify value (defaults to 1.0)</param>
        /// <returns>0 if the layer is successfully added otherwise -1</returns>
        /// 
        public int AddLayer(int numUnits, ActiveT unitType = ActiveT.kUnipolar, 
                            double initRange = 2, double slope = 1, double amplify = 1)
        {
            NNetWeightedConnect connect = new NNetWeightedConnect();
            NNetWeightedConnect output = new NNetWeightedConnect();

            // ignore invalid values
            if (numUnits > 0 && initRange > 0 && slope > 0 && amplify > 0)
            {
                if (mNumLayers == 0)
                {
                    // configure the first hidden layer
                    if (mNumInputs > 0)
                    {
                        // set up the weighted connections between the input and the first layer
                        // the weighted connections are initialised with random values in the
                        // range: -(initRange / 2) to +(initRange / 2)
                        connect.SetNumNodes(mNumInputs, numUnits, initRange);

                        // store the unit type for the layer
                        mActiveUnits.Add(unitType);

                        // store the steepness of the activation function's slope
                        mActiveSlope.Add(slope);

                        // store the amplification factor of the activation function
                        mActiveAmplify.Add(amplify);

                        mNumLayers++;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    // configure subsequent hidden layers
                    if (mNumLayers > 0)
                    {
                        int nInputs = mLayers[mNumLayers - 1].NumOutputNodes;

                        // set up the weighted connections between the previous layer and the new one
                        // the weighted connections are initialised with random values in the
                        // range: -(initRange / 2) to +(initRange / 2)
                        connect.SetNumNodes(nInputs, numUnits, initRange);

                        // store the unit type for the layer
                        mActiveUnits.Add(unitType);

                        // store the steepness of the activation function's slope
                        mActiveSlope.Add(slope);

                        // store the amplification factor of the activation function
                        mActiveAmplify.Add(amplify);

                        mNumLayers++;
                    }
                    else
                    {
                        return -1;
                    }
                }

                // connect the last hidden layer to the output layer
                if (mNumLayers > 1)
                {
                    // overwrite the old output connections
                    mLayers[mNumLayers - 1] = connect;
                }
                else
                {
                    // add the connections for the first layer
                    mLayers.Add(connect);
                }

                // set up the weighted connections between the last layer and the output
                output.SetNumNodes(numUnits, mNumOutputs, initRange);

                // add the output connections
                mLayers.Add(output);
            }
            else
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Gets the details of the specified hidden layer.
        /// </summary>
        /// <param name="n">the specified hidden layer index</param>
        /// <param name="unitType">the layer unit activation function type</param>
        /// <param name="slope">the layer unit activation function slope value</param>
        /// <param name="amplify">the layer unit activation function amplify value</param>
        /// 
        public void GetLayerDetails(int n, ref ActiveT unitType, ref double slope, ref double amplify)
        {
            if (n >= 0 && n < mNumLayers)
            {
                unitType = mActiveUnits[n];
                slope = mActiveSlope[n];
                amplify = mActiveAmplify[n];
            }
        }

        /// <summary>
        /// Gets the response of the network to the given input.
        /// 
        /// The number of elements in the inputs vector should correspond to 
        /// the number of the input units.  If the inputs vector contains 
        /// more elements than this, the additional input values are ignored.
        /// </summary>
        /// <param name="inputs">the network input values</param>
        /// <param name="outputs">the network output values</param>
        /// 
        public void GetResponse(List<double> inputs, List<double> outputs)
        {
	        if (inputs.Count >= mNumInputs && mNumLayers > 0)
	        {
                List<double> inputVec = new List<double>();
                List<double> outputVec = new List<double>();
                NNetWeightedConnect connect = new NNetWeightedConnect();

                // clear any old activation and unit input values
                mActivations.Clear();
		        mUnitInputs.Clear();

		        // 'load' the input vector 
		        for (int i = 0; i < mNumInputs; i++)
		        {
			        inputVec.Add(inputs[i]);
		        }

                // get the weighted connections between the input layer and first layer
                connect = mLayers[0];
		
		        // apply the weighted connections
		        connect.SetInputs(inputVec);
		        connect.GetOutputs(outputVec);

		        // store the output vector - this contains the unit input values
		        mUnitInputs.Add(outputVec);

		        // clear the input vector so it can be used to hold the input for the next layer
		        inputVec.Clear();

		        // set the unit type, slope and amplification for the first layer
		        NNetUnit unit = new NNetUnit(mActiveUnits[0], mActiveSlope[0], mActiveAmplify[0]);

		        // activate the net units
		        for (int i = 0; i<(int)outputVec.Count; i++)
		        {
			        unit.Input = outputVec[i];
			        inputVec.Add(unit.GetActivation());
		        }

                // store the activations
                mActivations.Add(inputVec);

		        // propagate the data through the remaining layers
		        for (int i = 1; i <= mNumLayers; i++)	// use <= to include the output layer
		        {                    
                    // get the weighted connections linking the next layer
                    connect = mLayers[i];

                    // apply the weighted connections
                    outputVec = new List<double>();
                    connect.SetInputs(inputVec);
			        connect.GetOutputs(outputVec);
                    inputVec = new List<double>();

                    // store the output vector - this contains the unit input values
                    mUnitInputs.Add(outputVec);

			        if (i < mNumLayers)
			        {
				        // set the unit type, slope and amplification for the next hidden layer
				        unit.ActivationType = mActiveUnits[i];
				        unit.Slope = mActiveSlope[i];
				        unit.Amplify = mActiveAmplify[i];
			        }
			        else
			        {
				        // set the unit type, slope and amplification for the output layer
				        unit.ActivationType = mOutUnitType;
				        unit.Slope = mOutUnitSlope;
				        unit.Amplify = mOutUnitAmplify;
			        }

			        // activate the net units
			        for (int j = 0; j<(int)outputVec.Count; j++)
			        {
				        unit.Input = outputVec[j];
				        inputVec.Add(unit.GetActivation());
			        }

			        // store the activations
			        mActivations.Add(inputVec);
		        }
	
		        // copy the results into the output vector
		        outputs.Clear();
		        outputs.AddRange(inputVec);
	        }
        }

        /// <summary>
        /// Gets the activation values for a specified layer.
        /// 
        /// This method is typically called by the training process to access
        /// the activation values of the hidden and output layers.
        /// </summary>
        /// <param name="activations">the activation values for the layer</param>
        /// <param name="layer">the specified layer</param>
        /// 
        public void GetActivations(List<double> activations, int layer)
        {
            if (layer >= 0 && layer < mActivations.Count)
            {
                activations.Clear();
                activations.AddRange(mActivations[layer]);
            }
        }

        /// <summary>
        /// Gets the unit input values for a specified layer.
        /// 
        /// This method is typically called by the training process to access
        /// the input values to the hidden and output layer activation functions.
        /// </summary>
        /// <param name="inputs">the unit input values for the layer</param>
        /// <param name="layer">the specified layer</param>
        /// 
        public void GetUnitInputs(List<double> inputs, int layer)
        {
            if (layer >= 0 && layer < mUnitInputs.Count)
            {
                inputs.Clear();
                inputs.AddRange(mUnitInputs[layer]);
            }
        }

        /// <summary>
        /// Gets the weighted connections for a specified layer.
        /// 
        /// This method is typically called by the training process to access the weighted connections.
        /// </summary>
        /// <param name="layer">the specified layer</param>
        /// <returns>the weighted connections between the specified layer and the next sequential layer in the network</returns>
        /// 
        public NNetWeightedConnect GetWeightedConnect(int layer)
        {
            NNetWeightedConnect wtConnect = null;

            if (layer >= 0 && layer < mLayers.Count)
            {
                wtConnect = new NNetWeightedConnect(mLayers[layer]);
            }

            return wtConnect;
        }

        /// <summary>
        /// Sets the weighted connections for a specified layer.
        /// 
        /// This method is typically called by the training process to update the weighted connections.
        /// </summary>
        /// <param name="wtConnect">the weighted connections between the specified layer and the next sequential layer in the network</param>
        /// <param name="layer">the specified layer</param>
        /// 
        public void SetWeightedConnect(NNetWeightedConnect wtConnect, int layer)
        {
	        if (layer >= 0 && layer < mLayers.Count)
	        {
		        mLayers[layer] = wtConnect;
	        }
        }

        /// <summary>
        /// Serializes this network and writes it to a file.
        /// </summary>
        /// <param name="fname">the file to write the data to</param>
        /// <returns>0 if successful otherwise -1</returns>
        /// 
        public int WriteToFile(string fname)
        {
            StreamWriter ofstream = new StreamWriter(fname);

            if (ofstream != null)
	        {
                ofstream.Write(Serialize());

                // tidy up
                ofstream.Close();
            }
	        else
	        {
		        return -1;
	        }

	        return 0;
        }

        /////////////////////////////////////////////////////////////////////
        // Private Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Generates a string representation of this network.
        /// </summary>
        /// <returns>a string representation of this network</returns>
        /// 
        private string Serialize()
        {
            List<double> weights = new List<double>();
            StringBuilder outStr = new StringBuilder();

            // serialize the main details
            int outUnitType = (int)mOutUnitType;    // cast the output unit type enum to an int

            outStr.Append(mNumInputs.ToString());
            outStr.Append(" ");
            outStr.Append(mNumOutputs.ToString());
            outStr.Append(" ");
            outStr.Append(mNumLayers.ToString());
            outStr.Append(" ");
            outStr.Append(outUnitType.ToString());
            outStr.Append(" ");
            outStr.Append(mOutUnitSlope.ToString());
            outStr.Append(" ");
            outStr.Append(mOutUnitAmplify.ToString());
            outStr.Append(" ");

            // serialize the layer data
            for (int i = 0; i <= mNumLayers; i++)       // use <= to include the output layer
            {
                NNetWeightedConnect connect = mLayers[i];
                int nIn = connect.NumInputNodes;
                int nOut = connect.NumOutputNodes;
                int nUnit = 0;
                double sUnit = 0.0, aUnit = 0.0;

                // get the unit type, slope and amplification for the hidden layer
                if (i < mNumLayers) nUnit = (int)mActiveUnits[i];
                if (i < mNumLayers) sUnit = mActiveSlope[i];
                if (i < mNumLayers) aUnit = mActiveAmplify[i];

                outStr.Append("L ");
                outStr.Append(nIn.ToString());
                outStr.Append(" ");
                outStr.Append(nOut.ToString());
                outStr.Append(" ");
                outStr.Append(nUnit.ToString());
                outStr.Append(" ");
                outStr.Append(sUnit.ToString());
                outStr.Append(" ");
                outStr.Append(aUnit.ToString());
                outStr.Append(" ");

                for (int j = 0; j < nOut; j++)
                {
                    connect.GetWeightVector(j, weights);

                    for (int k = 0; k < nIn; k++)
                    {
                        outStr.Append(weights[k].ToString("G16"));
                        outStr.Append(" ");
                    }
                }
            }

            // terminate the output string
            outStr.AppendLine();

            return outStr.ToString();
        }

        /// <summary>
        /// Reads a space separated string from the given stream.
        /// </summary>
        /// <param name="strStream">the given stream reader</param>
        /// <returns>the string read from the stream</returns>
        /// 
        private string ReadString(StreamReader strStream)
        {
            bool read = false;
            StringBuilder outStr = new StringBuilder();

            while (!read)
            {
                char[] c = new char[1];

                if (strStream.Peek() >= 0)
                {
                    strStream.Read(c, 0, c.Length);

                    string str = new string(c);

                    if (str == " ")
                    {
                        read = true;
                    }
                    else
                    {
                        outStr.Append(c, 0, c.Length);
                    }
                }
                else
                {
                    read = true;
                }
            }

            return outStr.ToString();
        }

        /// <summary>
        /// Instantiates this network from a string representation stored in a file.
        /// </summary>
        /// <param name="fname">the file containing a string representation of the network</param>
        /// 
        private void InitializeFromFile(string fname)
        {
            StreamReader inStream = new StreamReader(fname);

	        if (inStream != null)
	        {
		        int outUnitType;

                // deserialize the main details
                string sNumInputs = ReadString(inStream);
                string sNumOutputs = ReadString(inStream);
                string sNumLayers = ReadString(inStream);
                string sOutUnitType = ReadString(inStream);
                string sOutUnitSlope = ReadString(inStream);
                string sOutUnitAmplify = ReadString(inStream);

                mNumInputs = Convert.ToInt32(sNumInputs);
                mNumOutputs = Convert.ToInt32(sNumOutputs);
                mNumLayers = Convert.ToInt32(sNumLayers);
                outUnitType = Convert.ToInt32(sOutUnitType);
                mOutUnitSlope = Convert.ToDouble(sOutUnitSlope);
                mOutUnitAmplify = Convert.ToDouble(sOutUnitAmplify);

		        mOutUnitType = (ActiveT)outUnitType;

		        // deserialize the layer data
		        for (int i = 0; i <= mNumLayers; i++)		// use <= to include the output layer
		        {
                    string sDelim = ReadString(inStream);
                    string sNIn = ReadString(inStream);
                    string sNOut = ReadString(inStream);
                    string sNUnit = ReadString(inStream);
                    string sSUnit = ReadString(inStream);
                    string sAUnit = ReadString(inStream);

                    int nIn = Convert.ToInt32(sNIn);
                    int nOut = Convert.ToInt32(sNOut);
                    int nUnit = Convert.ToInt32(sNUnit);
                    double sUnit = Convert.ToDouble(sSUnit);
                    double aUnit = Convert.ToDouble(sAUnit);

			        NNetWeightedConnect connect = new NNetWeightedConnect(nIn, nOut);

			        for (int j = 0; j < nOut; j++)
			        {
				        List<double> weights = new List<double>();

				        for (int k = 0; k < nIn; k++)
				        {					        
                            string sWgt = ReadString(inStream);
                            double wgt = Convert.ToDouble(sWgt);

					        weights.Add(wgt);
				        }

                        connect.SetWeightVector(j, weights);
			        }

			        mLayers.Add(connect);
			        mActiveUnits.Add((ActiveT)nUnit);
			        mActiveSlope.Add(sUnit);
			        mActiveAmplify.Add(aUnit);
		        }

                // tidy up
                inStream.Close();
            }
        }
    }
}

/////////////////////////////////////////////////////////////////////
