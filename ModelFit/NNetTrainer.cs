/////////////////////////////////////////////////////////////////////
//
// Implements the NNetTrainer class
//
// Author: Jason Jenkins
//
// This class provides a framework for training a neural network.
//
// Once a network has been built it can then be trained using a
// suitable set of input and output values - known as the training 
// set.  Typically this set of values is quite large and for each 
// input element there is a corresponding output or target element.
// The training set input values are the inputs into the neural 
// network and the corresponding target values represents the 
// output that you would like the network to reproduce in response 
// to the given input.
// 
// As an example: if the network has three input units and two output
// units then each input element of the training set will have three 
// values (stored in a three element list) and each target element of 
// the training set will have two values (stored in a two element 
// list) - these input and output elements are equivalent to 
// mathematical vectors and this term is usually used to describe
// them.
//
// The calling process of this trainer must first add a suitable 
// training set consisting of the input and output values using the 
// AddNewTrainingSet method. The training parameters - the learning 
// constant and the momentum - can then be optionally set via the 
// LearningConstant and Momentum properties respectively. If these
// properties are not set then default values will be used instead. 
// The training routine can then be called by passing the neural 
// network that has been built with the required architecture to the
// TrainNeuralNet method which will carry out the training. The 
// following code outlines this process:
/*
    // create the trainer
    NNetTrainer trainer = new NNetTrainer();

    // the training set needs to be stored as follows
    List<List<double>> inputVectors = new List<List<double>>();
    List<List<double>> targetVectors = new List<List<double>>();

    // a routine to populate the training set is required ...

    // initialize the trainer
    trainer.AddNewTrainingSet(inputVectors, targetVectors);
    trainer.LearningConstant = 0.05;
    trainer.Momentum = 0.25;	

    // train the network - here Net is the prior built network
    trainer.TrainNeuralNet(Net);

    // inspect the total network error
    double netError = trainer.NetError;

    // if more training is required then reset the network error
    trainer.ResetNetError();

    // and call the training routine again
    trainer.TrainNeuralNet(net);
	netError = trainer.NetError;

	// and repeat until training is complete
*/
//
// The training routine feeds the first input vector of the training
// set into the network. The output of the network - known as the 
// response - is calculated and compared to the ideal output in the 
// corresponding target vector of the training set. The difference 
// between the response and target values - known as error - is 
// calculated. The error value is then fed back through the network 
// using a procedure called Backpropagation - the Wikipedia entry 
// for this process can be found here:
//
//         https://en.wikipedia.org/wiki/Backpropagation. 
//
// The weighted connections linking the layers of the network 
// together (initially set to random values) are adjusted by this
// process using the gradient descent method to minimize the error. 
// The next input element of the training set is then fed into the 
// network and the response and associated error are calculated 
// again and used to further adjust the weighted connections. This
// process is continued until all the training set has been fed into
// the network and the errors occurring at each step are totalled
// together to give the total network error.
//
// The calling process of the trainer can then interrogate this 
// value and if the total network error is found to be less than a
// given predetermined value the training is deemed complete. But if
// the total error is still above the predetermined value the network
// error can be reset to zero and the training process can be called
// again and again until the total error has reached the desired 
// level or a set number of iterations has been exceeded.
//
/////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

/////////////////////////////////////////////////////////////////////

using NeuralNetwork.NetUnit;
using NeuralNetwork.WeightConnect;

/////////////////////////////////////////////////////////////////////

namespace NeuralNetwork.NetTrainer
{
    /// <summary>
    /// This class provides a framework for training a neural network.
    /// 
    /// Once a network has been built it can then be trained using a
    /// suitable set of input and output elements - known as the training 
    /// set. Typically this set of elements is quite large and for each 
    /// input element - comprising of one or more values (usually called
    /// a vector) there is a corresponding output element or target 
    /// vector - which again can comprise of one or more values. The 
    /// training set input vectors are the inputs into the neural 
    /// network and the corresponding target vectors represents the 
    /// output that you would like the network to reproduce in 
    /// response to the given input.
    /// 
    /// </summary>
    /// 
    public class NNetTrainer
    {
        /////////////////////////////////////////////////////////////////////
        // Private Data Members
        /////////////////////////////////////////////////////////////////////

        /// <summary>the network error</summary>
        private double mNetError = 0;

