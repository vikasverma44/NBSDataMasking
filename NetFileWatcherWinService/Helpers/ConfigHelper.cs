using NetFileWatcherWinService.Models;
using System;
using System.Configuration;

namespace NetFileWatcherWinService.Helpers
{
    class ConfigHelper
    {
        public ConfigModel Model;
        public ConfigHelper()
        {
            Model = new ConfigModel
            {
                //Logger Congiguration
                IsLoggingEnabled = GetSetting<bool>("IsLoggingEnabled"),
                LoggingFileMaxSizeInMB = GetSetting<int>("LoggingFileMaxSizeInMB"),
                FileExtensions = GetSetting<string>("FileExtensions").Replace(" ", "").Split(','),
                MetaFileNameExtention = GetSetting<string>("MetaFileNameExtention"),

                //Service Start Configurations
                IsServiceStartManuallyScheduled = GetSetting<bool>("IsServiceStartManuallyScheduled"),
                ServiceStartPeriodInMinutes = GetSetting<double>("ServiceStartPeriodInMinutes"),

                //File Purge/Deletion Configuration
                DataDeletionPeriodInDays = GetSetting<int>("DataDeletionPeriodInDays"),
                DataPurgingPeriodInDays = GetSetting<int>("DataPurgingPeriodInDays"),
                RetryLockedFileAttempts=GetSetting<int>("RetryLockedFileAttempts"),
                WaitTimerForRetrying = GetSetting<int>("WaitTimerForRetrying"),

                //Timers Configurations
                PurgeTimerIntervalInMinutes = GetSetting<double>("PurgeTimerIntervalInMinutes"),
                PrintTimerIntervalInMinutes = GetSetting<double>("PrintTimerIntervalInMinutes"),
                WatcherWaitIntervalInSeconds = GetSetting<int>("WatcherWaitIntervalInSeconds"),

                //Local File Processing Path as per Local Machine FileIO Directories
                LoggingPath = GetSetting<string>("LoggingPath"),
                ConfigurationSourcePath = GetSetting<string>("ConfigurationSourcePath"),
                InputFilePathLocation_Local = GetSetting<string>("InputFilePathLocation_Local"),
                InputFileDiscardedPath = GetSetting<string>("InputFileDiscardedPath"),
                OutputFilePathLocation_Local = GetSetting<string>("OutputFilePathLocation_Local"),
                OutputFileDiscardedPath = GetSetting<string>("OutputFileDiscardedPath"),

                //Input File Location Path SFTP/Local Details
                InputFilePathLocation_SFTP_IsEnabled = GetSetting<bool>("InputFilePathLocation_SFTP_IsEnabled"),
                InputFilePathLocation_SFTP = GetSetting<string>("InputFilePathLocation_SFTP"),
                InputFilePathLocation_SFTP_Host = GetSetting<string>("InputFilePathLocation_SFTP_Host"),
                InputFilePathLocation_SFTP_Port = GetSetting<int>("InputFilePathLocation_SFTP_Port"),
                InputFilePathLocation_SFTP_Usr = GetSetting<string>("InputFilePathLocation_SFTP_Usr"),
                InputFilePathLocation_SFTP_Pwd = GetSetting<string>("InputFilePathLocation_SFTP_Pwd"),

                //Output File Location Path SFTP/Local Details
                OutputFilePathLocation_SFTP_IsEnabled = GetSetting<bool>("OutputFilePathLocation_SFTP_IsEnabled"),
                OutputFilePathLocation_SFTP = GetSetting<string>("OutputFilePathLocation_SFTP"),
                OutputFilePathLocation_SFTP_Host = GetSetting<string>("OutputFilePathLocation_SFTP_Host"),
                OutputFilePathLocation_SFTP_Port = GetSetting<int>("OutputFilePathLocation_SFTP_Port"),
                OutputFilePathLocation_SFTP_Usr = GetSetting<string>("OutputFilePathLocation_SFTP_Usr"),
                OutputFilePathLocation_SFTP_Pwd = GetSetting<string>("OutputFilePathLocation_SFTP_Pwd"),

                //SMTP Details
                IsEmailEnabled = GetSetting<bool>("IsEmailEnabled"),
                SMTP_Host = GetSetting<string>("SMTP_Host"),
                SMTP_Port = GetSetting<int>("SMTP_Port"),
                SMTP_Usr = GetSetting<string>("SMTP_Usr"),
                SMTP_Pwd = GetSetting<string>("SMTP_Pwd"),
                SMTP_From = GetSetting<string>("SMTP_From"),
                SMTP_To = GetSetting<string>("SMTP_To")
            };
        }

        private T GetSetting<T>(string _Key, T _DefaultValue = default) where T : IConvertible
        {
            string val = ConfigurationManager.AppSettings[_Key] ?? "";
            T result = _DefaultValue;
            if (!string.IsNullOrEmpty(val))
            {
                T typeDefault = default;
                if (typeof(T) == typeof(String))
                {
                    typeDefault = (T)(object)String.Empty;
                }
                result = (T)Convert.ChangeType(val, typeDefault.GetTypeCode());
            }
            return result;
        }
    }
}
