/////////////////////////////////////////////////////////////////////
//
// Implements the NNetUnit class.
//
// Author: Jason Jenkins
//
// This class is used by the neural network class (NeuralNet) and
// represents the basic neural network unit or neuron.
//
// A unit or neuron can be assigned one of a number activation 
// functions from a selection of available types:
//
// Threshold, Unipolar, Bipolar, Tanh, Gaussian, Arctan, Sine,
// Cosine, Sinc, Elliot, Linear, ISRU, SoftSign and SoftPlus.
//
// The output of the activation function can also be modified using 
// two parameters: slope and amplify.
//
// The amplify parameter can be used to increase or decrease the
// activation value which alters the range of the function.
//
// The slope parameter, sometimes termed lamda, can be used to 
// adjust the sensitivity of the activation function and its 
// effect depends on the particular function - For the sigmoidal 
// functions: unipolar, bipolar, tanh, arctan, elliot and soft 
// sign increasing the value of the slope parameter will increase
// the slope of the curve at the origin:-
//
//           decreasing <--- slope ---> increasing
//               ---                       ---
//              /                          |
//             /                           | 
//          ---                          ---
//
// For the sigmoidal function ISRU (inverse square root unit)
// changing the value of the slope parameter has the opposite 
// effect on the slope of the curve at the origin. Changing  
// the slope also effects the range of the activation value:
// default range: -1 / sqrt(mSlope) to 1 / sqrt(mSlope)
//
// For the periodic functions: sine, cosine and sinc increasing the 
// slope value will decrease the spatial period of the function i.e.
// the distance along the x-axis between repeated values is reduced.
// Decreasing the slope value has the opposite effect.
//
// For the Gaussian function - with a symmetric 'bell curve' shape -
// the slope value effects the width of 'bell': increasing the slope
// parameter will decrease the width of the 'bell' and vice versa.
//
// The effect of the slope parameter on the linear and soft plus
// functions is to increase or decrease the steepness or gradient
// of the functions as the slope parameter increases or decreases.
//
// For the threshold function the slope parameter has a similar
// effect to the amplify parameter. The threshold function usually
// returns one of two values - 0 or 1 - which can be adjusted by
// using the slope (or amplify) parameter to return 0 or the value
// of the product of the slope and amplify parameters.
//
// In all cases except ISRU and threshold varying the slope 
// parameter does not affect the default range of the particular 
// activation function - to alter the range of these functions you 
// need to vary the amplify parameter.
//
/////////////////////////////////////////////////////////////////////

using System;

/////////////////////////////////////////////////////////////////////

namespace NeuralNetwork.NetUnit
{
    /// <summary>
    /// The available neural network unit activation functions as an 
    /// enumerated type.
    /// </summary>
    /// 
    public enum ActiveT
    {
        /// <summary>Threshold activation function.</summary>
        kThreshold,
        /// <summary>Unipolar activation function.</summary>
        kUnipolar,
        /// <summary>Bipolar activation function.</summary>
        kBipolar,
        /// <summary>Hyperbolic tangent activation function.</summary>
        kTanh,
        /// <summary>Gaussian activation function.</summary>
        kGauss,
        /// <summary>Inverse tangent activation function.</summary>
        kArctan,
        /// <summary>Sine activation function.</summary>
        kSin,
        /// <summary>Cosine activation function.</summary>
        kCos,
        /// <summary>Sinc activation function.</summary>
        kSinC,
        /// <summary>Elliot activation function.</summary>
        kElliot,
        /// <summary>Linear activation function.</summary>
        kLinear,
        /// <summary>Inverse square root unit activation function.</summary>
        kISRU,
        /// <summary>SoftSign activation function.</summary>
        kSoftSign,
        /// <summary>SoftPlus activation function.</summary>
        kSoftPlus
    }

    /// <summary>
    /// This class is used by the neural network class (NeuralNet) and
    /// represents the basic neural network unit or neuron.
    /// 
    /// A unit or neuron can be assigned one of a number of activation
    /// functions from a selection of available types:
    ///
    /// Threshold; Unipolar; Bipolar; Tanh; Gaussian; Arctan; Sine;
    /// Cosine; Sinc; Elliot; Linear; ISRU; SoftSign and SoftPlus.
    ///
    /// The output of the activation function can also be modified using 
    /// two properties: slope and amplify.
    /// </summary>
    /// 
    public class NNetUnit
    {
        /////////////////////////////////////////////////////////////////////
        // Private Data Members
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// the unit input value
        /// </summary>
        private double mInput = -1;

        /// <summary>
        /// the activation function slope setting 
        /// </summary>
        private double mSlope = 1;

        /// <summary>
        /// the activation function amplify setting
        /// </summary>
        private double mAmplify = 1;

        /// <summary>
        /// the unit activation function type
        /// </summary>
        private ActiveT mActivationType = ActiveT.kThreshold;

        /////////////////////////////////////////////////////////////////////
        // Constructors
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// 
        public NNetUnit() {}

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="original">the NNetUnit object to be copied</param>
        /// 
        public NNetUnit(NNetUnit original)
        {
            mInput = original.mInput;
            mSlope = original.mSlope;
            mAmplify = original.mAmplify;
            mActivationType = original.mActivationType;
        }

