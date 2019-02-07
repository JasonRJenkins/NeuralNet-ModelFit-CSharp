/////////////////////////////////////////////////////////////////////
//
// Implements the Data Table class
//
// Author: Jason Jenkins
//
// This class is a representation of a database table that can be
// easily manipulated for use in mathematical or statistical analysis.
//
// A DataTable object can be created from a file representation of
// a table in .CSV format or by adding rows individually.
//
// Non-numeric values are automatically assigned a numeric alias to 
// help facilitate mathematical analysis of the data. These values
// can be overridden if desired.
//
/////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

/////////////////////////////////////////////////////////////////////

namespace Data.Table
{
    /// <summary>
    /// This class is a representation of a database table that can be
    /// easily manipulated for use in mathematical or statistical analysis.
    /// </summary>
    ///
    class DataTable
    {
        /////////////////////////////////////////////////////////////////////
        // Private Data Members
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// the number of rows in the table
        /// </summary>
        private int mRows = 0;

        /// <summary>
        /// the number of columns in the table
        /// </summary>
        private int mCols = 0;

        /// <summary>
        /// true if the table has a header row containing column names
        /// </summary>
        private bool mHeader = false;

        /// <summary>
        /// the column names (if supplied)
        /// </summary>
        private List<string> mColumnNames = new List<string>();

        /// <summary>
        /// the raw table data - each row is a list of column values in string format
        /// and the table consists of a list of rows
        /// </summary>
        private List<List<string>> mRawData = new List<List<string>>();

        /// <summary>
        /// keeps track of the automatic alias values used by each column
        /// </summary>
        private List<double> mAliasLst = new List<double>();

        /// <summary>
        /// maps a string column name to a corresponding numeric list index
        /// </summary>
        private Dictionary<string, int> mColIdx = new Dictionary<string, int>();

        /// <summary>
        /// maps a string name to a numeric value (in string format) 
        /// </summary>
        private Dictionary<string, string> mAliases = new Dictionary<string, string>();

        /////////////////////////////////////////////////////////////////////
        // Constructors
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// 
        public DataTable() { }

        /// <summary>
        /// Constructs a DataTable object from a .CSV file representation.
        /// </summary>
        /// <param name="fName">the name of the .CSV file containing the data</param>
        /// <param name="header">set it to true if the data has a header row
        ///                      containing column names otherwise set it to false</param>
        ///                      
        public DataTable(string fName, bool header = true)
        {
            mRows = 0;
	        mCols = 0;
	        mHeader = header;

	        // read in the data from the file stream
	        ReadFromStream(fName, header);

            // initialise the alias list values to zero
            for (int i = 0; i < mCols; i++)
	        {
		        mAliasLst.Add(0);
	        }
        }

        /////////////////////////////////////////////////////////////////////
        // Properties
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The number of rows in the table.
        /// </summary>
        /// 
        public int NumRows
        {
            get
            {
                return mRows;
            }
        }

        /// <summary>
        /// The number of columns in the table.
        /// </summary>
        /// 
        public int NumCols
        {
            get
            {
                return mCols;
            }
        }

        /// <summary>
        /// Set to true if the data file has a header row.
        /// </summary>
        public bool HasHeader
        {
            get
            {
                return mHeader;
            }
            set
            {
                mHeader = value;
            }
        }

        /////////////////////////////////////////////////////////////////////
        // Public Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Clears a DataTable object ready for re-use.
        /// </summary>
        /// 
        public void ClearTable()
        {
            mRows = 0;
            mCols = 0;
            mHeader = false;

            mColumnNames.Clear();
            mRawData.Clear();
            mColIdx.Clear();
            mAliases.Clear();
            mAliasLst.Clear();
        }

