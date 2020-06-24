using NetCommonUtilities;
using NetFileWatcherWinService.Helpers;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetFileWatcherWinService.Services
{
    class FileIOService
    {
        readonly Logger Logger;
        readonly ConfigHelper ConfigHelper;
        readonly int BufferSize = 80 * 1024;

        public FileIOService(Logger _Logger, ConfigHelper _ConfigHelper)
        {
            this.Logger = _Logger;
            this.ConfigHelper = _ConfigHelper;
        }

        #region FileIO Public Methods

        public bool MoveFile(string _OldFileName, string _DestFileName)
        {
            //Check if file is locked by another process
            bool fileLocked = false;
            fileLocked = IsFileLocked(_OldFileName);

            int retryCount = 0;
            bool val = false;
            _ = DeleteFile(_DestFileName);
            do
            {
                try
                {
                    if (File.Exists(_OldFileName))
                    {
                        File.Move(_OldFileName, _DestFileName);
                    }
                    val = true;
                }
                catch (Exception ex)
                {
                    Logger.Warning(string.Format("Exception handled for MoveFile Method. Retry counter: {0}", retryCount));
                    //Logger.Error(ex, "MoveFile");
                    Thread.Sleep(ConfigHelper.Model.WaitTimerForRetrying);
                }
                retryCount++;
            } while (fileLocked && retryCount < ConfigHelper.Model.RetryLockedFileAttempts);// Retry if file is locked
            return val;
        }

        public bool DeleteFile(string _DestFileName)
        {
            //Check if file is locked by another process
            bool fileLocked = false;
            fileLocked = IsFileLocked(_DestFileName);

            int retryCount = 0;
            bool val = false;

            do
            {
                try
                {
                    if (File.Exists(_DestFileName))
                    {
                        File.Delete(_DestFileName);
                    }
                    val = true;
                }
                catch (Exception ex)
                {
                    Logger.Warning(string.Format("Exception handled for DeleteFile Method. Retry counter: {0}", retryCount));
                    // Logger.Error(ex, "DeleteFile");
                    Thread.Sleep(ConfigHelper.Model.WaitTimerForRetrying);
                }
                retryCount++;

            } while (fileLocked && retryCount < ConfigHelper.Model.RetryLockedFileAttempts);// Retry if file is locked
            return val;
        }

        public bool CopyFile(string _InPath, string _OutPath)
        {
            bool val = false;
            try
            {
                using (var ins = new FileStream(_InPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, true))
                {
                    using (var ops = new FileStream(_OutPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                    {
                        val = CopyStream(ins, ops);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "CopyFile");
            }

            return val;
        }

        public string ReadFileContentAsString(string _FileNameWithPath)
        {
            if (File.Exists(_FileNameWithPath))
            {
                return File.ReadAllText(_FileNameWithPath);
            }
            else { return string.Empty; }
        }

        public string ResolveFileNameEndingChar(char _EndingChar, string _FileName)
        {
            string fName = Path.GetFileNameWithoutExtension(_FileName);
            string ext = Path.GetExtension(_FileName);
            if (fName.EndsWith(_EndingChar.ToString()))
            {
                fName = fName.Remove(fName.Length - 1, 1);
                //fName = fName.Replace(_EndingChar.ToString(), string.Empty);
            }
            return fName + ext;
        }

        public List<string> GetAllFilesFromDirectory(string _Path, string[] _SearchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool IgnoreSearchPattern = false)
        {
            if (!IgnoreSearchPattern)
            {
                return _SearchPatterns.AsParallel()
                       .SelectMany(searchPattern =>
                              Directory.EnumerateFiles(_Path, searchPattern, searchOption)).ToList();
            }
            else
            {
                return _SearchPatterns.AsParallel()
                      .SelectMany(searchPattern =>
                             Directory.EnumerateFiles(_Path)).Distinct().ToList();
            }
        }

        public string GetFileDirectory(string _FullFileNameWithPath)
        {
            if (File.Exists(_FullFileNameWithPath))
            {
                return Path.GetDirectoryName(_FullFileNameWithPath);
            }
            else { return string.Empty; }
        }

        public string GetFileName(string _FullFileNameWithPath)
        {
            if (File.Exists(_FullFileNameWithPath))
            {
                return Path.GetFileNameWithoutExtension(_FullFileNameWithPath);
            }
            else { return string.Empty; }
        }

        public string GetFileExtension(string _FullFileNameWithPath)
        {
            if (File.Exists(_FullFileNameWithPath))
            {
                return Path.GetExtension(_FullFileNameWithPath);
            }
            else { return string.Empty; }
        }

        public FileStream GetFileStream(string _FullFileNameWithPath)
        {
            FileStream fileStream = null;
            try
            {
                if (File.Exists(_FullFileNameWithPath))
                {
                    using (FileStream _fileStream = new FileStream(_FullFileNameWithPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, true))
                    {
                        fileStream = _fileStream;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetFileStream");
                fileStream = null;
            }
            return fileStream;
        }

        public bool CreateFileFromFileStream(string _FullFileNameWithPath, FileStream _FileStream)
        {
            bool _return = false;
            try
            {
                using (FileStream fs = new FileStream(_FullFileNameWithPath, FileMode.Create))
                {
                    fs.Close();
                    _return = true;
                }
            }
            catch (Exception ex)
            {
                _return = false;
                Logger.Error(ex, "GetFileStream");
            }
            return _return;
        }

        public bool CreateFileFromFileStream(string _FullFileNameWithPath, byte[] _FileStream)
        {
            bool _return = false; 
            try
            { 
                using (FileStream fs = new FileStream(_FullFileNameWithPath, FileMode.Create))
                {
                    fs.Write(_FileStream, 0, _FileStream.Length);
                    fs.Close();
                    _return = true;
                }
                _return = true;
            }
            catch (Exception ex)
            {
                _return = false;
                Logger.Error(ex, "GetFileStream");
            }
            return _return;
        }

        public DataSet GetMaskingSourceDataSet()
        {
            string MaskingConfigSourcePath = ConfigHelper.Model.ConfigurationSourcePath;
            string MaskingSourceFileName = string.Format("{0}{1}{2}", MaskingConfigSourcePath, Path.DirectorySeparatorChar, "MaskingSource.xml");
            string MaskingSourceSchemaFileName = string.Format("{0}{1}{2}", MaskingConfigSourcePath, Path.DirectorySeparatorChar, "MaskingSourceSchema.xsd");
            DataSet dsMaskingSource = new DataSet();
            dsMaskingSource.Clear();
            dsMaskingSource.ReadXmlSchema(MaskingSourceSchemaFileName);
            dsMaskingSource.ReadXml(MaskingSourceFileName);
            return dsMaskingSource;
        }

        /// <summary>
        /// This method will check if the file is being used by another process or not
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected bool IsFileLocked(string file)
        {
            try
            {
                using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }

        #endregion


        #region FileIO Private Methods

        private bool CopyStream(Stream _InStream, Stream _OutStream)
        {
            bool val = false;
            try
            {
                var DataQueue = new ConcurrentQueue<byte[]>();
                using (AutoResetEvent DataReady = new AutoResetEvent(false))
                {
                    using (AutoResetEvent DataProcessed = new AutoResetEvent(false))
                    {
                        var ReadDataTask = Task.Factory.StartNew(() =>
                        {
                            while (true)
                            {
                                var Data = new byte[BufferSize];
                                var BytesRead = _InStream.Read(Data, 0, Data.Length);
                                if (BytesRead != BufferSize)
                                    Data = SubArray(Data, 0, BytesRead);
                                DataQueue.Enqueue(Data);
                                DataReady.Set();
                                if (BytesRead != BufferSize)
                                    break;
                                DataProcessed.WaitOne();
                            }
                        });
                        var ProcessDataTask = Task.Factory.StartNew(() =>
                        {
                            byte[] Data;
                            do
                            {
                                DataReady.WaitOne();
                                DataQueue.TryDequeue(out Data);
                                DataProcessed.Set();
                                _OutStream.Write(Data, 0, Data.Length);
                                if (Data.Length != BufferSize)
                                    break;
                            } while (Data.Length == BufferSize);
                        });
                        ReadDataTask.Wait();
                        ProcessDataTask.Wait();
                    }
                }
                val = true;
            }
            catch
            {
                throw;
            }

            return val;
        }

        private T[] SubArray<T>(T[] _Data, int _Index, int _Length)
        {
            T[] result = new T[_Length];
            Array.Copy(_Data, _Index, result, 0, _Length);
            return result;
        }

        #endregion


        #region SFTP Public Methods

        public bool SendSFTPFile(Enums.SFTP_Site _SFTP_Site, string _InPath, string _SFTPPath)
        {
            bool retVal = false;
            if (!File.Exists(_InPath))
            { return false; }
            try
            {
                Logger.Trace("TRANSFERRING FILE TO SFTP LOCATION: ");
                using (SftpClient sftpClient = GetSFTPClient(_SFTP_Site))
                {
                    Logger.Trace("SFTP Connection established... ");
                    sftpClient.Connect();
                    //sftp.ChangeDirectory("/MyFolder");
                    using (var uplfileStream = File.OpenRead(_InPath))
                    {
                        sftpClient.UploadFile(uplfileStream, _SFTPPath, true);
                        Logger.Trace("SFTP file stream uploaded...");
                    }
                    sftpClient.Disconnect();
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error While SFTP File Transfer");
            }

            return retVal;
        }

        public IEnumerable<SftpFile> ListSFTPFiles(string _DirectoryPath, string SFTP_Host, int SFTP_Port, string SFTP_Usr, string SFTP_Pwd)
        {
            using (SftpClient sftp = new SftpClient(SFTP_Host, SFTP_Port, SFTP_Usr, SFTP_Pwd))
            {
                IEnumerable<SftpFile> files;
                try
                {
                    sftp.Connect();
                    Logger.Trace("Reading from SFTP location");
                    files = sftp.ListDirectory(_DirectoryPath);
                    sftp.Disconnect();
                    return files;
                }
                catch (Exception e)
                {
                    Logger.Error(e, "ListSFTPFiles");
                    return null;
                }
            }
        }

        public bool SendSFTPFile(string _InPath, string _SFTPPath, string SFTP_Host, int SFTP_Port, string SFTP_Usr, string SFTP_Pwd)
        {
            bool retVal = false;
            if (!File.Exists(_InPath))
            { return false; }

            using (SftpClient sftp = new SftpClient(SFTP_Host, SFTP_Port, SFTP_Usr, SFTP_Pwd))
            {
                try
                {
                    sftp.Connect();
                    Logger.Trace("TRANSFERRING FILE TO SFTP: ");
                    //sftp.ChangeDirectory("/MyFolder");
                    using (var uplfileStream = File.OpenRead(_InPath))
                    {
                        sftp.UploadFile(uplfileStream, _SFTPPath, true);
                    }
                    sftp.Disconnect();
                    retVal = true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Send SFTP");
                }
            }
            return retVal;
        }

        #endregion


        #region SFTP Private Methods

        private SftpClient GetSFTPClient(Enums.SFTP_Site _SFTP_Site)
        {
            SftpClient sftpClient = null;
            string SFTP_Host = string.Empty;
            int SFTP_Port = 0;
            string SFTP_Usr = string.Empty;
            string SFTP_Pwd = string.Empty;

            switch (_SFTP_Site)
            {
                case Enums.SFTP_Site.Default:
                    break;
                case Enums.SFTP_Site.Input:
                    SFTP_Host = ConfigHelper.Model.InputFilePathLocation_SFTP_Host;
                    SFTP_Port = ConfigHelper.Model.InputFilePathLocation_SFTP_Port;
                    SFTP_Usr = ConfigHelper.Model.InputFilePathLocation_SFTP_Usr;
                    SFTP_Pwd = ConfigHelper.Model.InputFilePathLocation_SFTP_Pwd;
                    break;
                case Enums.SFTP_Site.Output:
                    SFTP_Host = ConfigHelper.Model.OutputFilePathLocation_SFTP_Host;
                    SFTP_Port = ConfigHelper.Model.OutputFilePathLocation_SFTP_Port;
                    SFTP_Usr = ConfigHelper.Model.OutputFilePathLocation_SFTP_Usr;
                    SFTP_Pwd = ConfigHelper.Model.OutputFilePathLocation_SFTP_Pwd;
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(SFTP_Host) && SFTP_Port > 0 && !string.IsNullOrEmpty(SFTP_Usr) && !string.IsNullOrEmpty(SFTP_Pwd))
            {
                sftpClient = new SftpClient(SFTP_Host, SFTP_Port, SFTP_Usr, SFTP_Pwd);
            }

            return sftpClient;
        }

        #endregion

    }
}

