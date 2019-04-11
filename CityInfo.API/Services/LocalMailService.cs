using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace  CityInfo.API.Services
{
    public class LocalMailService
    {
        public string _mailTo ="ramhziworld@gmail.com";
        public string _mailFrom = "koleibrahimabdulrahman@gmail.com";

        public void Send(string subject,string message)
        {
            //Send mail-output to debug window
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo},with LocalMailServices. ");
            Debug.WriteLine($"Subject:{subject}");
            Debug.WriteLine($"Message:{message}");
        }

    }
    
}