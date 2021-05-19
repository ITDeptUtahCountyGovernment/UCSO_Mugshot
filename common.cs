using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SnapShot
{
  class common
  {

    /*
  int GetFile(string sFullFileName, ref ArrayList alData)
  bool DoesFileExist(string sFullFileName)
  bool DeleteFile(string sPath, string sFileName)
  bool CreateDirectory(string sDirectory, ref string sError)
  bool DoesDirectoryExist(string sDirectory)
  bool SaveFile(string sPath, string sFileName, ArrayList myItems, string sMyError)
  string ReplaceCharsInString(string sIn, string sReplaceCharString, string sNewStr)
  int NumStringMatchs(string sInString, string sLookFor)
  ArrayList extractDistinctValuesFromMultiStringArray(string[,] sarDepts, int xdistinctcolnum, int ydistinctcolnum, bool trimcolvaluebeforecompare, 
                                                      bool uppercasevalue, bool removespecialchars, ref Int32 iNumItems)
  ArrayList extractColNamesFromSQL(string csSQL)
  string[] ParseOutString(string sInString, char cDelim, ref int nItems)
  int ParseOutArrayListToStringArray(ArrayList alData, char cDelim, ref string[,] sarData)
  int NumCharMatchs(string sInString, string sLookFor)
  int NumMaxStringMatchsInArrayList(ArrayList alData, char cDelim)
  void InitMultiStringArray(ref string[,] sArray)
  bool ExtractDrivePathFile(string sFileSpec, ref string sDrive, ref string sPath, ref string sFile)
  int ScanStringFromEnd(string instring, string sLookFor, ref string resultstring, int nStartPos)
  string MakeReverseString(string sIn)
  double GetJulianDateValue(ref string sJDValue)
     */

    public static char cdblqt = Convert.ToChar(34);
    public static char cdblqtesc = Convert.ToChar(189);
    public static char csglqt = Convert.ToChar(39);
    public static char csglqtesc = Convert.ToChar(169);
    public static char ccomma = Convert.ToChar(44);
    public static char ccommaesc = Convert.ToChar(174);
    public static char camp = Convert.ToChar(38);
    public static char campesc = Convert.ToChar(188);
    public static char ccr = Convert.ToChar(13);
    public static char ccresc = Convert.ToChar(191);
    public static char clf = Convert.ToChar(10);
    public static char clfesc = Convert.ToChar(190);

    /// <summary>
    /// Inits the multi string array.
    /// </summary>
    /// <param name="sArray">The s array.</param>
    public static void InitMultiStringArray(ref string[,] sArray)
    {
      int nArrayLen1 = sArray.GetLength(0);
      int nArrayLen2 = sArray.GetLength(1);
      for (int x = 0; x < nArrayLen1; x++)
      {
        for (int y = 0; y < nArrayLen2; y++)
        {
          sArray[x, y] = "";
        }//end y Loop          
      }//end x Loop    
    }//end of InitSingleStringArray

    // Name: NumMaxStringMatchsInArrayList
    // 
    // Calculates the max number columns in the ArrayList string array elements.
    // In the below example the nMaxCols returned would be 5.
    // Example
    // ArrayList alTestArray = new ArrayList();
    //  alTestArray.Add("data1|656665443.000|data3|");
    //  alTestArray.Add("data4|data5|data3|xyz|123|");
    // 
    // On Entry:
    //   alData - array list holding string elements to determine max columns on.
    //   cDelim - delimiter to use in determining the max num columns.
    //
    // On Exit:
    //   int - Max number items/columns found in scanning the array list
    //

    /// <summary>
    /// Nums the max string matchs in array list.
    /// </summary>
    /// <param name="alData">The al data.</param>
    /// <param name="cDelim">The c delim.</param>
    /// <returns></returns>
    public static int NumMaxStringMatchsInArrayList(ArrayList alData, char cDelim)
    {
      int nMaxNumCols = 0;
      int nRows = alData.Count;
      int nCols = 0;
      for (int i = 0; i < nRows; i++)
      {
        //nCols = CCommon.ccommon.NumCharMatchs(alData[i].ToString(), Convert.ToString(cDelim));
        nCols = common.NumCharMatchs(alData[i].ToString(), Convert.ToString(cDelim));
        if (nCols == 0)
        {
          nMaxNumCols = 1;
        }
        else 
        {
          if (nCols > 0)
          {
            nMaxNumCols = nCols + 1;
          }
        }
      }//end nMaxNulCols calc loop
      return nMaxNumCols;
    }

    /// <summary>
    /// Nums the char matchs.
    /// </summary>
    /// <param name="sInString">The string to lookfor the cLookfor in.</param>
    /// <param name="sLookFor">The character to look for.</param>
    /// <returns></returns>
    public static int NumCharMatchs(string sInString, string sLookFor)
    {
      int nNumMatchs = 0;
      int nLen = sInString.Length;
      for (int x = 0; x < nLen; x++)
      {
        if (sInString.Substring(x, 1) == sLookFor)
          nNumMatchs++;
      }//end x loop
      return nNumMatchs;
    }//end of NumCharMatchs


    // Name: ParseOutArrayListToStringArray
    // 
    // This method scans the passed in array list (alData), and loops through the arraylist extracting
    // each string delimited array element. Then parses the array string element, returning the individual
    // string items and builds the sarData string array.
    // - Example
    // - ArrayList alTestArray = new ArrayList();
    // - alTestArray.Add("data1|656665443.000|data3|");
    // - alTestArray.Add("data4|data5|data3|");
    // - Returned string array
    // - sarData[0, 0] = "data1"
    // - sarData[0, 1] = "656665443.000"
    // - sarData[0, 2] = "data3"
    // - sarData[1, 0] = "data4"
    // - sarData[1, 1] = "data5"
    // - sarData[1, 2] = "data3"
    //
    // On Entry:
    //   alData - array list holding delimited string elements
    //   cDelim - delimiter to parse the individual array list string elements with
    //   sarData - empty multi string array[,]
    // On Exit:
    //   nNumItems - number of multi string array items parsed by using the cDelim delimiter character
    //   sarData - populated multi string array from the ArrayList
    //

    /// <summary>
    /// Parses the out array list to string array.
    /// </summary>
    /// <param name="alData">The al data.</param>
    /// <param name="cDelim">The c delim.</param>
    /// <param name="sarData">The sar data.</param>
    /// <returns></returns>
    public static int ParseOutArrayListToStringArray(ArrayList alData, char cDelim, ref string[,] sarData)
    {
      InitMultiStringArray(ref sarData);
      int nNumItems = 0;
      int nRows = alData.Count;
      if (nRows > 0)
      {
        int i = 0, j = 0, nCols = 0;
        int nMaxNumCols = NumMaxStringMatchsInArrayList(alData, cDelim);
        if (nMaxNumCols > 0)
        {
          sarData = new string[nRows, nMaxNumCols];
          string[] sarParseData = new string[1];
          int nParseItems = 0;
          for (i = 0; i < nRows; i++)
          {
            sarParseData = ParseOutString(alData[i].ToString(), cDelim, ref nParseItems);
            for (j = 0; j < nMaxNumCols; j++)
            {
              if (j < nParseItems)
              {
                sarData[i, j] = sarParseData[j];
              }
              else
              {
                sarData[i, j] = "";
              }
            }
            nNumItems++;
          }//end i loop
        }//nCols > 0 chk
      }//nRows > 0 chk
      return nNumItems;
    }//end of ParseOutArrayListToStringArray

    // Name: ParseOutString
    // 
    // This method scans an input string and returns a string array of items delimited by cDelim.
    //
    // On Entry:
    //   sInString - string to scan
    //   cDelim - delimiter to parse the string with
    //
    // On Exit:
    //   string[] - array of strings from the parsed sInString
    //   nItems - number of items parsed by using the cDelim delimiter character
    //

    /// <summary>
    /// Parses the out string.
    /// </summary>
    /// <param name="sInString">The s in string.</param>
    /// <param name="cDelim">The c delim.</param>
    /// <param name="nItems">The n items.</param>
    /// <returns></returns>
    public static string[] ParseOutString(string sInString, char cDelim, ref int nItems)
    {
      nItems = 0;
      int nNumElements = NumCharMatchs(sInString, Convert.ToString(cDelim));
      string[] sItems = new string[nNumElements];
      if (sInString.IndexOf(cDelim) >= 0)
      {
        sItems = sInString.Split(cDelim);
        nItems = sItems.Length;
      }
      return sItems;
    }//end parseoutstring

    public static ArrayList extractColNamesFromSQL(string csSQL)
    {
      //------------------------------------------------------------------------------------ 
      //NOTE: csSQL must individually specify the column names using ' as '. 
      // The ' as ' MUST have a space before and after 'as'. see example.
      // example = "select spec as spec, descr as descr from ets_categorys where active = 1"
      //------------------------------------------------------------------------------------ 
      ArrayList alColNames = new ArrayList();
      Int32 nsqllen = csSQL.Length;
      if(nsqllen > 0)
      {
        csSQL = csSQL.ToUpper(); //make all upper case.
        Int32 nSelectPos = csSQL.IndexOf("SELECT");
        if(nSelectPos >= 0)
        {
          string csProcessSql = "";
          csProcessSql = csSQL.Substring(nSelectPos + 7); //"spec as spec, descr as descr from ets_categorys where active = 1"
          Int32 nFromPos = csProcessSql.IndexOf("FROM");
          if(nFromPos > 0)
          {
            csProcessSql = csProcessSql.Substring(0, nFromPos - 1); //"SPEC AS SPEC, DESCR AS DESCR" 
            string csColNameStatement = "";
            string csColName = "";
            int iAsPos = 0;
            int iloop = 0;
            int iNumAsString = NumStringMatchs(csProcessSql, " AS ");
            if(iNumAsString > 1)
            {
              string[] csSeperator = new string[1];
              csSeperator[0] = ",";
              string[] cAsNames = csProcessSql.Split(csSeperator, StringSplitOptions.RemoveEmptyEntries);
              Int32 iNumCnames = cAsNames.Length;
              if (iNumCnames > 0)
              {
                for (iloop = 0; iloop < iNumCnames; iloop++)
                {
                  csColNameStatement = cAsNames[iloop].Trim();
                  if (csColNameStatement.Length > 0)
                  {
                    iAsPos = csColNameStatement.IndexOf(" AS ");
                    if (iAsPos > 0)
                    {
                      csColName = csColNameStatement.Substring(iAsPos + 4);
                      alColNames.Add(csColName);
                    }
                  }
                }//iloop
              }//iNumCnames > 0
            }
            else
            {
              iAsPos = csProcessSql.IndexOf(" AS ");
              if (iAsPos > 0)
              {
                csColName = csProcessSql.Substring(iAsPos + 4);
                alColNames.Add(csColName);
              }
            }
          }//nFromPos > 0
        }//nSelectPos > 0
      }//nsqllen > 0
      return alColNames;
    }//extractColNamesFromSQL

    public static ArrayList extractDistinctValuesFromMultiStringArray(string[,] sarDepts, int xdistinctcolnum, int ydistinctcolnum, bool trimcolvaluebeforecompare, 
                                                                      bool uppercasevalue, bool removespecialchars, ref Int32 iNumItems)
    {
      ArrayList alDistinctValues = new ArrayList();
      iNumItems = 0;
      int xarraylen = sarDepts.GetLength(0);
      int yarraylen = sarDepts.GetLength(1);
      if((xarraylen > 0) && (yarraylen > 0))
      {
        if ((xdistinctcolnum <= (xarraylen - 1)) && (ydistinctcolnum <= (yarraylen - 1)))
        {
          int xloop = 0;
          string csColValue = "";
          string csPrevColValue = "";
          for(xloop = 0; xloop < xarraylen; xloop++)
          {
            csColValue = sarDepts[xloop, ydistinctcolnum];
            if (trimcolvaluebeforecompare)
            {
              csColValue = csColValue.Trim();
            }
            if(uppercasevalue)
            {
              csColValue = csColValue.ToUpper();
            }
            if(removespecialchars)
            {
              csColValue = System.Text.RegularExpressions.Regex.Replace(csColValue, "[@&.'(\\s)<>#]", "");
            }
            if (xloop == 0)
            {
              csPrevColValue = csColValue;
            }
            if(csPrevColValue != csColValue)
            {
              alDistinctValues.Add(csPrevColValue);
              csPrevColValue = csColValue;
            }
          }//end xloop
          if (csPrevColValue.Length > 0)
          {
            alDistinctValues.Add(csPrevColValue);
          }
        }//((xdistinctcolnum <= (xarraylen - 1)) && (ydistinctcolnum <= (yarraylen - 1))
      }//(xarraylen > 0) && (yarraylen > 0)
      iNumItems = alDistinctValues.Count;
      return alDistinctValues;
    }

    /// <summary>
    /// Nums the string matchs.
    /// </summary>
    /// <param name="sInString">The s in string.</param>
    /// <param name="sLookFor">The s look for.</param>
    /// <returns></returns>
    public static int NumStringMatchs(string sInString, string sLookFor)
    {
      int nNumMatchs = 0;
      int nLen = sInString.Length;
      int nLookForLen = sLookFor.Length;
      int nMaxLoop = nLen - nLookForLen;
      for (int x = 0; x <= nMaxLoop; x++)
      {
        if (sInString.Substring(x, nLookForLen) == sLookFor)
          nNumMatchs++;
      }//end x loop
      return nNumMatchs;
    }//end of NumCharMatchs

    // Name: ReplaceCharsInString
    //
    // This method replaces all of the matching SINGLE characters in the sReplaceCharString with the sNewStr
    // NOTE-sReplaceCharString and sNewStr must be only one character.
    //
    // On Entry:
    //  sIn - string to replace chars in
    //  sReplaceCharString - string that holds characters to match and replace with sNewStr.
    //  sNewStr - string to replace matching characters with.
    //
    // On Exit:
    //  sRtn - string containing new string with replaced characters
    //    

    /// <summary>
    /// Replaces the chars in string.
    /// </summary>
    /// <param name="sIn">The s in.</param>
    /// <param name="sReplaceCharString">The s replace char string.</param>
    /// <param name="sNewStr">The s new STR.</param>
    /// <returns></returns>
    public static string ReplaceCharsInString(string sIn, string sReplaceCharString, string sNewStr)
    {
      string sRtn = "";
      if (sReplaceCharString != "")
      {
        char[] cchars = sReplaceCharString.ToCharArray();
        if (sIn.IndexOfAny(cchars) >= 0)
        {
          int nLen = sIn.Length;
          for (int x = 0; x < nLen; x++)
          {
            if (sReplaceCharString.IndexOf(sIn.Substring(x, 1)) >= 0)
            {
              sRtn += sNewStr;
            }
            else
            {
              sRtn += sIn.Substring(x, 1);
            }
          }//end x loop
        }
        else
        {
          sRtn = sIn; //just return sIn, no sReplaceCharString characters found in sIn
        }
      }
      else
      {
        sRtn = sIn;
      }
      return sRtn;
    }//end of ReplaceCharsInString

    // Name: SaveFile
    // 
    // This method saves the arraylist myItems to the filename specified in sFileName
    //
    // On Entry:
    //  o sPath - path to save file to
    //  o sFileName - filename to save data items in
    //  o myItems - ArrayList of string items to save
    //  o sMyError - string to hold any error that occured
    //
    // On Exit:
    //   bRtn - true if file was saved, otherwise false
    //   sMyError - try/catch file error
    //
    // <b><i><u>Description</u></i></b><br>
    // The method will take 3 parameters.......
    //  o string sPath
    //  o string sFileName
    //  o ArrayList myItems 
    // <pre>
    // Additional Info:
    //  If the sPath does not exist it will be created
    //  If the sPath+sFileName already exists it will be deleted
    //  A CR/LF is appended to the string item being written
    // </pre>

    /// <summary>
    /// Saves the file.
    /// </summary>
    /// <param name="sPath">The s path.</param>
    /// <param name="sFileName">Name of the s file.</param>
    /// <param name="myItems">My items.</param>
    /// <param name="sMyWorkPath">The s my work path.</param>
    /// <param name="sMyError">The s my error.</param>
    /// <returns></returns>
    public static bool SaveFile(string sPath, string sFileName, ArrayList myItems, ref string sMyError)
    {
      bool bRtn = false;
      bool bAbort = false;
      sFileName.Trim();
      sPath.Trim();
      if (sFileName.Length >= 1 && sPath.Length >= 2)
      {
        string sItem = "";
        string sWorkPath = sPath;
        //create directory if it does not exist
        if (!DoesDirectoryExist(sWorkPath))
        {
          string sError = "";
          if (!CreateDirectory(sWorkPath, ref sError))
            bAbort = true;
        }
        if (!bAbort)
        {
          string sFullFilePath = sPath + "\\" + sFileName;
          if (DoesFileExist(sFullFilePath))
          {
            File.Delete(sFullFilePath);
          }
          try
          {
            FileStream fs = new FileStream(sFullFilePath, FileMode.Create, FileAccess.Write);
            StreamWriter swTextOut = new StreamWriter(fs);
            int nCount = myItems.Count;
            int xLoop = 0;
            for (xLoop = 0; xLoop < nCount; xLoop++)
            {
              sItem = myItems[xLoop].ToString();
              swTextOut.WriteLine(sItem);
            }//end xLoop
            swTextOut.Close();
            fs.Close();
            bRtn = true;
          }
          catch (IOException e)
          {
            sMyError = e.ToString();
          }
          finally
          {
          }
        }
      }//sFileName.length >= 2 check        
      return bRtn;
    }//end savefile

    // Name: DoesDirectoryExist
    // 
    // This method checks if the specified directory exists.
    //
    // On Entry:
    //   sDirectory - fully qualified path
    //
    // On Exit:
    //   bRtn - true if directory exists, otherwise false

    /// <summary>
    /// Does the directory exist.
    /// </summary>
    /// <param name="sDirectory">The s directory.</param>
    /// <returns></returns>
    public static bool DoesDirectoryExist(string sDirectory)
    {

      bool bRtn = false;
      if (Directory.Exists(sDirectory))
        bRtn = true;
      return bRtn;
    }//end of DoesDirectoryExist

    // Name: CreateDirectory
    // 
    // This method creates the specified directory if it does not already exist.
    //
    // On Entry:
    //   sDirectory - fully qualified path
    //   sError - Empty string to hold returned error
    //
    // On Exit:
    //   bRtn - true if directory created, otherwise false
    //   sError - holds any try/catch error
    //
    // Note: the global variable gv_sCommonException will hold any exception that is thrown by this method.
    //

    /// <summary>
    /// Creates the directory.
    /// </summary>
    /// <param name="sDirectory">The s directory.</param>
    /// <param name="sError">The s error.</param>
    /// <returns></returns>
    public static bool CreateDirectory(string sDirectory, ref string sError)
    {
      bool bRtn = false;
      sError = "";
      try
      {
        // Determine whether the directory exists
        if (!Directory.Exists(sDirectory))
        {
          // Try to create the directory
          DirectoryInfo di = Directory.CreateDirectory(sDirectory);
          bRtn = true;
        }
      }
      catch (Exception e)
      {
        sError = e.ToString();
      }
      finally //called no matter what - cleans up try-catch block
      {
      }
      return bRtn;
    }//end of CreateDirectory

    // Name: DeleteFile
    // 
    // This method deletes the specified sPath + "\\" + sFileName
    //
    // On Entry:
    //  o sPath - path to save file to
    //  o sFileName - filename to save data items in
    //
    // On Exit:
    //   bRtn - true if file was deleted, otherwise false
    //
    // <b><i><u>Description</u></i></b><br>
    // The method will take 2 parameters.......
    //  o string sPath
    //  o string sFileName
    //  o ArrayList myItems 
    // <pre>
    // Additional Info:
    //  sPath must not contain a trailing path seperator '\'
    // </pre>

    /// <summary>
    /// Deletes the file.
    /// </summary>
    /// <param name="sPath">The s path.</param>
    /// <param name="sFileName">Name of the s file.</param>
    /// <returns></returns>
    public static bool DeleteFile(string sPath, string sFileName)
    {
      bool bRtn = false;
      sFileName.Trim();
      sPath.Trim();
      if (sFileName.Length >= 1 && sPath.Length >= 2)
      {
        string sWorkPath = sPath;
        string sFullFilePath = sPath + "\\" + sFileName;
        if (DoesFileExist(sFullFilePath))
        {
          try
          {
            File.Delete(sFullFilePath);
            bRtn = true;
          }
          catch (Exception e)
          {
            string smsg = e.Message.ToString();
            bRtn = false;
          }
        }
      }//sFileName.Length >= 1 && sPath.Length > 2     
      return bRtn;
    }//end DeleteFile

    // Name: DoesFileExist
    // 
    // This method checks if the specified file exists.
    //
    // On Entry:
    //   sFullFileName - fully qualified path and filename (c:\mypath\myfilename.txt)
    //
    // On Exit:
    //   bRtn - true if file exists, otherwise false
    //

    /// <summary>
    /// Does the file exist.
    /// </summary>
    /// <param name="sFullFileName">Name of the s full file.</param>
    /// <returns></returns>
    public static bool DoesFileExist(string sFullFileName)
    {
      bool bRtn = false;
      sFullFileName.Trim();
      if (File.Exists(sFullFileName))
        bRtn = true;
      return bRtn;
    }//end of DoesFileExist


    // Name: GetFile
    // 
    // This method loads into the ref ArrayList text file data. 
    //
    // On Entry:
    //  o sFullFileName - full file name to the file ie: c:\mydirectory\myfilename.txt
    //  o alData - ArrayList that will be populated with file data
    //
    // On Exit:
    //   nRtn - Number of items loaded into the alData arraylist
    //
    // <b><i><u>Description</u></i></b><br>
    // The method will open the sequential text file specified and load into the 
    // ArrayList alData the cr/lf delimited data.
    //

    /// <summary>
    /// Gets the file.
    /// </summary>
    /// <param name="sFullFileName">Name of the s full file.</param>
    /// <param name="alData">The al data.</param>
    /// <returns></returns>
    public static int GetFile(string sFullFileName, ref ArrayList alData)
    {
      int nRtn = 0;
      sFullFileName.Trim();
      if (sFullFileName.Length > 2) //must be greater then 2 chars. ie: c:\ = 3 chars
      {
        int nArraySize = alData.Count;
        if (nArraySize > 0)
        {
          alData.Clear();
        }
        bool bStatus = false;
        bStatus = DoesFileExist(sFullFileName);
        if (bStatus)
        {
          using (StreamReader sr = File.OpenText(sFullFileName))
          {
            string input;
            while ((input = sr.ReadLine()) != null)
            {
              alData.Add(input);
              nRtn++;
            }
            //Console.WriteLine("The end of the stream has been reached.");
            sr.Close();
          }
        }//bStatus check
      }//sFullFileName.Length > 2 check
      return nRtn;
    }//end of GetFile

    // Name: ExtractDrivePathFile
    // 
    // This method will scan the directory path string and return the Drive, Path and Filename
    //
    // On Entry:
    //   sFileSpec - fully qualified filename path ie:
    //
    // On Exit:
    //   bRtn - true if successful, otherwise false.
    //   sDrive - drive letter
    //   sPath - path
    //   sFile - file name
    //

    /// <summary>
    /// Extracts the drive path file.
    /// </summary>
    /// <param name="sFileSpec">The s file spec.</param>
    /// <param name="sDrive">The s drive.</param>
    /// <param name="sPath">The s path.</param>
    /// <param name="sFile">The s file.</param>
    /// <returns></returns>
    public static bool ExtractDrivePathFile(string sFileSpec, ref string sDrive, ref string sPath, ref string sFile)
    {
      bool bRtn = true;
      sFileSpec = sFileSpec.Trim();
      sDrive = "";
      sPath = "";
      sFile = "";
      string sDelim = "\\";
      if (sFileSpec.IndexOf("/") >= 0)
      {
        //change path delimiter to / vice \
        sDelim = "/";
      }
      //first extract drive
      int ndrives = NumCharMatchs(sFileSpec, ":");
      if (ndrives > 1) //more than one : not allowed
      {
        bRtn = false;
        return bRtn;
      }
      int dpos = sFileSpec.IndexOf(':');
      if (dpos == 1) //ok get it   
      {
        sDrive = sFileSpec.Substring(0, 2);
        dpos++;
      }
      else
        dpos = 0; //default dpos to front of filespec string
      int spos = sFileSpec.IndexOf(sDelim);
      if (spos >= 0) //yes there is some path information
      {
        int lpos = ScanStringFromEnd(sFileSpec, sDelim, ref sFile, 0);
        if (sFile.Length > 0)
        {
          //see if this is a valid file spec xxxx.ext  must have '.' 
          if (sFile.IndexOf(".") == -1) //no, so set lpos to end of string
          {
            sFile = "";
            lpos = sFileSpec.Length - 1;
          }
          else
            sFile = sFile.Substring((sFile.Length - (sFile.Length - 1)), sFile.Length - 1);
        }
        if (lpos > dpos)
          sPath = sFileSpec.Substring(dpos, ((lpos - dpos) + 1)); //ExtractString(dpos, lpos, csFileSpec, csPath);
      }
      return bRtn;
    }//end of ExtractDrivePathFile

    // Name: ScanStringFromEnd
    // 
    // This method scans the instring starting at the end of the string and looks for the sLookFor character
    //
    // On Entry:
    //  o instring = string to perform action on
    //  o sLookFor = single string character to look for and stop the scan
    //  o nStartPos = if zero the starting scan starts at the end of the string otherwise it starts at nStartPos
    //
    // On Exit:
    //  o nRtn = found postion within instring of sLookFor character string
    //  o resultstring = string made up from the point of the start scan to the found stop position

    /// <summary>
    /// Scans the string from end.
    /// </summary>
    /// <param name="instring">The instring.</param>
    /// <param name="sLookFor">The s look for.</param>
    /// <param name="resultstring">The resultstring.</param>
    /// <param name="nStartPos">The n start pos.</param>
    /// <returns></returns>
    public static int ScanStringFromEnd(string instring, string sLookFor, ref string resultstring, int nStartPos)
    {
      int nRtn = -1;
      string foundstr = "", work = "";
      resultstring = "";
      if (sLookFor.Length <= 0)
        return nRtn;
      //8/28/06 mkj removed the -1 from instring.Length. function was not returning full extension when extensions len > 3, ie: xyz.args;
      int nEndPos = instring.Length;
      if ((nStartPos > 0) && (nStartPos < nEndPos))
        nEndPos = nStartPos; //start at passed in position  
      int nCount = 0;
      for (int i = nEndPos; i > -1; i--)  // zero based
      {
        if (nCount > 0)
        {
          foundstr += instring[i].ToString();
          work = foundstr;
          work = MakeReverseString(foundstr);
          //work.MakeReverse();
          if (work.IndexOf(sLookFor) != -1) // found lookfor string
          {
            foundstr = MakeReverseString(foundstr); //.MakeReverse();
            resultstring = foundstr;
            nRtn = i;
            break;
          }
        }
        nCount++;
      }
      return nRtn;
    }//end of ScanStringFromEnd


    // Name: MakeReverseString
    // 
    // This method reverses the sIn string character by character. example: "Hi there" becomes "ereht iH"
    //
    // On Entry:
    //  o sIn = string to perform action on
    //
    // On Exit:
    //  o sRtn = string that was reversed 

    /// <summary>
    /// Makes the reverse string.
    /// </summary>
    /// <param name="sIn">The s in.</param>
    /// <returns></returns>
    public static string MakeReverseString(string sIn)
    {
      string sRtn = "";
      int nStart = sIn.Length - 1;
      for (int iLoop = nStart; iLoop > -1; iLoop--)
        sRtn += sIn[iLoop].ToString();
      return sRtn;
    }//end of MakeReverseString

    public static string createSQLInStrFromArrayList(ref ArrayList alIDs)
    {
      string csIDsInStr = "";
      Int32 nNumIds = alIDs.Count;
      if(nNumIds > 0)
      {
        Int32 nloop = 0;
        Int32 nloopmax = nNumIds;
        string csloopdata = "";
        for(nloop = 0; nloop < nloopmax; nloop++)
        {
          csloopdata = alIDs[nloop].ToString().Trim();
          if (isStringANumber(csloopdata))
          {
            csIDsInStr += csloopdata;
            csIDsInStr += ",";
          }
        }//end loop
        char[] charsToTrim = {','};
        csIDsInStr = csIDsInStr.TrimEnd(charsToTrim);
      }
      return csIDsInStr;
    }

    public static bool isStringANumber(string csNumber)
    {
      bool isNumber = false;
      isNumber = int.TryParse(csNumber, out int n);
      return isNumber;
    }

    /// <summary>
    /// Gets the julian date value.
    /// </summary>
    /// <param name="sJDValue">The s JD value.</param>
    /// <returns></returns>
    public static double GetJulianDateValue(ref string sJDValue, Int32 nFromYear)
    {
      //nFromYear = 1970, 1980, 1990
      double dJDValue = 0.0f;
      DateTime dt1990 = new DateTime(nFromYear, 1, 1, 0, 0, 0);
      DateTime dtValue;
      if (sJDValue.Length > 0)
      {
        dtValue = Convert.ToDateTime(sJDValue);
      }
      else
      {
        dtValue = DateTime.Now;
      }
      TimeSpan tsDiff = dtValue - dt1990;
      dJDValue = tsDiff.TotalSeconds; //		dTotSecs	537722618.40183067	double
      sJDValue = Convert.ToString(dJDValue);

      return dJDValue;
    }// end of GetJulianDateValue

    public static string mjcEscapeSQL(string csSQL)
    {
      string csEscapedSQL = csSQL;
      /*
        public static char cdblqt = Convert.ToChar(34);          "
        public static char cdblqtesc = Convert.ToChar(189);      "
        public static char csglqt = Convert.ToChar(39);          '
        public static char csglqtesc = Convert.ToChar(169);      '
        public static char ccomma = Convert.ToChar(44);          ,
        public static char ccommaesc = Convert.ToChar(174);      ,
        public static char camp = Convert.ToChar(38);            &
        public static char campesc = Convert.ToChar(188);        &
        public static char ccr = Convert.ToChar(13);             cr
        public static char ccresc = Convert.ToChar(191);         cr
        public static char clf = Convert.ToChar(10);             lf
        public static char clfesc = Convert.ToChar(190);         lf
      */
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, cdblqt.ToString(), cdblqtesc.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, csglqt.ToString(), csglqtesc.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, ccomma.ToString(), ccommaesc.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, camp.ToString(), campesc.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, ccr.ToString(), ccresc.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, clf.ToString(), clfesc.ToString());
      return csEscapedSQL;
    }

    public static string mjcUnEscapeSQL(string csSQL)
    {
      string csEscapedSQL = csSQL;
      /*
        public static char cdblqt = Convert.ToChar(34);          "
        public static char cdblqtesc = Convert.ToChar(189);      "
        public static char csglqt = Convert.ToChar(39);          '
        public static char csglqtesc = Convert.ToChar(169);      '
        public static char ccomma = Convert.ToChar(44);          ,
        public static char ccommaesc = Convert.ToChar(174);      ,
        public static char camp = Convert.ToChar(38);            &
        public static char campesc = Convert.ToChar(188);        &
        public static char ccr = Convert.ToChar(13);             cr
        public static char ccresc = Convert.ToChar(191);         cr
        public static char clf = Convert.ToChar(10);             lf
        public static char clfesc = Convert.ToChar(190);         lf
      */
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, cdblqtesc.ToString(), cdblqt.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, csglqtesc.ToString(), csglqt.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, ccommaesc.ToString(), ccomma.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, campesc.ToString(), camp.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, ccresc.ToString(), ccr.ToString());
      csEscapedSQL = common.ReplaceCharsInString(csEscapedSQL, clfesc.ToString(), clf.ToString());
      return csEscapedSQL;
    }

    /*
function getRequestIDsInString ( binds = [], opts = {} )
{
gs_functioncall_status = 'ERROR'
let reqidinstring = '';
let sql = '';
let result = null;
let reqidstr = '';
let reqid = 0;
return new Promise(async(resolve, reject) => {
let conn;
opts.outFormat = oracledb.OBJECT;
opts.autoCommit = true;
try {
  conn = await oracledb.getConnection(gs_poolaliasname);
  sql = "select id as id from ets.ets_requests_master where active = 1";
  result = await conn.execute(sql, binds, opts);
  if(result)
  {
    let extractedData = mjc.extractRowData(result, "id", ",");
    if (extractedData != undefined)
    {
      let datalen = extractedData.length;
      if (datalen > 0)
      {
        let xloop = 0;
        for (xloop = 0; xloop < datalen; xloop++)
        {
          reqidstr = extractedData[ xloop ].toString();
          reqid = mjc.convertStringToNumberAbsolute(reqidstr);
          if(reqid > 0)
          {
            reqidinstring += reqid.toString();
            reqidinstring += ',';
          }
        }//end xloop
        if(reqidinstring != undefined)
        {
          let instringlen = reqidinstring.length;
          if(instringlen > 0)
          {
            let lastchar = reqidinstring.substr((instringlen - 1), 1);
            if(lastchar === ',')
            {
              reqidinstring = reqidinstring.substr(0, (instringlen - 2));
            }
          }
        }
      } //datalen > 0
    } //extractedData != undefined
  }//if(result)
  gs_functioncall_status = 'SUCCESS'
  resolve(reqidinstring);
}
catch ( err )
{
  gs_functioncall_status = err.message + " | " + sql
  resolve(gs_functioncall_status);
}
finally
{
  if ( conn )
  { // conn assignment worked, need to close
    try
    {
      await conn.close();
    }
    catch ( err )
    {
      reject(err.message + " | " + sql);
    }
  }
}
});
}//getRequestIDsInString    */


  }//end class
}//end namespace