        /// <summary>
        /// Adds a new row to the DataTable.
        /// </summary>
        /// <param name="row">a list of column values in string format</param>
        /// <returns>0 if successful otherwise -1</returns>
        /// 
        public int AddRawRow(List<string> row)
        {
	        int retVal = 0;

	        // check for the very first row to be added
	        if (mRows <= 0 && mCols <= 0)
            {
		        mRows = 0;
		        mCols = row.Count;

		        // initialise the alias list values to zero
		        for (int i = 0; i < mCols; i++)
		        {
			        // this keeps track of the automatic alias values used by each column
			        mAliasLst.Add(0);
		        }
            }

	        if (mCols == row.Count)
	        {
		        mRawData.Add(row);
		        mRows++;
	        }
	        else
	        {
                WriteMessage("ERROR: You cannot insert a row with: " + row.Count +
                             " columns into a table with: " + mCols + " columns");
		        retVal = -1;
	        }

	        return retVal;
        }

        /// <summary>
        /// Gets a data row with the values in string format.
        /// </summary>
        /// <param name="nRow">the numeric index of the row to be returned</param>
        /// <param name="row">the row data as a list of string values</param>
        /// <returns>0 if successful otherwise -1</returns>
        /// 
        public int GetRawRow(int nRow, List<string> row)
        {
            int retVal = 0;

            if ((nRow < 0) || ((nRow + 1) > mRows))
            {
                WriteMessage("ERROR: The requested row index: " + nRow + " is out of bounds\n" +
                             "INFORMATION: The table has: " + mRows + " rows, indexed from 0 to " + (mRows - 1));
                retVal = -1;
            }
            else
            {
                row.Clear();
                row.AddRange(mRawData[nRow]);
            }

            return retVal;
        }

