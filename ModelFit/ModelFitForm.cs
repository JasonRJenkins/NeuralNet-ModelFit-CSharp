/////////////////////////////////////////////////////////////////////
//
// Implements the ModelFitForm class
//
// Author: Jason Jenkins
//
// This class implements the form based GUI used by the application.
//
/////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

/////////////////////////////////////////////////////////////////////

using NeuralNetwork;
using NeuralNetwork.NetUnit;
using NeuralNetwork.NetTrainer;

/////////////////////////////////////////////////////////////////////

namespace ModelFit
{
    /// <summary>
    /// This class implements the form based GUI used by the ModelFit 
    /// application.
    /// 
    /// The form allows the user to explore the various settings that 
    /// can be applied to a simple single hidden layer neural network 
    /// that can be used to model the potential relationship between 
    /// a single predictor variable (X) and a single corresponding 
    /// response variable (Y) chosen from a selected .CSV data file.
    ///
    /// The results of the model fit can be saved in both .CSV and .XLS
    /// format and they can also be viewed directly in Excel if desired. 
    /// </summary>
    public partial class ModelFitForm : Form
    {
        /////////////////////////////////////////////////////////////////////
        // Private Data Members
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// the neural network used to fit models to the data
        /// </summary>
        private NeuralNet mNet = new NeuralNet();

        /// <summary>
        /// used to store the data file as a table
        /// </summary>
        static private Data.Table.DataTable mDataTab = new Data.Table.DataTable();

        /// <summary>
        /// the training set input vectors
        /// </summary>
        private List<List<double>> mInputVecs = new List<List<double>>();

        /// <summary>
        /// the training set target vectors
        /// </summary>
        private List<List<double>> mTargetVecs = new List<List<double>>();

        /////////////////////////////////////////////////////////////////////
        // Properties
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The learning constant governs the 'size' of the steps taken down
        /// the error surface.
        ///
        /// Larger values decrease training time but can lead to the system 
        /// overshooting the minimum value.
        ///
        /// N.B. values between 0.001 and 10 have been reported as working 
        /// successfully.
        /// </summary>
        public double LearnConst
        {
            get { return (double)LearnConstNumUpDwn.Value; }
            set { LearnConstNumUpDwn.Value = (decimal)value; }
        }

        /// <summary>
        /// The momentum term can be used to weight the search of the error 
        /// surface to continue along the same 'direction' as the previous step.
        /// 
        /// A value of 1 will add 100% of the previous weighted connection
        /// value to the next weighted connection adjustment. If set to zero the
        /// next step of the search will always proceed down the steepest path
        /// of the error surface.
        /// </summary>
        public double Momentum
        {
            get { return (double)MomentNumUpDwn.Value; }
            set { MomentNumUpDwn.Value = (decimal)value; }
        }

        /// <summary>
        /// The output layer activation function type.
        /// 
        /// The available functions are: Threshold; Unipolar; Bipolar; Tanh; 
        /// Gaussian; Arctan; Sine; Cosine; Sinc; Elliot; Linear; ISRU; SoftSign
        /// and SoftPlus.
        /// </summary>
        public ActiveT OutputFunction
        {
            get { return (ActiveT)OutFuncListBox.SelectedIndex; }
            set { OutFuncListBox.SelectedIndex = (int)value; }
        }

        /// <summary>
        /// The hidden layer activation function type.
        /// 
        /// The available functions are: Threshold; Unipolar; Bipolar; Tanh; 
        /// Gaussian; Arctan; Sine; Cosine; Sinc; Elliot; Linear; ISRU; SoftSign
        /// and SoftPlus.
        /// </summary>
        public ActiveT HiddenFunction
        {
            get { return (ActiveT)HidFuncListBox.SelectedIndex; }
            set { HidFuncListBox.SelectedIndex = (int)value; }
        }

        /// <summary>
        /// The output layer activation function slope value.
        /// 
        /// This property can be used to adjust the sensitivity 
        /// of the output layer neurons activation function.
        /// </summary>
        public double OutputSlope
        {
            get { return (double)OutSlopeNumUpDwn.Value; }
            set { OutSlopeNumUpDwn.Value = (decimal)value; }
        }

        /// <summary>
        /// The hidden layer activation function slope value.
        /// 
        /// This property can be used to adjust the sensitivity 
        /// of the hidden layer neurons activation function.
        /// </summary>
        public double HiddenSlope
        {
            get { return (double)HidSlopeNumUpDwn.Value; }
            set { HidSlopeNumUpDwn.Value = (decimal)value; }
        }

