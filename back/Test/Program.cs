using System;
using log4net;

namespace Test
{
    class Program
    {
        static void Main()
        {
            //log4net.Config.XmlConfigurator.Configure();
            var log = LogManager.GetLogger(typeof(Program));

            log.Error("error");

            log.Debug("debug", new Exception("有异常"));

            log.Info("infos");

            log.Fatal("fatal");

            Console.Read();
        }
    }
}
