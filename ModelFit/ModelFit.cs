/////////////////////////////////////////////////////////////////////
//
// Implements the ModelFit class
//
// Author: Jason Jenkins
//
// This application displays a form based GUI that allows the user 
// to explore the various settings that can be applied to a simple
// single hidden layer neural network that can be used to model 
// the potential relationship between a single predictor variable (X)
// and a single corresponding response variable (Y) chosen from a
// selected .CSV data file.
//
// The results of the model fit can be saved in both .CSV and .XLS
// format and they can also be viewed directly in Excel if desired.
//
/////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;

namespace ModelFit
{
    static class ModelFit
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ModelFitForm());
        }
    }
}