        /// <summary>
        /// Gets a data row with the values in double format.
        /// 
        /// Non-numeric data is automatically assigned a numeric alias if an
        /// alias has not already been set up. The first non-numeric entry in 
        /// a column is assigned the value 0 the next distinct entry is 
        /// assigned the value 1 and the next 2 and so on. You can set your 
        /// own alias values using the SetAlias method.
        /// </summary>
        /// <param name="nRow">the numeric index of the row to be returned</param>
        /// <param name="row">the row data as a list of double values</param>
        /// <returns>0 if successful otherwise -1</returns>
        /// 
        public int GetNumericRow(int nRow, List<double> row)
        {
            int retVal = 0;

            if ((nRow < 0) || ((nRow + 1) > mRows))
            {
                WriteMessage("ERROR: The requested row index: " + nRow + " is out of bounds\n" +
                             "INFORMATION: The table has: " + mRows + " rows, indexed from 0 to " + (mRows - 1));
                retVal = -1;
            }
            else
            {
                // get the row data in raw (string) format
                List<string> rawRow = mRawData[nRow];
                int len = rawRow.Count;

                // convert each row element from a string to a numeric value
                for (int i = 0; i < len; i++)
                {
                    double value = 0;
                    string sVal = rawRow[i];

                    // check to see if we already have an alias set up
                    string sCol = i.ToString();

                    // use the string column value and the column index as the alias key
                    // this allows multiple aliases for the same value to be set up e.g.
                    // in one column you may have values: 'red', 'blue' and 'green' aliased
                    // to values 0, 1 and 2 respectively and in a second column you may have 
                    // values: 'black', 'yellow' and 'red' aliased to 0, 1 and 2 respectively
                    // i.e. two aliases for 'red' are set up, one for each column
                    string sKey = sVal + " " + sCol;

                    if (mAliases.ContainsKey(sKey))
                    {
                        sVal = mAliases[sKey];
                    }

                    // try the string to value conversion
                    try
                    {
                        value = Convert.ToDouble(sVal);
                    }
                    catch
			        {
                        // assign non-numeric column values integer values starting with zero
                        value = mAliasLst[i];          // this list has initial values set to 0
                        mAliasLst[i] = value + 1;      // the next value to use

                        // create and add the value-alias to the aliases map
                        string strAlias = value.ToString();
                        mAliases.Add(sKey, strAlias);
                    }

                    row.Add(value);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets a data column with the values in string format.
        /// </summary>
        /// <param name="nCol">the numeric index of the column to be returned</param>
        /// <param name="col">the column data as a list of string values</param>
        /// <returns>0 if successful otherwise -1</returns>
        /// 
        public int GetRawCol(int nCol, List<string> col)
        {
            int retVal = 0;

            if ((nCol < 0) || ((nCol + 1) > mCols))
            {
                WriteMessage("ERROR: The requested column index: " + nCol + " is out of bounds\n" +
                             "INFORMATION: The table has: " + mCols + " columns, indexed from 0 to " + (mCols - 1));
                retVal = -1;
            }
            else
            {
                // extract the column data
                for (int rows = 0; rows < mRows; rows++)
                {
                    col.Add(mRawData[rows][nCol]);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets a data column with the values in string format.
        /// </summary>
        /// <param name="sCol">the name of the column to be returned</param>
        /// <param name="col">the column data as a list of string values</param>
        /// <returns>0 if successful otherwise -1</returns>
        /// 
        public int GetRawCol(string sCol, List<string> col)
        {
	        int iCol;
            int retVal = 0;

            // map the string "index" to the actual integer index
            if (mColIdx.ContainsKey(sCol))
            {
                iCol = mColIdx[sCol];

                // use the numeric indexed version of getRawCol
                retVal = GetRawCol(iCol, col);
            }
        	else
	        {
                WriteMessage("ERROR: The requested column index string: " + sCol +
                             " does not have a corresponding numeric index assigned to it");
                retVal = -1;
	        }

	        return retVal;
        }

        /// <summary>
        /// Gets a data column with the values in double format.
        /// 
        /// Non-numeric data is automatically assigned a numeric alias if an
        /// alias has not already been set up. The first non-numeric entry in 
        /// a column is assigned the value 0 the next distinct entry is 
        /// assigned the value 1 and the next 2 and so on. You can set your 
        /// own alias values using the SetAlias method.
        /// </summary>
        /// <param name="nCol">the numeric index of the column to be returned</param>
        /// <param name="col">the column data as a list of double values</param>
        /// <returns>0 if successful otherwise -1</returns>
        /// 
        public int GetNumericCol(int nCol, List<double> col)
        {
            int retVal = 0;

            if ((nCol < 0) || ((nCol + 1) > mCols))
            {
                WriteMessage("ERROR: The requested column index: " + nCol + " is out of bounds\n" +
                             "INFORMATION: The table has: " + mCols + " columns, indexed from 0 to " + (mCols - 1));
                retVal = -1;
            }
            else
            {
                for (int rows = 0; rows < mRows; rows++)
                {
                    double value = 0;
                    string sVal = mRawData[rows][nCol];

                    // check to see if we already have an alias set up
                    string sCol = nCol.ToString();

                    // use the string column value and the column index as the alias key
                    // this allows multiple aliases for the same value to be set up e.g.
                    // in one column you may have values: 'red', 'blue' and 'green' aliased
                    // to values 0, 1 and 2 respectively and in a second column you may have 
                    // values: 'black', 'yellow' and 'red' aliased to 0, 1 and 2 respectively
                    // i.e. two aliases for 'red' are set up, one for each column
                    string sKey = sVal + " " + sCol;

                    if (mAliases.ContainsKey(sKey))
                    {
                        sVal = mAliases[sKey];
                    }

                    // try the string to value conversion
                    try
                    {
                        value = Convert.ToDouble(sVal);
                    }
                    catch
			        {
                        // assign non-numeric column values integer values starting with zero
                        value = mAliasLst[nCol];          // this list has initial values set to 0
                        mAliasLst[nCol] = value + 1;      // the next value to use

                        // create and add the value-alias to the aliases map
                        string strAlias = value.ToString();
                        mAliases.Add(sKey, strAlias);
                    }

                    col.Add(value);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets a data column with the values in double format.
        /// </summary>
        /// <param name="sCol">the name of the column to be returned</param>
        /// <param name="col">the column data as a list of double values</param>
        /// <returns>0 if successful otherwise -1</returns>
        /// 
        public int GetNumericCol(string sCol, List<double> col)
        {
	        int iCol;
            int retVal = 0;

            // map the string "index" to the actual integer index
            if (mColIdx.ContainsKey(sCol))
            {
                iCol = mColIdx[sCol];

                // use the numeric indexed version of getRawCol
                retVal = GetNumericCol(iCol, col);
            }
	        else
	        {
                WriteMessage("ERROR: The requested column index string: " + sCol +
                             " does not have a corresponding numeric index assigned to it");
                retVal = -1;
	        }

	        return retVal;
        }

        /// <summary>
        /// Sets a numeric alias for a given string value and column index.
        /// </summary>
        /// <param name="sValue">the value to be given an alias</param>
        /// <param name="dAlias">the alias to be applied</param>
        /// <param name="nCol">the column index of the column containing the value</param>
        /// 
        public void SetAlias(string sValue, double dAlias, int nCol)
        {
            string sAlias = dAlias.ToString();

	        // check to see if we have an alias set up
	        string sCol = nCol.ToString();

	        // using the string value and the column index as the alias key
	        // allows multiple aliases for the same value to be set up e.g.
	        // in one column you may have values: 'red', 'blue' and 'green' aliased
	        // to values 0, 1 and 2 respectively and in a second column you may have 
	        // values: 'black', 'yellow' and 'red' aliased to 0, 1 and 2 respectively
	        // i.e. two aliases for 'red' are set up, one for each column
	        string sKey = sValue + " " + sCol;

            if (mAliases.ContainsKey(sKey))
            {
                // replace the pre-existing alias
                mAliases.Remove(sKey);
                mAliases.Add(sKey, sAlias);
            }
	        else
	        {
                mAliases.Add(sKey, sAlias);
	        }
        }

        /// <summary>
        /// Returns the numeric alias for a given string value and column index.
        /// </summary>
        /// <param name="sValue">the string value whose alias is required</param>
        /// <param name="nCol">the column index of the column containing the value</param>
        /// <returns>the alias value if it exists otherwise -1e10</returns>
        /// 
        public double GetAliasValue(string sValue, int nCol)
        {
            double value = -1e10;

            string sCol = nCol.ToString();

	        // using the string value and the column index as the alias key
	        // allows multiple aliases for the same value to be set up e.g.
	        // in one column you may have values: 'red', 'blue' and 'green' aliased
	        // to values 0, 1 and 2 respectively and in a second column you may have 
	        // values: 'black', 'yellow' and 'red' aliased to 0, 1 and 2 respectively
	        // i.e. two aliases for 'red' are set up, one for each column
	        string sKey = sValue + " " + sCol;

	        if (mAliases.ContainsKey(sKey))
            {

                string sVal = mAliases[sKey];

                // try the string to value conversion
                try
		        {
			        value = Convert.ToDouble(sVal);
                }
		        catch
		        {
                    // the alias has been set up incorrectly
                    WriteMessage("ERROR: The alias value: " + sVal + " for the string: " + sValue +
                                 " cannot be converted to a numeric value");
		        }
	        }
	        else
	        {
                // an alias has not been set up
                WriteMessage("ERROR: The string: " + sValue + " does not have an alias");
	        }

	        return value;
        }

        /// <summary>
        /// Returns the column index for a given named column.
        /// </summary>
        /// <param name="sColName">the column name</param>
        /// <returns>the integer index of the named column or -1 if it doesn't exist</returns>
        /// 
        public int GetColIndex(string sColName)
        {
	        int iCol = -1;

            // map the string "index" to the actual integer index
	        if (mColIdx.ContainsKey(sColName))
	        {
		        iCol = mColIdx[sColName];
	        }
	
	        return iCol;
        }

        /// <summary>
        /// Gets the column names list.
        /// </summary>
        /// <param name="colNames">the list containing the column names</param>
        /// 
        public void GetColumnNames(List<string> colNames)
        {
            if (mHeader == true)
            {
                colNames.Clear();
                colNames.AddRange(mColumnNames);
            }
        }

        /// <summary>
        /// Sets the column names list.
        /// </summary>
        /// <param name="colNames">the list containing the column names</param>
        /// 
        public void SetColumnNames(List<string> colNames)
        {
	        int size = colNames.Count;

            if (mCols <= 0)
            {
                // we have an empty table
                mColumnNames.Clear();
                mColumnNames.AddRange(colNames);
                mCols = size;
                mHeader = true;
            }
            else if (mCols == size)
            {
                // a straight replacement for the column names
                mColumnNames.Clear();
                mColumnNames.AddRange(colNames);
                mHeader = true;
            }
	        else
	        {
                WriteMessage("ERROR: The number of supplied column names: " + size +
                             " is not compatible with the number of columns in the table: " + mCols);
	        }
        }

        /// <summary>
        /// Write this DataTable object to a .CSV file.
        /// </summary>
        /// <param name="fName">the name of the file to write the data to</param>
        /// 
        public void WriteToFile(string fName)
        {
            using (StreamWriter ofstream = new StreamWriter(fName))
            {
                WriteToStream(ofstream);
            }
        }

        /// <summary>
        /// Clears and re-instantiates this DataTable object from a .CSV file representation.
        /// </summary>
        /// <param name="fName">the name of the .CSV file containing the data</param>
        /// <param name="header">true if the data has a header row containing
        ///                      column names otherwise set to false</param>
        /// <returns>0 if successful otherwise -1</returns>
        /// 
        public int ReadFromFile(string fName, bool header)
        {
            int retVal = 0;

            ClearTable();
            ReadFromStream(fName, header);

            // set the table header parameter
            HasHeader = header;

            // initialise the alias list values to zero
            for (int i = 0; i < mCols; i++)
	        {
		        mAliasLst.Add(0);
	        }

	        // check to see if there was an error reading the file
	        if (mRows < 0 || mCols < 0)
	        {
		        retVal = -1;
	        }

	        return retVal;
        }

        /////////////////////////////////////////////////////////////////////
        // Private Methods
        /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Outputs the table data via a file stream writer.
        /// </summary>
        /// <param name="ofstream">the output file stream writer</param>
        /// 
        private void WriteToStream(StreamWriter ofstream)
        {
            // output the header
            if (mHeader == true)
            {
                int size = mColumnNames.Count;

                for (int i = 0; i < (size - 1); i++)
                {
                    // the data is comma delimited
                    ofstream.Write(mColumnNames[i]);
                    ofstream.Write(",");
                }

                ofstream.WriteLine(mColumnNames[size - 1]);
            }

            // output the data
            for (int row = 0; row < mRows; row++)
            {
                for (int col = 0; col < mCols; col++)
                {
                    ofstream.Write(mRawData[row][col]);

                    if (col < (mCols - 1))
                    {
                        // the data is comma delimited
                        ofstream.Write(",");
                    }
                }

                ofstream.WriteLine();
            }

            ofstream.Close();
        }

        /// <summary>
        /// Reads in the table data using a file stream reader.
        /// </summary>
        /// <param name="fName">the name of the file to read the data from</param>
        /// <param name="header">set to true if the input file has a header row 
        ///                      containing column names otherwise set to false</param>
        ///                      
        private void ReadFromStream(string fName, bool header)
        {
            using (StreamReader inStream = new StreamReader(fName))
            {
                char[] ws = { '\t', '\n', '\r', '\f', '\v' };   // white space characters

                if (inStream != null)
                {
                    // read in the header data if it is available
                    if (header == true)
                    {
                        string headline = inStream.ReadLine();

                        if (headline != null)
                        {
                            bool nextLine = false;
                            int idx = 0;
                            int sLen = headline.Length;

                            if (sLen > 0)
                            {
                                string sValue = "";

                                while (!nextLine)
                                {
                                    bool nextValue = false;

                                    while (!nextValue)
                                    {
                                        string sChar = headline.ElementAt(idx).ToString();

                                        // the data is comma delimited
                                        if (sChar == ",")
                                        {
                                            nextValue = true;
                                        }
                                        else if (sChar != "\"")
                                        {
                                            // quotes can be used to delimit strings - so ignore them
                                            sValue += sChar;
                                        }

                                        idx++;

                                        if (idx >= sLen)
                                        {
                                            nextLine = true;
                                            nextValue = true;
                                        }
                                    }

                                    // trim leading and trailing whitespace
                                    if (sValue.Length != 0)
                                    {
                                        sValue.TrimStart(ws);   // left trim
                                        sValue.TrimEnd(ws);     // right trim
                                    }
                                    else
                                    {
                                        sValue = "<blank>";
                                    }

                                    mColumnNames.Add(sValue);
                                    mColIdx.Add(sValue, mCols);
                                    mCols++;

                                    sValue = "";
                                }
                            }
                        }
                    }

                    // read in the data
                    string line = inStream.ReadLine();

                    while (line != null)
                    {
                        List<string> row = new List<string>();

                        bool nextLine = false;
                        int idx = 0;
                        int sLen = line.Length;

                        if (sLen > 0)
                        {
                            bool discardLine = false;
                            string sValue = "";

                            while (!nextLine)
                            {
                                bool nextValue = false;

                                while (!nextValue)
                                {
                                    string sChar = line.ElementAt(idx).ToString();

                                    // the data is comma delimited
                                    if (sChar == ",")
                                    {
                                        nextValue = true;
                                    }
                                    else if (sChar != "\"")
                                    {
                                        // quotes can be used to delimit strings - so ignore them
                                        sValue += sChar;
                                    }

                                    idx++;

                                    // check for end of line
                                    if (idx >= sLen)
                                    {
                                        nextLine = true;
                                        nextValue = true;
                                    }
                                }

                                // trim leading and trailing whitespace
                                if (sValue.Length != 0)
                                {
                                    sValue.TrimStart(ws);   // left trim
                                    sValue.TrimEnd(ws);     // right trim
                                }

                                // reject data rows with missing data - identified by "?"
                                if (sValue != "?")
                                {
                                    row.Add(sValue);
                                    sValue = "";
                                }
                                else
                                {
                                    discardLine = true;
                                    nextLine = true;
                                }
                            }

                            if (!discardLine)
                            {
                                mRawData.Add(row);
                                mRows++;
                            }

                            // if a header is not supplied set the number of columns
                            if (mCols == 0 && header == false && !discardLine)
                            {
                                mCols = row.Count;
                            }

                            // check that the row sizes are consistent
                            if (mCols != row.Count && !discardLine)
                            {
                                WriteMessage("ERROR: Reading from file - the data in the file: " + fName +
                                             " does not maintain a consistent number of columns");

                                // set the number of rows and columns to invalid values to signify an error
                                mRows = -1;
                                mCols = -1;

                                return;
                            }
                        }

                        // read the next line
                        line = inStream.ReadLine();
                    }

                    // tidy up
                    inStream.Close();
                }
                else
                {
                    WriteMessage("ERROR: Reading from file - unable to open the file: " + fName);

                    // set the number of rows and columns to invalid values to signify an error
                    mRows = -1;
                    mCols = -1;
                }
            }
        }

        /// <summary>
        /// Writes a message to the console or a message box depending on the context.
        /// </summary>
        /// <param name="msgString">contains the message</param>
        private void WriteMessage(string msgString)
        {
            if (Console.IsErrorRedirected)
            {
                MessageBox.Show(msgString, "DataTable", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Console.WriteLine(msgString);
            }
        }

        /////////////////////////////////////////////////////////////////////
    }
}

/////////////////////////////////////////////////////////////////////
