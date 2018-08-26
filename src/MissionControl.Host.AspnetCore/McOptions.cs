﻿using System;
using Microsoft.AspNetCore.Http;

namespace MissionControl.Host.AspnetCore
{
    public class McOptions
    {
        public McOptions()
        {
            Url = "/mc";
            Authentication = req => false;
        }
        
        /// <summary>
        /// Url that opens Mission Control.
        /// Default is 'mc/'
        /// </summary>
        public string Url { get; set; }

        public Func<HttpRequest, bool> Authentication { get; set; }
    }
}