using System.IO;
using System.Reflection;
using MozaeekCore.Specs.Hosting;
using TechTalk.SpecFlow;

namespace MozaeekCore.Specs.Hooks
{
    [Binding]
    public static class HostingHook
    {
        private static DotNetCoreHost _host;

        [BeforeTestRun]
        public static void StartSutHost()
        {
            var sulotionPathParent = Directory.GetParent(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()))).Parent.FullName;
           
            //  Assembly.GetExecutingAssembly().Location
            var options = new DotNetCoreHostOptions()
            {
                //TODO: Fix This O_o
                CsProjectPath = @$"{sulotionPathParent}\MozaeekCore.RestAPI",
                Port = 5000,
            };
            _host = new DotNetCoreHost(options);
            _host.Start();
        }

        [AfterTestRun]
        public static void StopSutHost()
        {
            _host.Stop();
        }
    }

}