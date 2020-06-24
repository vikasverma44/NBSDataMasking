using NetCommonUtilities.Models;
using NetFileWatcherWinService.Helpers;
using Masking = NetDataMaskingLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Timers;
using CurrentThread = System.Threading;
using static NetCommonUtilities.Enums;
using System.ServiceProcess;

namespace NetFileWatcherWinService.Services
{
    class NBSService
    {
        #region Class Members Definitions & Constructor

        public Logger Logger;
        private readonly ConfigHelper ConfigHelper;
        private readonly EmailService EmailService;
        private readonly FileIOService FileIOService;
        private readonly Masking.NetDataMaskingLib DataMaskingService;

        private readonly Timer TimerFileMover;
        private readonly Timer TimerPurge;
        private readonly FileSystemWatcher InputFolderFileSystemWatcher;
        readonly string Mailer_MasterDiscardFilePath = string.Empty;
        readonly string Mailer_DuplicateDiscardFilePath = string.Empty;
        private string CntrGrp = ".CNTR.GRP";
        private string Nbs_East = ".EAST";
        public NBSService()
        {
            ConfigHelper = new ConfigHelper();

            Logger = new Logger(ConfigHelper);
            EmailService = new EmailService(Logger, ConfigHelper);
            FileIOService = new FileIOService(Logger, ConfigHelper);
            DataMaskingService = new Masking.NetDataMaskingLib();

            TimerPurge = new Timer();
            TimerFileMover = new Timer();
            InputFolderFileSystemWatcher = new FileSystemWatcher();
            Mailer_MasterDiscardFilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Models\Mailer_MasterDiscard.html");
            Mailer_DuplicateDiscardFilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Models\Mailer_DuplicateDiscard.html");
        }

        #endregion


        #region Public Methods - Service Level Event Triggering

        public void Start_Processing()
        {
            Timer timer;
            DateTime time = DateTime.Now;
            if (ConfigHelper.Model.IsServiceStartManuallyScheduled)
            {
                time = DateTime.Now.AddMinutes(ConfigHelper.Model.ServiceStartPeriodInMinutes);
                var span = time - DateTime.Now;
                timer = new Timer { Interval = span.TotalMilliseconds < 1 ? 10 : span.TotalMilliseconds, AutoReset = false };
                Logger.Trace("Service Processing will be started at :" + DateTime.Now.AddMilliseconds(timer.Interval).ToString());

            }
            else
            {
                timer = new Timer { Interval = 10, AutoReset = false };
                Logger.Trace("Service Processing will be started at :" + DateTime.Now.AddMilliseconds(timer.Interval).ToString());
            }

            timer.Elapsed += (sender, e) => { InitializeServiceTimers(); };
            timer.Start();
            ProcessInputFilesForDataMasking();
        }

        public void Stop_Processing()
        {
            InputFolderFileSystemWatcher.EnableRaisingEvents = false;
            TimerPurge.Enabled = false;
            TimerPurge.AutoReset = false;
            TimerPurge.Stop();
            TimerFileMover.Enabled = false;
            TimerFileMover.AutoReset = false;
            TimerFileMover.Stop();
        }

        #endregion


        #region Component Event Methods