        /// <summary>the learning constant</summary>
        private double mLearnConst = 0.5;

        /// <summary>the momentum parameter</summary>
        private double mMomentum = 0;

        /// <summary>keeps track of the output layer weightings for use by the momentum term</summary>
        private List<double> mPrevOutWt = new List<double>();

        /// <summary>keeps track of the hidden layer weightings for use by the momentum term</summary>
        private List<double> mPrevHidWt = new List<double>();

        /// <summary>the training set input values</summary>
        private List<List<double>> mTrainInput = new List<List<double>>();

        /// <summary>the training set target values</summary>
        private List<List<double>> mTrainTarget = new List<List<double>>();

        /////////////////////////////////////////////////////////////////////
        // Constructors
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// 
        public NNetTrainer() { }

        /////////////////////////////////////////////////////////////////////
        // Properties
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The network error.
        /// 
        /// This represents the total difference (or error) between the
        /// training set target vectors (the desired output of the network)
        /// and actual network output in response to the input vectors in
        /// the training set.
        /// </summary>
        /// 
        public double NetError
        {
            get
            {
                return mNetError;
            }
        }

        /// <summary>
        /// The learning constant training parameter.
        /// 
        /// The learning constant governs the 'size' of the steps taken down
        /// the error surface. Larger values decrease training time but can
        /// lead to the system overshooting the minimum value.
        /// </summary>
        /// 
        public double LearningConstant
        {
            get
            {
                return mLearnConst;
            }
            set
            {
                // ignore invalid values
                if (value > 0)
                {
                    mLearnConst = value;
                }
            }
        }

        /// <summary>
        /// The momentum training parameter.
        /// 
        /// This property can be used to weight the search of the error surface
        /// to continue along the same 'direction' as the previous step.
        /// 
        /// A value of 1 will add 100% of the previous weighted connection
        /// value to the next weighted connection adjustment.  If set to 
        /// zero (the default value) the next step of the search will always
        /// proceed down the steepest path of the error surface.
        /// </summary>
        /// 
        public double Momentum
        {
            get
            {
                return mMomentum;
            }
            set
            {
                // ignore invalid values
                if (value > 0)
                {
                    mMomentum = value;
                }
            }
        }

        /////////////////////////////////////////////////////////////////////
        // Public Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Resets the total network error to zero.
        /// </summary>
        /// 
        public void ResetNetError()
        {
            mNetError = 0;
        }

        /// <summary>
        /// Trains the supplied neural network.
        /// 
        /// Each time this method is called the order of the training set
        /// elements are randomly shuffled to try and avoid any potential
        /// bias toward certain patterns that may occur if the data
        /// were always presented to the trainer in the same order.
        /// </summary>
        /// <param name="nNet">the neural network to be trained</param>
        /// 
        public void TrainNeuralNet(NeuralNet nNet)
        {
            int nTrain = mTrainInput.Count;

            List<int> idx = new List<int>();

            // populate the index list for the training set
            for (int i = 0; i < nTrain; i++)
            {
                idx.Add(i);                
            }

            if (nTrain > 0)
            {
                // randomly shuffle the index list
                Random rnd = new Random();
                idx = idx.OrderBy(x => rnd.Next()).ToList();

                for (int i = 0; i < nTrain; i++)
                {
                    int index = idx[i];
                    List<double> outVec = new List<double>();                   // the network output values
                    List<double> outErrSig = new List<double>();                // the output layer errors
                    List<List<double>> hidErrSig = new List<List<double>>();    // the hidden layer errors

                    // get the next input values vector from the training set
                    List<double> trainVec = mTrainInput[index];

                    // calculate the response from the training set input vector
                    nNet.GetResponse(trainVec, outVec);

                    // calculate the total network error
                    mNetError += CalcNetworkError(outVec, index);

                    // calculate the error signal on each output unit
                    CalcOutputError(nNet, outErrSig, outVec, index);

                    // calculate the error signal on each hidden unit
                    CalcHiddenError(hidErrSig, outErrSig, nNet);

                    // calculate the weight adjustments for the connections into the output layer
                    CalcOutputWtAdjust(outErrSig, nNet);

                    // calculate the weight adjustments for the connections into the hidden layers
                    CalcHiddenWtAdjust(hidErrSig, trainVec, nNet);
                }
            }
        }

