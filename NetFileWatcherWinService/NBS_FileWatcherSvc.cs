using NetFileWatcherWinService.Services;
using System;

namespace NetFileWatcherWinService
{
    public class FileWatcherService
    {

        #region Class Members Definitions

        readonly NBSService NBSService;

        #endregion


        #region Service Constructor and Events

        public FileWatcherService()
        {
            NBSService = new NBSService();
        }

        public void Start()
        {
            try
            {
                NBSService.Logger.Trace("STARTED: OnStart Service Method");
                NBSService.Start_Processing();
                NBSService.Logger.Trace("ENDED: OnStart Service Method");
            }
            catch (Exception ex)
            {
                NBSService.Logger.Error(ex, "OnStart");
            }
        }

        public void Stop()
        {
            try
            {
                NBSService.Logger.Trace("STARTED: OnStop Service Method");
                NBSService.Stop_Processing();
                NBSService.Logger.Trace("ENDED: OnStop Service Method");
            }
            catch (Exception ex)
            {
                NBSService.Logger.Error(ex, "OnStop");
            }
        }


        #endregion

    }
}