        private void PurgeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Logger.Trace("STARTED: PurgeTimer_Elapsed method");
            try
            {
                ProcessFilePurging();
                ProcessFileRecordsDeletion();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "PurgeTimer_Elapsed");
            }
            Logger.Trace("ENDED: PurgeTimer_Elapsed method");
        }

        private void TimerFileMover_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Logger.Trace("STARTED: TimerFileMover_Elapsed method");
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "TimerFileMover_Elapsed");
            }
            Logger.Trace("ENDED: TimerFileMover_Elapsed method");
        }

        private void InputFolderFileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Logger.Trace("STARTED: InputFolderFileSystemWatcher_Created method");
            CurrentThread.Thread.Sleep(ConfigHelper.Model.WatcherWaitIntervalInSeconds * 1000);
            ProcessInputFilesForDataMasking();
            Logger.Trace("ENDED: InputFolderFileSystemWatcher_Created method");
        }

        private void ProcessInputFilesForDataMasking()
        {
            Logger.Trace("STARTED: ProcessInputFilesForDataMasking Method");
            try
            {
                List<string> allFileList = FileIOService.GetAllFilesFromDirectory(ConfigHelper.Model.InputFilePathLocation_Local, ConfigHelper.Model.FileExtensions, IgnoreSearchPattern: true);
                List<string> validFileList = FileIOService.GetAllFilesFromDirectory(ConfigHelper.Model.InputFilePathLocation_Local, ConfigHelper.Model.FileExtensions, IgnoreSearchPattern: false);
                List<string> invalidFileList = allFileList.Where(p => validFileList.All(p2 => p2 != p)).ToList();
                
              
                foreach (string fileName in validFileList)
                {
                    string metaFileName = fileName + ConfigHelper.Model.MetaFileNameExtention;
                    if (File.Exists(metaFileName))
                        _ =CheckAndCreateMaskedFile(fileName, metaFileName);
                }
                foreach (string fileName in invalidFileList)
                {
                    _ = MoveFilesToInputDiscarded(fileName, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "ProcessInputFilesForDataMasking Method");
            }
            Logger.Trace("STARTED: ProcessInputFilesForDataMasking Method");
        }

        #endregion


        #region Private Methods

        private void InitializeServiceTimers()
        {
            InputFolderFileSystemWatcher.Path = ConfigHelper.Model.InputFilePathLocation_Local;
            InputFolderFileSystemWatcher.Created += InputFolderFileSystemWatcher_Created;
            InputFolderFileSystemWatcher.Changed += InputFolderFileSystemWatcher_Created;
            this.InputFolderFileSystemWatcher.EnableRaisingEvents = true;

            TimerPurge.Elapsed += new System.Timers.ElapsedEventHandler(PurgeTimer_Elapsed);
            TimerPurge.Enabled = true;
            TimerPurge.AutoReset = true;
            //TimerPurge.Interval = DateTime.Today.AddDays(ConfigHelper.Model.PurgeTimerIntervalInMinutes).Subtract(DateTime.Today).TotalSeconds * 1000;
            TimerPurge.Interval = ConfigHelper.Model.PurgeTimerIntervalInMinutes * 60 * 1000;
            TimerPurge.Start();

            TimerFileMover.Elapsed += new System.Timers.ElapsedEventHandler(TimerFileMover_Elapsed);
            TimerFileMover.Enabled = true;
            TimerFileMover.AutoReset = true;
            TimerFileMover.Interval = ConfigHelper.Model.PrintTimerIntervalInMinutes * 60 * 1000;
            TimerFileMover.Start();
            Logger.Trace("Service Processing Timers Initialized");
        }

        private void ProcessFilePurging()
        {
            Logger.Trace("FILE PURGING: Feature is not available");
        }

        private void ProcessFileRecordsDeletion()
        {
            Logger.Trace("FILE DELETION: Feature is not available");
        }

        private bool CheckAndCreateMaskedFile(string _FileName, string _MetaFileName)
        {
            bool _return = false;
            Logger.Trace("STARTED: CheckAndCreateMaskedFile Method.");
            try
            {
                FileProcessingModel _fileProcessingModel = null;
                bool IsProcessed = false;
                try
                {
                     FileExtn _fileExt = new FileExtn();
                    
                    FileExtnType _fileExtTyp = new FileExtnType();
                    if (_FileName.ToUpper().Trim().Contains(Nbs_East))
                    {
                        _fileExt = FileExtn.TXT;
                    }
                  
                    else
                    {
                       
                            _fileExt = (FileExtn)Enum.Parse(typeof(FileExtn), FileIOService.GetFileExtension(_FileName).Split('.')[1].ToUpper());
                    }
                  
                     if (_fileExt.ToString().ToUpper() == Convert.ToString(FileExtn.TXT))
                    {
                        _fileExtTyp = (FileExtnType)GetFileExtensionAndTypeDictionary()
                         .FirstOrDefault(x => x.Key == Convert.ToString(_fileExt)).Value.Key;
                    }
                    _fileProcessingModel = new FileProcessingModel
                    {
                        InputFileName = FileIOService.GetFileName(_FileName),
                        InputFileNameExtension = FileIOService.GetFileExtension(_FileName),
                        InputFileStream = FileIOService.GetFileStream(_FileName),
                        InputMetadataFileName = FileIOService.GetFileName(_MetaFileName),
                        InputMetadataFileNameExtension = FileIOService.GetFileExtension(_MetaFileName),
                        InputMetaDataFileStream = FileIOService.GetFileStream(_MetaFileName),
                        FileExtensionType = _fileExtTyp,
                        FileExtension = _fileExt
                    };
                    IsProcessed = true;
                    Logger.Trace(string.Format("STARTED: Processing of file: {0}", _FileName));
                }
                catch (Exception e) { IsProcessed = false; }

                if (IsProcessed)
                {
                    //DataMasking *****Started****
                    Logger.Trace("STARTED: DataMasking Process");
                    DataSet dsMaskingSource = new DataSet();
                    dsMaskingSource.Clear();
                    dsMaskingSource = FileIOService.GetMaskingSourceDataSet();
                   
                    FileProcessingModel model = DataMaskingService.DataMaskingFileProcessing(dsMaskingSource, _FileName, _MetaFileName, _fileProcessingModel);
                    _fileProcessingModel.HasOutputFileDetails = (model == null ? false : model.HasOutputFileDetails);
                    _fileProcessingModel.HasOutputErrors = (model == null ? true : model.HasOutputErrors);

                    Logger.Trace("ENDED: DataMasking Process");
                    //DataMasking *****Ended****

                    if (!_fileProcessingModel.HasOutputErrors && _fileProcessingModel.HasOutputFileDetails)
                    {
                        _fileProcessingModel.OutputFileName = "Masked_" + _fileProcessingModel.InputFileName;
                        _fileProcessingModel.OutputFileNameExtension = _fileProcessingModel.InputFileNameExtension;
                        _fileProcessingModel.OutputMetadataFileName = "Masked_" + _fileProcessingModel.InputMetadataFileName;
                        _fileProcessingModel.OutputMetadataFileNameExtension = _fileProcessingModel.InputMetadataFileNameExtension;

                        string outputFileName = ConfigHelper.Model.OutputFilePathLocation_Local + Path.DirectorySeparatorChar + _fileProcessingModel.OutputFileName + _fileProcessingModel.OutputFileNameExtension;
                        string outputMetadataFileName = ConfigHelper.Model.OutputFilePathLocation_Local + Path.DirectorySeparatorChar + _fileProcessingModel.OutputMetadataFileName + _fileProcessingModel.OutputMetadataFileNameExtension;

                        bool retOutputFile = FileIOService.CreateFileFromFileStream(outputFileName, _fileProcessingModel.OutputDataFileBytes);
                        bool retOutputMetaFile = FileIOService.CopyFile(_MetaFileName, outputMetadataFileName);
                      

                        Logger.Trace(string.Format("ENDED: Processing of file: {0}", _FileName));

                        if (retOutputFile && retOutputMetaFile)
                        {
                            _ = RemoveInputFiles(_FileName, _MetaFileName);

                            Logger.Trace(string.Format("SUCCESS: Output file generated successfully - {0}", outputFileName));
                            _return = true;
                        }
                        else
                        {
                            Logger.Trace("DISCARDED: Output Error");
                            _return = false;
                            _ = MoveFilesToOutputDiscarded(_FileName, _MetaFileName);
                        }
                    }
                    else
                    {
                        Logger.Trace("DISCARDED: Output Error");
                        _return = false;
                        _ = MoveFilesToOutputDiscarded(_FileName, _MetaFileName);
                    }
                }
                else
                {
                    Logger.Trace("DISCARDED: INPUT Error");
                    _return = false;
                    _ = MoveFilesToInputDiscarded(_FileName, _MetaFileName);
                }
            }
            catch (Exception ex)
            {
                _return = false;
                Logger.Error(ex, "CheckAndCreateMaskedFile");
                Logger.Trace("DISCARDED: Output Error");
                _ = MoveFilesToOutputDiscarded(_FileName, _MetaFileName);
            }
            Logger.Trace("ENDED: CheckAndCreateMaskedFile Method");
             return _return;
        }
      

        private bool MoveFilesToInputDiscarded(string FileName, string MetaFileName)
        {
            try
            {
                bool ret = false;
                if (!string.IsNullOrEmpty(FileName))
                    ret = FileIOService.MoveFile(FileName, FileName.Replace(ConfigHelper.Model.InputFilePathLocation_Local, ConfigHelper.Model.InputFileDiscardedPath));
                if (!string.IsNullOrEmpty(MetaFileName))
                    ret = FileIOService.MoveFile(MetaFileName, MetaFileName.Replace(ConfigHelper.Model.InputFilePathLocation_Local, ConfigHelper.Model.InputFileDiscardedPath));
                return true;
            }
            catch { return false; }
        }
        private bool MoveFilesToOutputDiscarded(string FileName, string MetaFileName)
        {
            try
            {
                bool ret = false;
                if (!string.IsNullOrEmpty(FileName))
                    ret = FileIOService.MoveFile(FileName, FileName.Replace(ConfigHelper.Model.InputFilePathLocation_Local, ConfigHelper.Model.OutputFileDiscardedPath));
                if (!string.IsNullOrEmpty(MetaFileName))
                    ret = FileIOService.MoveFile(MetaFileName, MetaFileName.Replace(ConfigHelper.Model.InputFilePathLocation_Local, ConfigHelper.Model.OutputFileDiscardedPath));
                return true;
            }
            catch { return false; }
        }
        private bool RemoveInputFiles(string FileName, string MetaFileName)
        {
            try
            {
                bool ret = false;
                ret = FileIOService.DeleteFile(FileName);
                ret = FileIOService.DeleteFile(MetaFileName);
                return true;
            }
            catch { return false; }
        }

        #endregion


    }
}