        /// <summary>
        /// Adds an individual input vector (stored in a list) and the corresponding
        /// target vector to the training set.
        /// </summary>
        /// <param name="inVec">the input vector values</param>
        /// <param name="outVec">the corresponding target vector values</param>
        /// 
        public void AddToTrainingSet(List<double> inVec, List<double> outVec)
        {
	        mTrainInput.Add(inVec);
	        mTrainTarget.Add(outVec);
        }

        /// <summary>
        /// Adds a complete training set of input and corresponding target vectors to the trainer.
        /// 
        /// A single input element of the training set consists of a vector of
        /// values (stored in a list) hence the complete set of input values 
        /// consists of a list of lists. The complete target set is similarly defined.
        /// </summary>
        /// <param name="inVecs">a list of input vector values</param>
        /// <param name="outVecs">a list of corresponding target vector values</param>
        /// 
        public void AddNewTrainingSet(List<List<double>> inVecs, List<List<double>> outVecs)
        {
            mTrainInput.Clear();
            mTrainTarget.Clear();

            mTrainInput.AddRange(inVecs);
            mTrainTarget.AddRange(outVecs);
        }

        /////////////////////////////////////////////////////////////////////
        // Private Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Calculates the network error between a given vector of response 
        /// values and the corresponding vector of target values.
        /// </summary>
        /// <param name="response">the network response values</param>
        /// <param name="nTarget">the index of the corresponding target values in the training set</param>
        /// <returns>the current network error value</returns>
        /// 
        private double CalcNetworkError(List<double> response, int nTarget)
        {
            double error = 0;
            List<double> targetVec = mTrainTarget[nTarget];

	        for (int i = 0; i<(int)response.Count; i++)
	        {
		        error += 0.5 * Math.Pow((targetVec[i] - response[i]), 2);
	        }

	        return error;
        }

        /// <summary>
        /// Calculates the error signal on each individual unit in the output layer.
        /// 
        /// Uses the gradient descent method to search the error surface.
        /// </summary>
        /// <param name="nNet">the network undergoing training</param>
        /// <param name="outErr">the calculated output unit errors</param>
        /// <param name="response">the network response values</param>
        /// <param name="nTarget">the index of the corresponding target values in the training set</param>
        /// 
        private void CalcOutputError(NeuralNet nNet, List<double> outErr,
                                     List<double> response, int nTarget)
        {
            List<double> unitInputs = new List<double>();
            List<double> targetVec = mTrainTarget[nTarget];

            // get the output layer activation unit details
            ActiveT outType = nNet.OutputUnitType;
            double outSlope = nNet.OutputUnitSlope;
            double outAmplify = nNet.OutputUnitAmplify;

            // get the output layer activation unit input values
            nNet.GetUnitInputs(unitInputs, nNet.NumLayers);

	        for (int i = 0; i < response.Count; i++)
	        {
		        double yi = response[i];
                double xi = unitInputs[i];

                // follow the steepest path on the error function by moving along the gradient
                // of the output units activation function - the gradient descent method
                double err = (targetVec[i] - yi) * GetGradient(outType, outSlope, outAmplify, xi);

                outErr.Add(err);
	        }
        }

        /// <summary>
        /// Calculates the error signal on each individual unit within the networks hidden layers.
        /// 
        /// Uses the gradient descent method to search the error surface.
        /// </summary>
        /// <param name="hidErr">the calculated hidden unit errors</param>
        /// <param name="outErr">the output unit errors</param>
        /// <param name="nNet">the network undergoing training</param>
        /// 
        private void CalcHiddenError(List<List<double>> hidErr,
                                     List<double> outErr, NeuralNet nNet)
        {
            int nHidden = nNet.NumLayers;
            double slope = 1, amplify = 1;            
            ActiveT unitType = ActiveT.kUnipolar;

            // initialise the the previous layer error with the output layer errors
            List<double> prevErr = new List<double>();
            prevErr.AddRange(outErr);

	        // start with the last hidden layer and work back to the first
	        for (int i = nHidden; i >= 1; i--)
	        {
                List<double> unitInputs = new List<double>();
                List<double> layerErr = new List<double>();

                // get the weighted connections for the current hidden layer
                NNetWeightedConnect wtConnect = nNet.GetWeightedConnect(i);

		        int nUnits = wtConnect.NumInputNodes;
                int nConnect = wtConnect.NumOutputNodes;

                // get the hidden layer activation unit details
                nNet.GetLayerDetails(i - 1, ref unitType, ref slope, ref amplify);

		        // get the hidden layer activation unit input values
		        nNet.GetUnitInputs(unitInputs, i - 1);

		        // calculate the hidden layer errors
		        for (int j = 0; j < nUnits; j++)
		        {
			        double error = 0;
                    double xj = unitInputs[j];
			
			        for (int k = 0; k < nConnect; k++)
			        {
				        List<double> weights = new List<double>();
                        wtConnect.GetWeightVector(k, weights);

				        // follow the steepest path on the error function by moving along the gradient
				        // of the hidden layer units activation function - the gradient descent method
				        error += GetGradient(unitType, slope, amplify, xj) * prevErr[k] * weights[j];
			        }

                    layerErr.Add(error);
		        }

                // update the hidden errors with the current layer error
                // N.B. Since we start from the last hidden layer the 
                // hidden layer error signals are stored in reverse order
                hidErr.Add(layerErr);

		        // back propagate the layer errors
		        prevErr.Clear();
		        prevErr = layerErr;
	        }
        }

