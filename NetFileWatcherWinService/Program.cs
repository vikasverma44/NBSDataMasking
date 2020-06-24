using System;
using System.Reflection;
using Topshelf;

namespace NetFileWatcherWinService
{
    class Program
    {
        static void Main(string[] args)
        {

            AssemblyName asm = Assembly.GetExecutingAssembly().GetName();
            string version = "1.0.0.0";// string.Format("{0}.{1}", asm.Version.Major, asm.Version.Minor);
            string SvcDisplayName = string.Format("NBS Router Service (v{0})", version);

            var exitCode = HostFactory.Run(windowsService =>
            {
                windowsService.Service<FileWatcherService>(s =>
                {
                    s.ConstructUsing(service => new FileWatcherService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                windowsService.EnableShutdown();
                windowsService.RunAsLocalSystem();
                windowsService.StartAutomatically();

                windowsService.SetServiceName("NBS Router");
                windowsService.SetDisplayName(SvcDisplayName);
                windowsService.SetDescription("NBS Router is a File Routing Service for Conversion/Data Masking.");
            });
            Environment.ExitCode = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
        }
    }
}
