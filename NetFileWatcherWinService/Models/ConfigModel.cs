namespace NetFileWatcherWinService.Models
{
    class ConfigModel
    {
        //Logger Congiguration
        public bool IsLoggingEnabled { get; set; }
        public int LoggingFileMaxSizeInMB { get; set; }
        public string[] FileExtensions { get; set; }
        public string MetaFileNameExtention { get; set; }

        //Service Start Configurations
        public bool IsServiceStartManuallyScheduled { get; set; }
        public double ServiceStartPeriodInMinutes { get; set; }

        //File Purge/Deletion Configuration
        public int DataDeletionPeriodInDays { get; set; }
        public int DataPurgingPeriodInDays { get; set; }
        public int RetryLockedFileAttempts { get; set; }
        public int WaitTimerForRetrying { get; set; }

        

        //Timers Configurations
        public double PurgeTimerIntervalInMinutes { get; set; }
        public double PrintTimerIntervalInMinutes { get; set; }
        public int WatcherWaitIntervalInSeconds { get; set; }

        //Local File Processing Path as per Local Machine FileIO Directories
        public string LoggingPath { get; set; }
        public string ConfigurationSourcePath { get; set; }
        public string InputFilePathLocation_Local { get; set; }
        public string InputFileDiscardedPath { get; set; }
        public string OutputFilePathLocation_Local { get; set; }
        public string OutputFileDiscardedPath { get; set; }

        //Input File Location Path SFTP/Local Details
        public bool InputFilePathLocation_SFTP_IsEnabled { get; set; }
        public string InputFilePathLocation_SFTP { get; set; }
        public string InputFilePathLocation_SFTP_Host { get; set; }
        public int InputFilePathLocation_SFTP_Port { get; set; }
        public string InputFilePathLocation_SFTP_Usr { get; set; }
        public string InputFilePathLocation_SFTP_Pwd { get; set; }

        //Output File Location Path SFTP/Local Details
        public bool OutputFilePathLocation_SFTP_IsEnabled { get; set; }
        public string OutputFilePathLocation_SFTP { get; set; }
        public string OutputFilePathLocation_SFTP_Host { get; set; }
        public int OutputFilePathLocation_SFTP_Port { get; set; }
        public string OutputFilePathLocation_SFTP_Usr { get; set; }
        public string OutputFilePathLocation_SFTP_Pwd { get; set; }

        //SMTP Details
        public bool IsEmailEnabled { get; set; }
        public string SMTP_Host { get; set; }
        public int SMTP_Port { get; set; }
        public string SMTP_Usr { get; set; }
        public string SMTP_Pwd { get; set; }
        public string SMTP_From { get; set; }
        public string SMTP_To { get; set; }
    }
}
