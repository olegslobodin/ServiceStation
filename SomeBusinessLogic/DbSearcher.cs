using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStation.Models;

namespace ServiceStation.SomeBusinessLogic
{
    public static class DbSearcher
    {
        public static IEnumerable<Client> SearchClients(ServiceStationEntities db, string filter)
        {
            return (filter == null || filter.Split(' ').Length == 0) ? db.Clients :
                db.Clients.ToList().Where(c =>
                {
                    return filter.Split(' ').All(word =>
                    c.FirstName.ToLower().Contains(word.ToLower()) || c.LastName.ToLower().Contains(word.ToLower()));
                });
        }
    }
}