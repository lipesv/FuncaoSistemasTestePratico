using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FI.WebAtividadeEntrevista.Configuration.Response
{
    public class Notification
    {
        public string Message { get; set; }
        public string Type { get; set; } // success, error, warning, info
    }

    public class JsonResponse
    {
        public bool Success { get; set; }
        public List<Notification> Notifications { get; set; }
        public string Html { get; set; }
    }
}