        /// <summary>
        /// Calculates the weight adjustments for the connections into the output layer.
        /// </summary>
        /// <param name="outErr">the output unit errors</param>
        /// <param name="nNet">the network undergoing training</param>
        /// 
        private void CalcOutputWtAdjust(List<double> outErr, NeuralNet nNet)
        {
            int n = nNet.NumLayers, prevIdx = 0;
            List<double> xVec = new List<double>();            

            // get the weighted connections between the last hidden layer and the output layer
            NNetWeightedConnect wtConnect = nNet.GetWeightedConnect(n);
	
	        // get the input values for the weighted connections
	        nNet.GetActivations(xVec, n - 1);

	        int nOut = wtConnect.NumOutputNodes;

	        // calculate the weight adjustments for each weighted connection output unit
	        for (int i = 0; i < nOut; i++)
	        {
		        double ei = outErr[i];
                List<double> weights = new List<double>();

                // get the output units weight vector
                wtConnect.GetWeightVector(i, weights);

		        // calculate the total weight adjustment
		        for (int j = 0; j < xVec.Count; j++)
		        {
                    // the weight adjustment calculation
                    double dW = mLearnConst * ei * xVec[j];

			        // if the momentum term is greater than 0
			        // the previous weighting needs to be taken into account
			        if (mMomentum > 0)
			        {
				        if (mPrevOutWt.Count > prevIdx)
				        {
					        double dWPrev = mPrevOutWt[prevIdx];
					
					        // include a percentage of the previous weighting
					        dW += mMomentum* dWPrev;

                            // store the weighting 
                            mPrevOutWt[prevIdx] = dW;
				        }
				        else
				        {
					        // store the first weighting
					        mPrevOutWt.Add(dW);
				        }
			        }

			        // the total weight adjustment
			        weights[j] += dW;
			        prevIdx++;
		        }

		        wtConnect.SetWeightVector(i, weights);
	        }

	        nNet.SetWeightedConnect(wtConnect, n);
        }