        /// <summary>
        /// The output layer activation function amplify value.
        /// 
        /// This property can be used to boost or reduce the 
        /// output layer neurons signal.
        /// </summary>
        public double OutputAmplify
        {
            get { return (double)OutAmpNumUpDwn.Value; }
            set { OutAmpNumUpDwn.Value = (decimal)value; }
        }

        /// <summary>
        /// The hidden layer activation function amplify value.
        /// 
        /// This property can be used to boost or reduce the 
        /// hidden layer neurons signal.
        /// </summary>
        public double HiddenAmplify
        {
            get { return (double)HidAmpNumUpDwn.Value; }
            set { HidAmpNumUpDwn.Value = (decimal)value; }
        }

        /// <summary>
        /// The maximum number of iterations.
        /// 
        /// If a solution has not been found the search will terminate once 
        /// this value has been exceeded.
        /// </summary>
        public int NumIterations
        {
            get { return (int)NumIterNumUpDwn.Value; }
            set { NumIterNumUpDwn.Value = value; }
        }

        /// <summary>
        /// The convergence criteria - training will stop when the total
        /// network error is less than this value.
        /// 
        /// N.B. too low a value can lead to overtraining which can lock
        /// the network in a final state that is difficult to generalise from.
        /// </summary>
        public double MinNetError
        {
            get { return (double)MinNetErrNumUpDwn.Value; }
            set { MinNetErrNumUpDwn.Value = (decimal)value; }
        }

        /// <summary>
        /// This value is used to set the range of the initial random values
        /// for the weighted connections linking the layers of the neural net.
        /// 
        /// 1 represents the range -0.5 to +0.5; 2 represents -1 to +1; 
        /// 3 represents -1.5 to +1.5 and 4 represents -2 to +2 etc.
        /// N.B. randomising within the range -2 to +2 is usually sufficient.
        /// </summary>
        public double InitRange
        {
            get { return (double)InitRangeNumUpDwn.Value; }
            set { InitRangeNumUpDwn.Value = (decimal)value; }
        }

        /// <summary>
        /// The number of units in the hidden layer.
        /// </summary>
        public int NumberOfHiddenUnits
        {
            get { return (int)UnitsNumUpDwn.Value; }
            set { UnitsNumUpDwn.Value = value; }
        }

        /// <summary>
        /// Used to divide the data values to reduce their magnitude.
        /// 
        /// Scaling the magnitude of the data values so that they fall
        /// within the range: 0-1 can improve the model fit.
        /// </summary>
        private double ScaleFactor
        {
            get { return (double)ScaleNumUpDwn.Value; }
            set { ScaleNumUpDwn.Value = (decimal)value; }
        }

        /// <summary>
        /// The name of data file (including the full path).
        /// </summary>
        public string DataFile
        {
            get { return DataFileTBox.Text; }
            set { DataFileTBox.Text = value; }
        }

        /// <summary>
        /// Set to true if the data file has a header row
        /// </summary>
        public bool HasHeader
        {
            get { return HeaderCkBox.Checked; }
            set { HeaderCkBox.Checked = value; }
        }

        /// <summary>
        /// The data table column index of the selected predictor variable (X).
        /// </summary>
        private int PredictorIdx
        {
            get { return XlistBox.SelectedIndex; }
            set { XlistBox.SelectedIndex = value; }
        }

        /// <summary>
        /// The data table column index of the selected response variable (Y).
        /// </summary>
        private int ResponseIdx
        {
            get { return YlistBox.SelectedIndex; }
            set { YlistBox.SelectedIndex = value; }
        }

        /////////////////////////////////////////////////////////////////////
        // Constructor
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Default constructor
        /// </summary>
        public ModelFitForm()
        {
            InitializeComponent();

            // populates the form's Activation List boxes
            PopulateActivationListBoxes();
        }

        /////////////////////////////////////////////////////////////////////
        // Message Handlers
        /////////////////////////////////////////////////////////////////////

        private void FileBrowseBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // display the wait cursor
                    Cursor.Current = Cursors.WaitCursor;

                    // the full file name of specified data file
                    string fullFileName = openFileDialog.FileName;

