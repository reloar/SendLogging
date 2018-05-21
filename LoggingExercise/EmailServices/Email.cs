using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggingExercise.EmailServices
{
    public class Email
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
    
}