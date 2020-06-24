using Microsoft.VisualBasic;
using NetCommonUtilities;
using NetCommonUtilities.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using System.Threading.Tasks;


namespace NetDataMaskingLib
{
    public class NetDataMaskingLib
    {
        private char randomChar;
        private int randomInt;
        private Random randomValue;
        private StringBuilder objStringBuilder;
        private const string defaultVal = "default";
        Dictionary<string, string> dicMaskedVariable = new Dictionary<string, string>();
        const string printImageIdentifier = "MAIL-TO NAME AND ADDRESS";
        const string printImageSDRecDelimiter = "Å";
        List<string> recordRepeatlist = new List<string>();
        public NetDataMaskingLib()
        {
            dicMaskedVariable.Clear();
            objStringBuilder = new StringBuilder();
            randomValue = new Random();
        }

        public FileProcessingModel DataMaskingFileProcessing(DataSet dsConfigSource, string FileNameWithPath, string MetaFileNameWithPath, FileProcessingModel model)
        {
            try
            {
              if (model.FileExtensionType == Enums.FileExtnType.PrintImageSD)
                {
                    PrintImageSD_FileProcessing(dsConfigSource, FileNameWithPath, MetaFileNameWithPath, model);
                }
            }
            catch (Exception Ex)
            {
                model = null;
                throw Ex;
            }
            return model;
        }
        private void PrintImageSD_FileProcessing(DataSet Dsconfigurations, string FileNameWithPath, string MetaFileNameWithPath, FileProcessingModel model)
        {
            bool isFileValid = true;
            string ReordType = string.Empty, FileExtensionTargets = string.Empty;
            (string workflowGroup, string productNum, string subType) = GetXmlNodesValue(MetaFileNameWithPath);
             DataTable GetDataTable = new DataTable();
            GetDataTable.Clear();
            GetDataTable = GetConsolidatedConfiguration(Dsconfigurations);
            if (model.FileExtension.ToString().ToUpper() == Enums.FileExtn.TXT.ToString())
            {
                FileExtensionTargets = Enums.FileExtn.TXT.ToString();
                ReordType = (GetDataTable.AsEnumerable().Where(p => p.Field<string>("FileExtensionTargets") == Enums.FileExtn.TXT.ToString()
               ).Select(p => p.Field<string>("RecordType"))).FirstOrDefault();
            }
            StringBuilder sbText = new StringBuilder();
            string countValue = GetDelimiterCount(FileNameWithPath);
            string[] countVal = countValue.Split(' ');
            int[] printImageIdentifierPosition = new int[Convert.ToInt32(countVal[0])];
            int[] specialCharacterArray = new int[Convert.ToInt32(countVal[1])];
            List<ClientDataPageWise> clientDataList = new List<ClientDataPageWise>();
            recordRepeatlist.Clear();
            using (StreamReader file = new StreamReader(new FileStream(FileNameWithPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), Encoding.Default))
            {
                List<string> fileRows = new List<string>();
                int counter = 0;
                string ln;
                dicMaskedVariable.Clear();
                int indexval = 0;
                int specialcharacterIndexVal = 0;
                int linesCount = 0;
                //Getting the Index of Mail_to_Address and special character
                while ((ln = file.ReadLine()) != null)
                {
                    fileRows.Add(ln.TrimEnd());
                    int linenumber = GetIndexValue(printImageIdentifier, ln);
                    counter++;
                    if (linenumber != -1)
                    {
                        printImageIdentifierPosition[indexval] = counter;
                        indexval++;
                    }
                    int specialCharacterLineNumber = GetIndexValue(printImageSDRecDelimiter, ln);
                    if (specialCharacterLineNumber != -1)
                    {
                        specialCharacterArray[specialcharacterIndexVal] = counter;
                        specialcharacterIndexVal++;
                    }
                    linesCount++;
                }
                //splliting the page client wise
                if (linesCount != 0 && indexval != 0 && printImageIdentifierPosition.Length != 0)
                {
                    List<List<string>> clientRecord = new List<List<string>>();
                    string[] clientArray = new string[printImageIdentifierPosition.Length + 1];
                    int ther = 0;
                    for (int count = 0; count <= printImageIdentifierPosition.Length + 1; count++)
                    {
                        if (count == (int)Enums.RecordType.Initial)
                        {
                            clientRecord.Add(fileRows.GetRange(count + 1, printImageIdentifierPosition[0] - 14));
                        }
                        else if (count == printImageIdentifierPosition.Length)
                        {
                            int loc = ((specialCharacterArray[specialCharacterArray.Length - 1]) - (printImageIdentifierPosition[count - 1] - 14));
                            clientRecord.Add(fileRows.GetRange(printImageIdentifierPosition[count - 1] - 14, loc - 1));
                        }
                        else if (count == printImageIdentifierPosition.Length + 1)
                        {
                            int loc = ((linesCount) - (specialCharacterArray[specialCharacterArray.Length - 1]));
                            clientRecord.Add(fileRows.GetRange(specialCharacterArray[specialCharacterArray.Length - 1], loc));
                        }
                        else
                        {
                            int loc = ((printImageIdentifierPosition[count] - 14) - (printImageIdentifierPosition[count - 1] - 14));
                            clientRecord.Add(fileRows.GetRange(printImageIdentifierPosition[count - 1] - 14, loc));
                        }
                        ther += clientRecord[count].Count;
                    }
                    int clientRecordCount = 0;
                    int clientcount = 0;
                    foreach (List<string> clientRec in clientRecord)
                    {
                        ClientDataPageWise ObjClientDataPageWise = new ClientDataPageWise();
                        string outPut = string.Join(Environment.NewLine, clientRec.ToArray());
                        string[] pages = outPut.Split(Convert.ToChar(printImageSDRecDelimiter));
                        int count = 0;
                        for (int i = 0; i < pages.Length; i++)
                        {
                            if ((clientcount == 0 && i == 0))
                            {
                                pages[i] = printImageSDRecDelimiter + "\r" + pages[i];
                            }
                            else if (clientcount == clientRecord.Count - 1)
                            {
                                pages[i] = printImageSDRecDelimiter + "\r\n" + pages[i];
                            }
                            else if (pages[i].Trim() != "")
                            {
                                pages[i] = printImageSDRecDelimiter + pages[i];//.Replace("�", "");
                            }
                            if (pages[i].Trim() != "")
                                ObjClientDataPageWise.Pages.Add(pages[i]);

                            count++;
                            clientRecordCount++;
                        }
                        clientcount++;
                        clientDataList.Add(ObjClientDataPageWise);
                    }
                }
                else
                {
                    isFileValid = false;
                }
                bool isDescreptionPages = false;
                int customerRecordNo = 1;
                int isAccountValid = 0;
                if (isFileValid)
                {
                    foreach (var page in clientDataList)
                    {
                        if (!isFileValid)
                        {
                            break;
                        }
                        if (customerRecordNo == (int)Enums.RecordType.First)
                        {
                            isDescreptionPages = true;
                        }
                        else
                        {
                            isDescreptionPages = false;
                        }
                        customerRecordNo++;
                        int customerPageCount = 1;
                        foreach (var row in page.Pages)
                        {
                            int totalPageLines = CountLinesInFile(row.ToString());
                            StringBuilder maskedLine = new StringBuilder();
                            var tempread = new StringReader(row);
                            if (isDescreptionPages)
                            {
                                sbText.AppendLine(row.ToString().TrimEnd());
                            }
                            else
                            {
                                for (int currentRowofPage = 1; currentRowofPage <= totalPageLines; currentRowofPage++)
                                {
                                    string unMaskedLine = tempread.ReadLine();
                                    maskedLine = GeneratePrintImageSDMaskedData(unMaskedLine, Dsconfigurations, MetaFileNameWithPath, customerPageCount, Convert.ToString(currentRowofPage), ref isAccountValid);
                                    sbText.AppendLine(maskedLine.ToString().TrimEnd());
                                }
                            }
                            customerPageCount++;
                        }
                    }

                    file.Close();
                }

                if (customerRecordNo - 3 == isAccountValid)
                {
                    isFileValid = false;
                }
            }
            // Full Scanning the document for repeated string
            StringBuilder sbFinal = new StringBuilder();
            sbFinal.Clear();
            if (isFileValid)
            {
                int linesss = sbText.Length;
                String[] arrFinal = sbText.ToString().TrimEnd().Split('\n');
                bool isDescrptionPageExists = true;
                foreach (string finalRow in arrFinal)
                {
                    if (!isDescrptionPageExists)
                    {
                        StringBuilder scanLine = new StringBuilder(finalRow);
                        List<string> dicPreMasked = recordRepeatlist.OrderByDescending(s => s.Length).ToList();
                        Parallel.ForEach(dicPreMasked, record =>
                        {
                            string[] repeatString = record.Split(new[] { ' ', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            string[] lineCheck = finalRow.Trim('\r').Split(new Char[] { ' ', '-' });
                            StringBuilder stringMatched = new StringBuilder();
                            List<string> repeatLintMatch = new List<string>();
                            foreach (string dictionay in repeatString)
                            {
                                if (finalRow.Contains(dictionay))
                                {
                                    foreach (string currentScanWord in lineCheck)
                                    {
                                        if (currentScanWord == dictionay)
                                        {
                                            repeatLintMatch.Add(dictionay);
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                         
                            foreach (string value in repeatLintMatch)
                            {
                                stringMatched.Append(value + ' ');
                            }
                            string finalMatch = stringMatched.ToString().TrimEnd().TrimStart();
                            string originalArray = ConvertStringArrayToString(repeatString).TrimEnd();
                            if (finalMatch.Length == originalArray.Length)
                            {
                                string finalMaskedString = GetMaskedFieldValue(originalArray);
                                string[] maskedarray = finalMaskedString.Split(' ');
                                Dictionary<string, string> unMaskedAndMaskedDict = new Dictionary<string, string>();
                                for (int unm = 0; unm < repeatString.Length; unm++)
                                {
                                    unMaskedAndMaskedDict.Add(repeatString[unm], maskedarray[unm]);
                                }
                                foreach (string final in finalMatch.Split(' '))
                                {
                                    scanLine.Replace(final, unMaskedAndMaskedDict[final]);
                                }
                            }
                            else if (finalMatch.Length > originalArray.Length)
                            {
                                scanLine.Replace(originalArray, GetMaskedFieldValue(originalArray));
                            }
                        });
                        sbFinal.AppendLine(scanLine.ToString().TrimEnd());
                    }
                    else
                    {
                        if (finalRow.Contains(printImageIdentifier))
                        {
                            isDescrptionPageExists = false;
                        }
                        sbFinal.AppendLine(finalRow.TrimEnd());
                    }
                }
            }
            else
            {
                sbFinal = null;
            }
            if (!isFileValid)
            {
                sbFinal = null;
            }
            using (MemoryStream fileStream = new MemoryStream())
            {
                fileStream.Write(Encoding.Default.GetBytes(sbFinal.ToString()), 0, Encoding.Default.GetBytes(sbFinal.ToString()).Length);
                model.OutputDataFileBytes = fileStream.ToArray();
            }
            using (MemoryStream metaStream = new MemoryStream())
            {
                metaStream.Write(Encoding.Default.GetBytes(sbFinal.ToString()), 0, Encoding.Default.GetBytes(sbFinal.ToString()).Length);
                model.OutputMetaDataFileBytes = metaStream.ToArray();
            }
            model.HasOutputErrors = (model.OutputDataFileBytes == null || model.OutputMetaDataFileBytes == null ? true : false);
            model.HasOutputFileDetails = (model.OutputDataFileBytes == null || model.OutputMetaDataFileBytes == null ? false : true);

        }
        public static int CountLinesInFile(string arrayString)
        {
            int count = 0;
            string countlinestest = "\n";
            string[] test = new string[] { countlinestest };
            string[] finalline = arrayString.TrimEnd().Split(test, StringSplitOptions.None);
            count = finalline.Count();
            return count;
        }
    
        public static int GetIndexValue(string text, string lineToFind, StringComparison comparison = StringComparison.CurrentCulture)
        {
            int lineNum = 0;
            using (StringReader reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNum++;
                    if (lineToFind.Contains(text))
                        return lineNum;
                }
            }
            return -1;
        }
        public static string GetDelimiterCount(string path)
        {
            string ln;
            using (StreamReader file = new StreamReader(path, Encoding.Default))
            {
                int i = 0;
                int j = 0;
                while ((ln = file.ReadLine()) != null)
                {
                    if (ln.Contains(printImageIdentifier))
                        i++;
                    if (ln.Contains(printImageSDRecDelimiter))
                        j++; ;
                }
                file.Close();
                file.Dispose();
                return i.ToString() + " " + j.ToString();

            }


        }

         public string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(' ');
            }
            return builder.ToString();
        }
        private StringBuilder GeneratePrintImageSDMaskedData(string unMaskedLines, DataSet dsConfigSource, string MetaFileNameWithPath, int PageNo, string RowNo, ref int isAccountValid)
        {
            StringBuilder maskedString = new StringBuilder();
            StringBuilder maskedlines = new StringBuilder();
            StringBuilder sbLine = new StringBuilder(unMaskedLines);
            DataTable dtConfig = GetConsolidatedConfiguration(dsConfigSource);
            bool isMasked = false;
            (string workflowGroup, string productNum, string subType) = GetXmlNodesValue(MetaFileNameWithPath);


            var rows = from myRow in dtConfig.AsEnumerable().Where(x => x.Field<string>("FileExtnType").ToString() == Convert.ToString(Enums.FileExtnType.PrintImageSD) && x.Field<string>("SubType").ToString() == subType.ToUpper().Trim()
                      && x.Field<int>("PageNumber") == PageNo)
                       select myRow;

            if (!isMasked)
            {
                foreach (DataRow dr in rows)
                {
                    string[] arRowOffSet = Convert.ToString(dr["RowNo"]).Split(',');
                    int startPos = Convert.ToInt32(dr["StartPos"]);
                    string rowOffSet;
                    int pageNumber = Convert.ToInt32(dr["PageNumber"]);
                    string fieldLength = dr["FieldLength"].ToString();
                    if (arRowOffSet.Any(x => x == Convert.ToString(RowNo)))
                    {
                        rowOffSet = Array.Find(arRowOffSet, ele => ele.Equals(Convert.ToString(RowNo)));
                    }
                    else
                    {
                        rowOffSet = "0";
                    }
                    bool checkRecordLenType = int.TryParse(Convert.ToString(fieldLength), out int recordLength);
                    if (pageNumber == PageNo && Convert.ToInt32(rowOffSet) == Convert.ToInt32(RowNo))
                    {
                        isMasked = true;
                        string readPositionedData = "";
                        int startPosition = startPos - 1;
                        int dataLength = unMaskedLines.Length - startPosition;
                        if (checkRecordLenType)
                        {
                            if (unMaskedLines.Length > 0)
                            {
                              
                                if (dataLength > 0)
                                {
                                    if (dataLength < recordLength)
                                    {
                                        readPositionedData = unMaskedLines.Substring(startPosition, dataLength).TrimEnd();
                                        recordLength = dataLength;
                                    }
                                    else
                                    {
                                        readPositionedData = unMaskedLines.Substring(startPosition, recordLength).TrimEnd();
                                    }
                                }
                                else
                                {
                                    readPositionedData = "";
                                }
                            }
                            else
                                readPositionedData = "";
                        }
                        else
                        {
                            if (unMaskedLines.Length > 0)
                            {
                                if (dataLength > 0)
                                {
                                    readPositionedData = unMaskedLines.Substring(startPosition, dataLength).TrimEnd();
                                    recordLength = dataLength;
                                }
                                else
                                {
                                    readPositionedData = "";
                                }
                            }
                            else
                                readPositionedData = "";
                        }
                        if (PageNo == 2 && readPositionedData == "")
                        {
                            isAccountValid = isAccountValid + 1;
                        }
                        if (readPositionedData != "")
                        {
                            string maskedValue = GetMaskedFieldValue(readPositionedData);
                            sbLine.Replace(readPositionedData, maskedValue, startPosition, recordLength);
                            if (!recordRepeatlist.Contains(readPositionedData))
                            {
                                recordRepeatlist.Add(readPositionedData.TrimEnd());
                            }
                            break;
                        }
                    }
                    else
                    {
                        isMasked = false;
                    }
                }
            }
      
            if (isMasked)
                maskedString = sbLine;
            else
                maskedString = new StringBuilder(unMaskedLines);

            return maskedString;
        }
    
    
        private DataTable GetConsolidatedConfiguration(DataSet ds)
        {
            DataTable dtConfig = new DataTable();
            dtConfig.Clear();
            dtConfig.Columns.Add("SourceId", typeof(int));
            dtConfig.Columns.Add("FileExtensionTargets", typeof(string));
            dtConfig.Columns.Add("SubType", typeof(string));
            dtConfig.Columns.Add("RecordType", typeof(string));
            dtConfig.Columns.Add("SettingName", typeof(string));
            dtConfig.Columns.Add("StartPos", typeof(int));
            dtConfig.Columns.Add("FieldLength", typeof(string));
            dtConfig.Columns.Add("FileExtnType", typeof(string));
            dtConfig.Columns.Add("DelimitedColumns", typeof(string));
            dtConfig.Columns.Add("Workflow", typeof(string));
            dtConfig.Columns.Add("Product", typeof(string));
            dtConfig.Columns.Add("RowNo", typeof(string));
            dtConfig.Columns.Add("PageNumber", typeof(int));

            DataTable dt1 = ds.Tables["Source"];
            DataTable dt2 = ds.Tables["Configuration"];
            var results = from table1 in dt1.AsEnumerable()
                          join table2 in dt2.AsEnumerable() on (int)table1["SourceId"] equals (int)table2["SourceId"]
                          select dtConfig.LoadDataRow(new object[]
                         {
                                table1.Field<int>("SourceId")!= 0 ? table2.Field<int>("SourceId") : 0,
                                table1.Field<string>("FileExtensionTargets"),
                                table1.Field<string>("SubType"),
                                table1.Field<string>("RecordType"),
                                table2.Field<string>("SettingName"),
                                table2.Field<int?>("StartPos") != null ? table2.Field<int>("StartPos") : 0 ,
                                table2.Field<string>("FieldLength"),
                                table1.Field<string>("FileExtnType"),
                                table2.Field<string>("DelimitedColumns"),
                                table2.Field<string>("Workflow"),
                                table2.Field<string>("Product"),
                                table2.Field<string>("RowNo"),
                                table2.Field<int?>("PageNumber") != 0 ? table2.Field<int>("PageNumber") : 0


                          }, false);

            if (results.Count() > 0)
            {
                dtConfig = results.CopyToDataTable();
            }

            return dtConfig;
        }
        private string GetMaskedFieldValue(string fieldValue)
        {
            if (IsWhiteSpace(fieldValue)) return fieldValue;
            if (!dicMaskedVariable.ContainsKey(fieldValue.Trim().ToUpper()))
            {
                objStringBuilder.Clear();
                foreach (char ch in fieldValue)
                {
                    if (Char.IsLetter(ch))
                    {
                        do
                        {
                            randomChar = (char)randomValue.Next('A', 'Z');
                        }
                        while (char.ToUpper(ch) == randomChar);
                        objStringBuilder.Append(randomChar);
                    }
                    else if (Char.IsNumber(ch))
                    {
                        do
                        {
                            randomInt = randomValue.Next(0, 9);
                        }
                        while ((int)Char.GetNumericValue(ch) == randomInt);
                        objStringBuilder.Append(randomInt);
                    }
                    else if (!Char.IsLetterOrDigit(ch) && !Char.IsWhiteSpace(ch))
                    {
                        do
                        {
                            randomInt = randomValue.Next(0, 9);
                        }
                        while ((int)Char.GetNumericValue(ch) == randomInt);
                        objStringBuilder.Append(randomInt);
                    }
                    else
                    {
                        objStringBuilder.Append(ch);
                    }
                }
                dicMaskedVariable.Add(fieldValue.Trim().ToUpper(), objStringBuilder.ToString().Trim());
            }
            return dicMaskedVariable[fieldValue.Trim().ToUpper()].ToString();
        }
      
        private bool IsWhiteSpace(string field)
        {
            foreach (char ch in field)
            {
                if (!Char.IsWhiteSpace(ch))
                    return false;
            }
            return true;
        }

        //private (string workflowGroup, string productNum, string quantity, string subType, string recLength, string conversion) GetXmlNodesValue(string MetaFileNameWithPath)
        //{
        private (string workflowGroup, string productNum, string subType) GetXmlNodesValue(string MetaFileNameWithPath)
        {
            XmlDocument xml = new XmlDocument();
            string workflowGroup = string.Empty;
            string productNum = string.Empty;
            //string quantity = string.Empty;
            string subType = string.Empty;
            //string recLength = string.Empty;
            //string conversion = string.Empty;
            string noRecord = "NoRecord";
            try
            {
                xml.Load(MetaFileNameWithPath);
                workflowGroup = xml.SelectSingleNode("//WFG")?.InnerText ?? noRecord;
                productNum = xml.SelectSingleNode("//PRODNUM")?.InnerText ?? noRecord;
                //quantity = xml.SelectSingleNode("//NUMACCTS")?.InnerText ?? noRecord;
                subType = xml.SelectSingleNode("//SUBTYPE")?.InnerText ?? noRecord;
                //recLength = xml.SelectSingleNode("//RECLEN")?.InnerText ?? "0";
                //conversion = xml.SelectSingleNode("//CONVERSION")?.InnerText ?? noRecord;
            }
            catch (Exception ex)
            {
            }
            return (workflowGroup, productNum, subType);
            //return (workflowGroup, productNum, quantity, subType, recLength, conversion);//
        }
        public (string processor, string fileType) GetMetaXmlFileProcessor(string MetaFileNameWithPath)
        {
            XmlDocument xml = new XmlDocument();
            string processor = string.Empty;
            string fileType = string.Empty;
            try
            {
                xml.Load(MetaFileNameWithPath);
                processor = xml.SelectSingleNode("//PROCESSOR")?.InnerText ?? "";
                fileType = xml.SelectSingleNode("//FILETYPE")?.InnerText ?? "";
            }
            catch (Exception ex)
            {
            }
            return (processor, fileType);
        }
     }
}
