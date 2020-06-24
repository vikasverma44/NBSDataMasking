using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCommonUtilities
{
    public static class Enums
    {
        [System.Flags]
        public enum LogLevel
        {
            TRACE,
            INFO,
            DEBUG,
            WARNING,
            ERROR,
            FATAL
        }
        public enum SFTP_Site
        {
            Default = 0,
            Input = 1,
            Output = 2,
        }

        #region File Extentsion & Type Mapping - REQUIRED
        public enum FileExtn
        {
           TXT
        }
        private enum FileExtnMapping
        {
          TXT = FileExtnType.PrintImageSD,
        }
        public enum FileExtnType
        {
          PrintImageSD = 0
        }

        #endregion

        public enum FormMode
        {
            Add,
            Edit
        }
        public enum RecordType
        {
            Initial = 0,
            First=1,
            Second = 2,
            Three=3,
            Four=4,
            Five=5
           
        }
        public static Dictionary<int, string> GetFileExtensionTypeDictionary()
        {
            Dictionary<int, string> FileExtensionType = new Dictionary<int, string>();
            foreach (FileExtnType itm in Enum.GetValues(typeof(FileExtnType)).Cast<FileExtnType>())
             { FileExtensionType.Add((int)itm, itm.ToString()); }
            return FileExtensionType;
        }

        public static Dictionary<string, KeyValuePair<int, string>> GetFileExtensionAndTypeDictionary()
        {
            Dictionary<string, KeyValuePair<int, string>> FileExtension = new Dictionary<string, KeyValuePair<int, string>>();
            foreach (string itm in Enum.GetNames(typeof(FileExtnMapping)))
            {
                string _ext = itm;
                int _extVal = (int)Enum.Parse(typeof(FileExtnMapping), itm);
                string _extTyp = ((FileExtnType)_extVal).ToString();
                int _extTypVal = Convert.ToInt32((FileExtnType)_extVal);

                FileExtension.Add(itm, GetFileExtensionTypeDictionary().FirstOrDefault(x => x.Key == _extTypVal));
            }
            return FileExtension;
        }

    }
}