        /// <summary>
        /// Calculates the weight adjustments for the connections into the hidden layers.
        /// </summary>
        /// <param name="hidErrSig">the hidden unit errors</param>
        /// <param name="inputVec">the current training set input values</param>
        /// <param name="nNet">the network undergoing training</param>
        /// 
        private void CalcHiddenWtAdjust(List<List<double>> hidErrSig,
                                        List<double> inputVec, NeuralNet nNet)
        {
            List<double> xVec = new List<double>();
            int maxHidLayIdx = nNet.NumLayers - 1, prevIdx = 0;

	        // calculate the weight adjustments for the hidden layers
	        for (int n = maxHidLayIdx; n >= 0; n--)
	        {
		        // get the weighted connections between the current layer and the previous hidden layer
		        NNetWeightedConnect wtConnect = nNet.GetWeightedConnect(n);

                // get the hidden unit errors for the previous hidden layer
                // N.B. the hidden error signals are stored in reverse order
                List<double> outErr = hidErrSig[maxHidLayIdx - n];

		        if (n == 0)
		        {
			        // we are dealing with the input layer
			        xVec = inputVec;
		        }
		        else
		        {
			        // we are dealing with a hidden layer
			        nNet.GetActivations(xVec, n - 1);
		        }

                int nOut = wtConnect.NumOutputNodes;

		        // calculate the weight adjustments for each weighted connection output unit
		        for (int i = 0; i < nOut; i++)
		        {
			        double ei = outErr[i];
                    List<double> weights = new List<double>();

                    // get the output units weight vector
                    wtConnect.GetWeightVector(i, weights);

			        // calculate the total weight adjustment
			        for (int j = 0; j < xVec.Count; j++)
			        {
                        // the weight adjustment calculation
                        double dW = mLearnConst * ei * xVec[j];

				        // if the momentum term is greater than 0
				        // the previous weighting needs to be taken into account
				        if (mMomentum > 0)
				        {
					        if (mPrevHidWt.Count > prevIdx)
					        {
						        double dWPrev = mPrevHidWt[prevIdx];

						        // include a percentage of the previous weighting
						        dW += mMomentum* dWPrev;

                                // store the weighting 
                                mPrevHidWt[prevIdx] = dW;
					        }
					        else
					        {
						        // store the first weighting
						        mPrevHidWt.Add(dW);
					        }
				        }

				        // the total weight adjustment
				        weights[j] += dW;
				        prevIdx++;
			        }

			        wtConnect.SetWeightVector(i, weights);
		        }

		        nNet.SetWeightedConnect(wtConnect, n);
	        }
        }

        /// <summary>
        /// Gets the gradient of the activation function at the given value of x.
        /// </summary>
        /// <param name="unitType">the activation function type</param>
        /// <param name="slope">the activation function slope value</param>
        /// <param name="amplify">the activation function amplify value</param>
        /// <param name="x">calculates the gradient at this value</param>
        /// <returns>the gradient of the activation function at the given value</returns>
        /// 
        private double GetGradient(ActiveT unitType, double slope, double amplify, double x)
        {
            double gradient = 0;
            double expMX, expMX1, tanMX, absMX1, grad;

            switch (unitType)
            {
                // 0 everywhere except the origin where the derivative is undefined!
                case ActiveT.kThreshold:

                    // return the value of the slope parameter if x = 0
                    if (x == 0) gradient = slope;
                    break;

                case ActiveT.kUnipolar:

                    expMX = Math.Exp(-slope * x);
                    expMX1 = 1 + expMX;

                    gradient = (slope * expMX) / (expMX1 * expMX1);
                    break;

                case ActiveT.kBipolar:

                    expMX = Math.Exp(-slope * x);
                    expMX1 = 1 + expMX;

                    gradient = (2 * slope * expMX) / (expMX1 * expMX1);
                    break;

                case ActiveT.kTanh:

                    tanMX = Math.Tanh(slope * x);

                    gradient = slope * (1 - (tanMX * tanMX));
                    break;

                case ActiveT.kGauss:

                    gradient = -2 * slope * x * Math.Exp(-slope * x * x);
                    break;

                case ActiveT.kArctan:

                    gradient = slope / (1 + slope * slope * x * x);
                    break;

                case ActiveT.kSin:

                    gradient = slope * Math.Cos(slope * x);
                    break;

                case ActiveT.kCos:

                    gradient = -slope * Math.Sin(slope * x);
                    break;

                case ActiveT.kSinC:

                    if (Math.Abs(x) < 0.00001)
                    {
                        gradient = 0;
                    }
                    else
                    {
                        gradient = (slope * x * Math.Cos(slope * x) - Math.Sin(slope * x)) / (slope * x * x);
                    }

                    break;

                case ActiveT.kElliot:

                    absMX1 = 1 + Math.Abs(slope * x);

                    gradient = (0.5 * slope) / (absMX1 * absMX1);
                    break;

                case ActiveT.kLinear:

                    gradient = slope;
                    break;

                case ActiveT.kISRU:

                    grad = 1 / Math.Sqrt(1 + slope * x * x);

                    gradient = grad * grad * grad;
                    break;

                case ActiveT.kSoftSign:

                    absMX1 = 1 + Math.Abs(slope * x);

                    gradient = slope / (absMX1 * absMX1);
                    break;

                case ActiveT.kSoftPlus:

                    expMX = Math.Exp(slope * x);

                    gradient = (slope * expMX) / (1 + expMX);
                    break;
            }

            return amplify * gradient;
        }
    }
}

/////////////////////////////////////////////////////////////////////