        /// <summary>
        /// Constructs a neuron with the given activation function and settings.
        /// </summary>
        /// <param name="activationMode">the unit activation function type</param>
        /// <param name="slope">the slope parameter value</param>
        /// <param name="amplify">the amplify parameter value</param>
        /// 
        public NNetUnit(ActiveT activationMode, double slope, double amplify)
        {
	        mActivationType = activationMode;

	        // ignore invalid values
	        if (slope > 0)
	        {
		        mSlope = slope;
	        }

	        // ignore invalid values
	        if(amplify > 0)
	        {
		        mAmplify = amplify;
	        }
        }

        /////////////////////////////////////////////////////////////////////
        // Properties
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The unit input value.
        /// </summary>
        /// 
        public double Input
        {
            set
            {
                mInput = value;
            }
        }

        /// <summary>
        /// The unit activation function type.
        /// </summary>
        /// 
        public ActiveT ActivationType
        {
            set
            {
                mActivationType = value;
            }
        }

        /// <summary>
        /// The slope value - this property can be used to 
        /// adjust the sensitivity of the activation function.
        /// </summary>
        /// 
        public double Slope
        {
            set
            {
                // ignore invalid values
                if (value > 0)
                {
                    mSlope = value;
                }
            }
        }

        /// <summary>
        /// The amplify value - this property can be used to increase 
        /// (value greater than 1) or decrease (value less than 1) the 
        /// activation function value - this alters the range of the 
        /// activation function.
        /// </summary>
        /// 
        public double Amplify
        {
            set
            {
                // ignore invalid values
                if (value > 0)
                {
                    mAmplify = value;
                }
            }
        }

        /////////////////////////////////////////////////////////////////////
        // Public Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets the activation function value of the neuron.
        /// </summary>
        /// <returns>the activation value</returns>
        /// 
        public double GetActivation()
        {
            double activation = 0;

            switch (mActivationType)
            {
                case ActiveT.kThreshold:    // default range: 0 OR mSlope
                                            // amplified range: 0 OR (mSlope * mAmplify)
                    if (mInput >= 0)
                    {
                        activation = 1 * mSlope;
                    }
                    break;

                case ActiveT.kUnipolar:     // default range: 0 to 1
                                            // amplified range: 0 to mAmplify

                    activation = 1.0 / (1.0 + Math.Exp(-mSlope * mInput));
                    break;

                case ActiveT.kBipolar:      // default range: -1 to 1
                                            // amplified range: -mAmplify to mAmplify

                    activation = (2.0 / (1.0 + Math.Exp(-mSlope * mInput))) - 1;
                    break;

                case ActiveT.kTanh:         // default range: -1 to 1
                                            // amplified range: -mAmplify to mAmplify

                    activation = Math.Tanh(mSlope * mInput);
                    break;

                case ActiveT.kGauss:        // default range: 0 to 1
                                            // amplified range: 0 to mAmplify

                    activation = Math.Exp(-mSlope * mInput * mInput);
                    break;

                case ActiveT.kArctan:       // default range: -pi/2 to +pi/2
                                            // amplified range: -(pi/2) * mAmplify to +(pi/2) * mAmplify

                    activation = Math.Atan(mSlope * mInput);
                    break;

                case ActiveT.kSin:          // default range: -1 to 1
                                            // amplified range: -mAmplify to mAmplify

                    activation = Math.Sin(mSlope * mInput);
                    break;

                case ActiveT.kCos:          // default range: -1 to 1
                                            // amplified range: -mAmplify to +mAmplify

                    activation = Math.Cos(mSlope * mInput);
                    break;

                case ActiveT.kSinC:         // default range: ~ -0.217234 to 1
                                            // amplified range: ~ -(mAmplify * 0.217234) to mAmplify

                    if (Math.Abs(mInput) < 0.00001)
                    {
                        activation = 1.0;
                    }
                    else
                    {
                        activation = Math.Sin(mSlope * mInput) / (mSlope * mInput);
                    }

                    break;

                case ActiveT.kElliot:       // default range: 0 to 1
                                            // amplified range: 0 to mAmplify

                    activation = ((mSlope * mInput) / 2) / (1 + Math.Abs(mSlope * mInput)) + 0.5;
                    break;

                case ActiveT.kLinear:       // range: -infinity to +infinity

                    activation = mSlope * mInput;
                    break;

                case ActiveT.kISRU:         // default range: -1 / sqrt(mSlope) to 1 / sqrt(mSlope)
                                            // amplified range: -(mAmplify / sqrt(mSlope)) to +(mAmplify / sqrt(mSlope))

                    activation = mInput / Math.Sqrt(1 + mSlope * mInput * mInput);
                    break;

                case ActiveT.kSoftSign:     // default range: -1 to 1
                                            // amplified range: -mAmplify to mAmplify

                    activation = (mSlope * mInput) / (1 + Math.Abs(mSlope * mInput));
                    break;

                case ActiveT.kSoftPlus:     // range: 0 to +infinity

                    activation = Math.Log(1 + Math.Exp(mSlope * mInput));
                    break;
            }

            // the activation value is increased if amplify > 1 or reduced if amplify < 1
            return mAmplify * activation;
        }
    }
}

/////////////////////////////////////////////////////////////////////
