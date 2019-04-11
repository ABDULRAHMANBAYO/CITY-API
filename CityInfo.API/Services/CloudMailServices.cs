using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace  CityInfo.API.Services
{
    public class CloudMailService:IMailService
    { public string _mailTo =Startup.Configuration["mailSettings:mailToAddress"];
        
        public string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject,string message)
        {
            //Send mail-output to debug window
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo},with CloudMailService. ");
            Debug.WriteLine($"Subject:{subject}");
            Debug.WriteLine($"Message:{message}");
        }

    }
    
}