                    // load the CSV file containing the training data
                    try
                    {
                        LoadData(fullFileName);

                        // when new data is aquired the model parameters must be set before we can fit a model
                        this.FitModelBtn.Enabled = false;
                        this.SaveToBtn.Enabled = false;
                        this.SaveNetworkBtn.Enabled = false;
                    }
                    catch (System.IO.IOException ioEx)
                    {
                        MessageBox.Show(ioEx.Message, "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    // reset the cursor
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /////////////////////////////////////////////////////////////////////

        private void HeaderCkBox_CheckedChanged(object sender, EventArgs e)
        {
            HasHeader = HeaderCkBox.Checked;

            mDataTab.HasHeader = HasHeader;

            // re-load the CSV file (if selected) to reflect the change in header status
            if (DataFile.Length > 0)
            {
                LoadData(DataFile);
                PopulateVariableListBoxes();
            }                        
        }

        /////////////////////////////////////////////////////////////////////

        private void FitModelBtn_Click(object sender, EventArgs e)
        {
            if (PredictorIdx != -1 && ResponseIdx != -1)
            {
                // display the wait cursor
                Cursor.Current = Cursors.WaitCursor;

                // disable the GUI buttons
                this.ExitBtn.Enabled = false;
                this.FitModelBtn.Enabled = false;
                this.FileBrowseBtn.Enabled = false;
                this.SaveToBtn.Enabled = false;
                this.SaveNetworkBtn.Enabled = false;

                // update the information text label and status bar
                this.InfoLabel.Text = "Training Started - Please wait...";
                this.PanelStatus.Text = "Status: Running...";
                this.PanelIterations.Text = "Iterations: 0";
                this.PanelNetError.Text = "Minimum Error:";
                Update();

                // fit the model
                int result = FitModel();

                // reset the cursor
                Cursor.Current = Cursors.Default;

                // update the information text label
                this.PanelStatus.Text = "Status: Idle";

                // always re-enable the exit and browse buttons
                this.ExitBtn.Enabled = true;
                this.FileBrowseBtn.Enabled = true;

                // only re-enable the rest of the controls if there is a valid result
                if (result == 0)
                {
                    this.InfoLabel.Text = "Training Complete!";
                    
                    // enable the GUI buttons
                    this.FitModelBtn.Enabled = true;
                    this.SaveToBtn.Enabled = true;
                    this.SaveNetworkBtn.Enabled = true;
                }
                else
                {
                    this.InfoLabel.Text = "Training Terminated!";
                    this.PanelIterations.Text = "";
                    this.PanelNetError.Text = "";
                }
            }
            else
            {
                // the GUI should prevent these situations - but just in case - handle them anyway
                if (PredictorIdx == -1 && ResponseIdx == -1)
                {
                    MessageBox.Show("You must select a predictor (X) and a response variable (Y).",
                                    "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (PredictorIdx == -1)
                {
                    MessageBox.Show("You have not selected a predictor variable (X).",
                                    "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("You have not selected a response variable (Y).",
                                    "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /////////////////////////////////////////////////////////////////////

        private void XlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PredictorIdx != -1 && ResponseIdx != -1)
            {
                // update the information text label
                this.InfoLabel.Text = "You can now fit the model";

                // enable the fit model button
                if (this.FitModelBtn.Enabled == false)
                {
                    this.FitModelBtn.Enabled = true;
                }
            }
        }

        /////////////////////////////////////////////////////////////////////

        private void SaveToBtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();                
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                // the default output file name
                string fname = DataFile.Substring(DataFile.LastIndexOf('\\') + 1);
                
                // determine the output type
                if (CSVRadioBtn.Checked)
                {
                    saveFileDialog.Filter = "csv files (*.csv)|*.csv";
                    fname = fname.Substring(0, fname.IndexOf('.')) + "_TrainedOutput.csv";
                }
                else if (ExcelRadioBtn.Checked)
                {
                    saveFileDialog.Filter = "xls files (*.xls)|*.xls";
                    fname = fname.Substring(0, fname.IndexOf('.')) + "_TrainedOutput.xls";
                }

                saveFileDialog.FileName = fname;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // the full name of the file to save the data to
                    string fullFileName = saveFileDialog.FileName;

                    // generate output of the appropriate type
                    try
                    {
                        if (CSVRadioBtn.Checked)
                        {
                            GenerateCSVOutput(mNet, fullFileName);
                        }
                        else if (ExcelRadioBtn.Checked)
                        {
                            GenerateExcelOutput(mNet, fullFileName);
                        }
                    }
                    catch (System.IO.IOException ioEx)
                    {
                        MessageBox.Show(ioEx.Message, "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////

        private void SaveNetworkBtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Filter = "net files (*.net)|*.net";

                // the default network file name
                string fname = DataFile.Substring(DataFile.LastIndexOf('\\') + 1);
                fname = fname.Substring(0, fname.IndexOf('.')) + "_TrainedNetwork.net";

                saveFileDialog.FileName = fname;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // the full name of the file to save the network to
                    string fullFileName = saveFileDialog.FileName;

                    // write the network to the file
                    try
                    {
                        mNet.WriteToFile(fullFileName);
                    }
                    catch (System.IO.IOException ioEx)
                    {
                        MessageBox.Show(ioEx.Message, "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
         
        /////////////////////////////////////////////////////////////////////

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        /////////////////////////////////////////////////////////////////////
        // Private Helper Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructs and trains a neural network, using the user selected
        /// settings, to fit a model to the selected dataset.
        /// 
        /// Training continues until the maximum number of iterations has 
        /// been exceeded or the total network error is less than the set
        /// minimum network error value.  In the former case the trained 
        /// neural network is the network that achieved the minimum network
        /// error during the training process.
        /// </summary>
        /// <returns>0 if successful otherwise -1</returns>        
        /// 
        private int FitModel()
        {
            int result = 0;
            bool converged = false;
            bool invalidResult = false;
            double netError = -1;
            double minErr = double.MaxValue;
            NeuralNet minNet = new NeuralNet();         // keeps track of the network with the minimum network error
            NNetTrainer trainer = new NNetTrainer();    // this object trains the neural net

            // populate the training set data
            PopulateTrainingSet();

            // initialize the trainer
            trainer.AddNewTrainingSet(mInputVecs, mTargetVecs);
            trainer.LearningConstant = LearnConst;
            trainer.Momentum = Momentum;

            // clear the neural network ready to fit the model data
            mNet.ClearNeuralNetwork();

            // initialize the network
            mNet.NumInputs = 1;                          // a single input value (the 'x-value')
            mNet.NumOutputs = 1;                         // a single output value (the 'y-value')
            mNet.OutputUnitType = OutputFunction;
            mNet.OutputUnitSlope = OutputSlope;
            mNet.OutputUnitAmplify = OutputAmplify;

            // use a fixed architecture of one hidden layer
            mNet.AddLayer(NumberOfHiddenUnits, HiddenFunction, InitRange, HiddenSlope, HiddenAmplify);

            // carry out the training
            for (int i = 1; i <= NumIterations; i++)
            {
                trainer.TrainNeuralNet(mNet);
                netError = trainer.NetError * ScaleFactor;

                // check for an invalid result from the network trainer
                if (double.IsNaN(netError) || double.IsInfinity(netError))
                {
                    MessageBox.Show("The network trainer has produced an invalid result:\nNetwork Error = " + netError.ToString() +
                                    "\nThe training process has been stopped.\nPlease adjust the model settings and try again.",
                                    "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    invalidResult = true;
                    result = -1;
                    break;
                }

                if (netError < MinNetError)
                {
                    // the solution has converged
                    MessageBox.Show("The solution has converged after " + i.ToString() + " iterations.",
                                    "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // update the status bar
                    this.PanelIterations.Text = "Iterations: " + i.ToString();
                    this.PanelNetError.Text = "Minimum Error: " + netError.ToString("G5");

                    converged = true;
                    break;
                }

                // keep track of the minimum error value
                if (netError < minErr)
                {
                    // copy the state of the neural net at the minimum error value
                    minNet.CopyNeuralNet(mNet);
                    minErr = netError;
                }

                // show the current progress
                if (i % 100 == 0)
                {
                    this.PanelIterations.Text = "Iterations: " + i.ToString();
                    this.PanelNetError.Text = "Network Error: " + netError.ToString("G5");
                    Update();
                }

                trainer.ResetNetError();
            }

            if (!converged && !invalidResult)
            {
                // the solution has not converged within the given number of iterations
                MessageBox.Show("The solution has not converged.\n" +
                                "The minimum error value was: " + minErr.ToString("G5") + "\n" +
                                "The neural network that achieved this minimum will be used to fit the model.",
                                "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // copy the net with settings at the minimum error value
                mNet.CopyNeuralNet(minNet);

                // update the status bar
                this.PanelIterations.Text = "Iterations: " + NumIterations.ToString();
                this.PanelNetError.Text = "Minimum Error: " + minErr.ToString("G5");
            }

            // show the output in excel - if requested
            if (ExcelOutputCkBox.Checked && !invalidResult)
            {
                ShowOutputInExcel(mNet);
            }

            return result;
        }

        /// <summary>
        /// Populates the training set input and target vectors.
        /// 
        /// The input and target vectors are extracted from the database
        /// table and stored within a list so that they can be used by the 
        /// neural network training routine. The values are also scaled -
        /// scaling the magnitude of the data values to fall within the
        /// range 0-1 can improve the model fit.
        /// </summary>
        /// 
        private void PopulateTrainingSet()
        {
            if (PredictorIdx != -1 && ResponseIdx != -1)
            {
                // we are only using single input and target values in this application but 
                // the neural net allows for multiple input and target values using a list
                // so the single x- and y-values are stored in a list with only one element
                List<double> dX = new List<double>();
                List<double> dY = new List<double>();

                // clear the training set data
                mInputVecs.Clear();
                mTargetVecs.Clear();

                // read the x-predictor and y-response values from the data table
                mDataTab.GetNumericCol(PredictorIdx, dX);
                mDataTab.GetNumericCol(ResponseIdx, dY);

                // populate the training set input and target vectors
                for (int i = 0; i < dX.Count; i++)
                {
                    List<double> iVec = new List<double>();
                    List<double> tVec = new List<double>();

                    // scale the training set input and output values
                    iVec.Add(dX[i] / ScaleFactor);      // training set input vector
                    tVec.Add(dY[i] / ScaleFactor);      // training set target vector

                    // the input and target vectors are also stored within a list
                    mInputVecs.Add(iVec);
                    mTargetVecs.Add(tVec);
                }
            }
        }

        /// <summary>
        /// Applies the trained neural network model to the selected predictor
        /// input data and outputs the results to a .csv file.
        /// 
        /// The output consists of 3 columns - the first contains the selected
        /// training set input (or predictor) values, the second the selected 
        /// training set target values and the third contains the trained model 
        /// output responses to the given input values.
        /// </summary>
        /// <param name="net">the trained neural network</param>
        /// <param name="fname">the name of the file to write the results to</param>
        /// 
        private void GenerateCSVOutput(NeuralNet net, string fname)
        {
            List<double> dX = new List<double>();
            List<double> dM = new List<double>();

            try
            {
                StreamWriter ofstream = new StreamWriter(fname);

                // output the column titles
                ofstream.WriteLine("input,target,model");

                for (int i = 0; i < mInputVecs.Count; i++)
                {
                    // calculate the model response value given the predictor value from the training set
                    dX = mInputVecs[i];
                    net.GetResponse(dX, dM);

                    // the required values are stored in vectors and need re-scaling
                    double xValue = dX[0] * ScaleFactor;
                    double yValue = mTargetVecs[i][0] * ScaleFactor;
                    double mValue = dM[0] * ScaleFactor;
                    
                    // write the results to the output file
                    ofstream.Write(xValue.ToString("G16"));
                    ofstream.Write(",");
                    ofstream.Write(yValue.ToString("G16"));
                    ofstream.Write(",");
                    ofstream.WriteLine(mValue.ToString("G16"));
                }

                ofstream.Close();
            }
            catch (System.IO.IOException ioEx)
            {
                MessageBox.Show(ioEx.Message, "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Applies the trained neural network model to the selected predictor
        /// input data and outputs the results to an Excel .xls file.
        /// 
        /// The output consists of 3 columns - the first contains the selected
        /// training set input (or predictor) values, the second the selected 
        /// training set target values and the third contains the trained model 
        /// output responses to the given input values. An XY-Scatter graph of 
        /// the data is also produced.
        /// </summary>
        /// <param name="net">the trained neural network</param>
        /// <param name="fname">the name of the file to write the results to</param>
        /// //
        private void GenerateExcelOutput(NeuralNet net, string fname)
        {
            List<double> dX = new List<double>();
            List<double> dM = new List<double>();

            // open Excel, add a workbook and obtain a worksheet
            Excel.Application xlApp = new Excel.Application();            
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            // stops Excel asking the user if they want to replace an existing file
            // the save file dialog will have already asked this question
            xlApp.DisplayAlerts = false;

            // add a header line
            xlWorkSheet.Cells[1, 1] = "";
            xlWorkSheet.Cells[1, 2] = "target";
            xlWorkSheet.Cells[1, 3] = "model";

            // add the model and training data 
            for (int i = 0; i < mInputVecs.Count; i++)
            {
                // calculate the model response value given the predictor value from the training set
                dX = mInputVecs[i];
                net.GetResponse(dX, dM);

                // the required values are stored in vectors and need re-scaling
                double xValue = dX[0] * ScaleFactor;
                double yValue = mTargetVecs[i][0] * ScaleFactor;
                double mValue = dM[0] * ScaleFactor;

                // write out the results
                xlWorkSheet.Cells[i + 2, 1] = xValue.ToString("G16");
                xlWorkSheet.Cells[i + 2, 2] = yValue.ToString("G16");
                xlWorkSheet.Cells[i + 2, 3] = mValue.ToString("G16");
            }

            // plot the data as an xy-scatter graph
            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject modelChart = (Excel.ChartObject)xlCharts.Add(200, 10, 450, 300);
            Excel.Chart chartPage = modelChart.Chart;

            chartRange = xlWorkSheet.get_Range("A1", "C" + (mInputVecs.Count + 1).ToString());
            chartPage.SetSourceData(chartRange);
            chartPage.ChartType = Excel.XlChartType.xlXYScatter;

            // set the axis labels
            List<string> colNames = new List<string>();
            mDataTab.GetColumnNames(colNames);

            Excel.Axis xAxis = (Excel.Axis)chartPage.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xAxis.HasTitle = true;
            xAxis.AxisTitle.Text = colNames[PredictorIdx];

            Excel.Axis yAxis = (Excel.Axis)chartPage.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yAxis.HasTitle = true;
            yAxis.AxisTitle.Text = colNames[ResponseIdx];

            // set the graph title
            chartPage.HasTitle = true;
            chartPage.ChartTitle.Font.Size = 12;
            chartPage.ChartTitle.Text = GraphTitle();

            // save the workbook and close Excel
            xlWorkBook.SaveAs(fname, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            xlWorkBook.Close(true, Type.Missing, Type.Missing);
            xlApp.DisplayAlerts = true;
            xlApp.Quit();

            // release resources
            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }

        /// <summary>
        /// Applies the trained neural network model to the selected predictor
        /// input data and displays the results in Excel.
        /// 
        /// The results consists of 3 columns - the first contains the selected
        /// training set input (or predictor) values, the second the selected 
        /// training set target values and the third contains the trained model 
        /// output responses to the given input values. An XY-Scatter graph of 
        /// the data is also produced.
        /// </summary>
        /// <param name="net">the trained neural network</param>
        private void ShowOutputInExcel(NeuralNet net)
        {
            List<double> dX = new List<double>();
            List<double> dM = new List<double>();

            // plot the model and target data in Excel
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            // open Excel and add a worksheet
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            // add a header line
            xlWorkSheet.Cells[1, 1] = "";
            xlWorkSheet.Cells[1, 2] = "target";
            xlWorkSheet.Cells[1, 3] = "model";

            // add the model and training data 
            for (int i = 0; i < mInputVecs.Count; i++)
            {
                // calculate the model response value given the predictor value from the training set
                dX = mInputVecs[i];
                net.GetResponse(dX, dM);

                // the required values are stored in vectors and need re-scaling
                double xValue = dX[0] * ScaleFactor;
                double yValue = mTargetVecs[i][0] * ScaleFactor;
                double mValue = dM[0] * ScaleFactor;

                // write out the results
                xlWorkSheet.Cells[i + 2, 1] = xValue.ToString("G16");
                xlWorkSheet.Cells[i + 2, 2] = yValue.ToString("G16");
                xlWorkSheet.Cells[i + 2, 3] = mValue.ToString("G16");
            }

            // plot the data as an xy-scatter graph
            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject modelChart = (Excel.ChartObject)xlCharts.Add(200, 10, 450, 300);
            Excel.Chart chartPage = modelChart.Chart;

            chartRange = xlWorkSheet.get_Range("A1", "C" + (mInputVecs.Count + 1).ToString());
            chartPage.SetSourceData(chartRange);
            chartPage.ChartType = Excel.XlChartType.xlXYScatter;

            // set the axis labels
            List<string> colNames = new List<string>();
            mDataTab.GetColumnNames(colNames);

            Excel.Axis xAxis = (Excel.Axis)chartPage.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xAxis.HasTitle = true;
            xAxis.AxisTitle.Text = colNames[PredictorIdx];

            Excel.Axis yAxis = (Excel.Axis)chartPage.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yAxis.HasTitle = true;
            yAxis.AxisTitle.Text = colNames[ResponseIdx];

            // set the graph title
            chartPage.HasTitle = true;
            chartPage.ChartTitle.Font.Size = 12;
            chartPage.ChartTitle.Text = GraphTitle();

            // make Excel visible to the user
            xlApp.Visible = true;

            // release resources
            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }

        /// <summary>
        /// Releases a specified COM object.
        /// </summary>
        /// <param name="obj">the object to be released</param>
        /// 
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Formats the graph title for the Excel plot.
        /// </summary>
        /// <returns>the formatted graph title</returns>
        private String GraphTitle()
        {
            // extract the variable names and make them lowercase
            List<string> colNames = new List<string>();
            mDataTab.GetColumnNames(colNames);            
            string predictor = colNames[PredictorIdx].ToLower();
            string response = colNames[ResponseIdx].ToLower();

            // make the first letter of each name uppercase
            string firstLetter = predictor.Substring(0, 1);
            firstLetter = firstLetter.ToUpper();
            predictor = predictor.Remove(0, 1);
            predictor = predictor.Insert(0, firstLetter);

            firstLetter = response.Substring(0, 1);
            firstLetter = firstLetter.ToUpper();
            response = response.Remove(0, 1);
            response = response.Insert(0, firstLetter);

            // create and format the finished title
            string title = predictor + " (Predictor) vs " + response + " (Response)";

            return title;
        }

        /// <summary>
        /// Loads the selected data file into an internal DataTable object.
        ///
        /// The first 100 lines of the data file are also written to a table and
        /// displayed in the form's rich text box data preview control. The 
        /// predictor and response variables list boxes are also populated with
        /// the data file column headings.
        /// </summary>
        /// <param name="filename">the full name, including path, of the data file</param>
        /// 
        private void LoadData(string filename)
        {
            // create a data table from the database file containing the training data
            mDataTab.ReadFromFile(filename, HasHeader);

            // check that the data has been read from the file without any errors
            if (mDataTab.NumRows <= 0 || mDataTab.NumCols <= 0)
            {
                MessageBox.Show("The selected file does not appear to be in the correct format.",
                                "ModelFit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // set the data file name (after the data has successfully loaded)
                DataFile = filename;

                // populate the data preview
                this.DataPreviewRTBox.Clear();
                this.DataPreviewRTBox.Rtf = InsertPreviewTableInRTBox(1000);
                
                // populate the x and y variable list boxes
                PopulateVariableListBoxes();

                // update the information text label
                this.InfoLabel.Text = "Please select the variables";
            }
        }

        /// <summary>
        /// Adds the first 100 lines of the input data file to a rich text format table.
        /// </summary>
        /// <param name="width">the width of the cells in the rtf table</param>
        /// <returns>a string representation of the rtf table</returns>
        /// 
        private String InsertPreviewTableInRTBox(int width)
        {
            // use a string builder to create the preview table
            StringBuilder sringTableRtf = new StringBuilder();

            // begin the rich text formatting
            sringTableRtf.Append(@"{\rtf1 ");

            // add the DataTable column headings to the start of preview table
            AddColumnNamesToRtfTable(sringTableRtf, width);

            // populate the preview table with up to the first 100 rows of the DataTable
            int numRows = 100;

            if (mDataTab.NumRows < 100)
            {
                // the table has less than 100 rows
                numRows = mDataTab.NumRows;
            }

            // variable for cell width
            int cellWidth;

            for (int i = 0; i < numRows; i++)
            {
                // start the table row
                sringTableRtf.Append(@"\trowd");

                // get the row data
                List<double> row = new List<double>();
                mDataTab.GetNumericRow(i, row);

                for (int j = 0; j < mDataTab.NumCols; j++)
                {
                    // create a cell with the required width
                    cellWidth = (j + 1) * width;

                    // set the cell width
                    sringTableRtf.Append(@"\cellx" + cellWidth.ToString());

                    // populate the row cells
                    string colVal = row[j].ToString();

                    // only show at most 9 significant figures (to fit the cell width)
                    if (colVal.Length > 9)
                    {
                        colVal = row[j].ToString("G9");
                    }

                    if (j == 0)
                    {
                        sringTableRtf.Append(@"\intbl  " + colVal);
                    }
                    else
                    {                        
                        sringTableRtf.Append(@"\cell  " + colVal);
                    }
                }

                // end the table row
                sringTableRtf.Append(@"\intbl \cell \row");
            }

            // add the minimum and maximum values found in each column to the end of the table
            AddMinMaxValuesToRtfTable(sringTableRtf, width);

            // add the DataTable column headings to the end preview table
            AddColumnNamesToRtfTable(sringTableRtf, width);

            // close rich text formatting
            sringTableRtf.Append(@"\pard");
            sringTableRtf.Append(@"}");

            // convert the string builder to string
            return sringTableRtf.ToString();
        }

        /// <summary>
        /// Adds the DataTable column headings to the RTF preview table.
        /// </summary>
        /// <param name="sTableRTF">the rtf table string builder</param>
        /// <param name="width">the width of the cells in the rtf table</param>        
        ///
        private void AddColumnNamesToRtfTable(StringBuilder sTableRTF, int width)
        {
            // populate the preview table header from the DataTable column headings
            List<string> colNames = new List<string>();
            mDataTab.GetColumnNames(colNames);

            if (colNames.Count > 0)
            {
                // start the row
                sTableRTF.Append(@"\trowd");

                for (int j = 0; j < mDataTab.NumCols; j++)
                {
                    // create a cell with the required width
                    sTableRTF.Append(@"\cellx" + ((j + 1) * width).ToString());

                    string columnName = colNames[j];

                    // truncate the name if it is too long to fit inside a table cell
                    if (columnName.Length > 12)
                    {
                        columnName = columnName.Substring(0, 12);
                    }

                    if (j == 0)
                    {
                        sTableRTF.Append(@"\intbl  " + columnName);
                    }
                    else
                    {
                        sTableRTF.Append(@"\cell  " + columnName);
                    }
                }

                // add the table header row
                sTableRTF.Append(@"\intbl \cell \row");
            }
        }

        /// <summary>
        /// Adds the minimum and maximum values from each DataTable column to
        /// the relevant column of the RTF preview table.
        /// </summary>
        /// <param name="sTableRTF">the rtf table string builder</param>
        /// <param name="width">the width of the cells in the rtf table</param>
        private void AddMinMaxValuesToRtfTable(StringBuilder sTableRTF, int width)
        {
            int cellWidth;            
            List<double> column = new List<double>();

            // add rows to the rtf table containing the minimum and maximum column values
            for (int i = 0; i < 2; i++)
            {
                // start the table row
                sTableRTF.Append(@"\trowd");

                for (int j = 0; j < mDataTab.NumCols; j++)
                {
                    // get the column data
                    column.Clear();
                    mDataTab.GetNumericCol(j, column);

                    double minMaxvalue = 0;

                    if (i == 0 && column.Count > 0)
                    {
                        // get the minimum value using linq
                        minMaxvalue = column.Min();
                    }
                    else if (i == 1 && column.Count > 0)
                    {
                        // get the maximum value using linq
                        minMaxvalue = column.Max();
                    }

                    // create a cell with the required width
                    cellWidth = (j + 1) * width;

                    // set the cell width
                    sTableRTF.Append(@"\cellx" + cellWidth.ToString());

                    // populate the row cells
                    string colVal = minMaxvalue.ToString();

                    // only show at most 9 significant figures (to fit the cell width)
                    if (colVal.Length > 9)
                    {
                        colVal = minMaxvalue.ToString("G9");
                    }

                    if (j == 0)
                    {
                        sTableRTF.Append(@"\intbl  " + colVal);
                    }
                    else
                    {
                        sTableRTF.Append(@"\cell  " + colVal);
                    }
                }

                // end the table row
                sTableRTF.Append(@"\intbl \cell \row");
            }
        }

        /// <summary>
        /// Populates the form's Activation List boxes with the available 
        /// values of the ActiveT enumerated type.
        /// </summary>
        /// 
        private void PopulateActivationListBoxes()
        {
            int id = 0;
            bool done = false;

            while (!done)
            {
                ActiveT func = (ActiveT)id;
                string funcID = func.ToString();

                if (funcID == id.ToString())
                {
                    // we have processed all the valid ActiveT enum values
                    done = true;
                }
                else
                {
                    funcID = funcID.Substring(1);

                    this.OutFuncListBox.Items.Add(funcID);
                    this.HidFuncListBox.Items.Add(funcID);

                    id++;
                }
            }

            // select the first entries in the list boxes
            this.OutFuncListBox.SelectedIndex = 0;
            this.HidFuncListBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Populates the forms predictor and response variables 
        /// list boxes with the data file column headings.
        /// </summary>
        /// 
        private void PopulateVariableListBoxes()
        {
            // clear the list boxes
            this.XlistBox.Items.Clear();
            this.YlistBox.Items.Clear();

            if (mDataTab.HasHeader)
            {
                // populate the list boxes with the data header row
                List<string> colNames = new List<string>();
                mDataTab.GetColumnNames(colNames);

                for (int i = 0; i < colNames.Count; i++)
                {
                    this.XlistBox.Items.Add(colNames[i]);
                    this.YlistBox.Items.Add(colNames[i]);
                }                
            }
            else
            {
                // populate the list boxes with column ids
                for (int i = 0; i < mDataTab.NumCols; i++)
                {
                    string name = "Column " + (i + 1).ToString();

                    this.XlistBox.Items.Add(name);
                    this.YlistBox.Items.Add(name);
                }
            }
        }
    }
}
