using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using ServiceStation.Models;

namespace ServiceStation.ViewModels
{
    public class ClientsIndex
    {
        public IEnumerable<Client> Clients { get; set; }

        public string Filter { get; set; }
    }
}