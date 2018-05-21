using LoggingExercise.EmailServices;
using LoggingExercise.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace LoggingExercise.Controllers.api
{
    [RoutePrefix("api/logs")]
    public class LogsController : ApiController
    {
        [HttpPost]
        [Route("log")]        
        public HttpResponseMessage Nlog(ErrorLog el)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, "your fields are not valid");

                }

                string key = ConfigurationManager.AppSettings["Sendgrid.key"];

                SendgridEmail svc = new SendgridEmail(key);

                string htmlBody = $@"<ul>
                                <li>Name: {el.Name}</li>    
                                <li>Email: {el.Email}</li>
                                <li>Phone Number: {el.Phone}</li>
                                <li>Message Deatails: {el.Message}</li>
                            </ul>";
                Email msg = new Email
                {
                    Body = htmlBody,
                    From = el.Name,
                    Subject = "Logfile",
                    To = el.Email


                };

                string envPath = HttpRuntime.AppDomainAppPath;
                string fileName = $"{envPath}\\logs\\2018-05-18.log";
                byte[] fileData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long FileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                fileData = br.ReadBytes((int)FileLength);

                svc.SendMail(msg, true, fileName, fileData);

                return this.Request.CreateResponse(HttpStatusCode.OK, "Successfully sent your mail!");

            }
            catch (Exception ex)
            {

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
        }
    }
}
