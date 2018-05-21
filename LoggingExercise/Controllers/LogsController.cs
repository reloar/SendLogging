using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace LoggingExercise.Controllers
{
    public class LogsController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Log4net()
        {

            log.Error("This is our error message");
            try
            {
                int i = 5;
                var s = i / 0;
            }
            catch (DivideByZeroException ex)
            {
                log.Fatal("This was a terrible error");
                log.Debug("Here again", ex);
            }

            return View();
        }
        [HttpGet]
        public ActionResult Nlog()
        {

            try
            {
                int i = 5;
                var s = i / 0;
            }
            catch (DivideByZeroException ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();

                logger.Error(ex, "this too");

            }

            return View();
        }
    }
}