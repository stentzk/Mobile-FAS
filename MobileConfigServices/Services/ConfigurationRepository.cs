using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobileConfigServices.Entities;
using Microsoft.EntityFrameworkCore;


namespace MobileConfigServices.Services
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private RoutebookPTContext _context;

        public ConfigurationRepository(RoutebookPTContext context)
        {
            _context = context;
        }

        public CfgApplication GetApplication(String appName)
        {
            return _context.CfgApplication.FirstOrDefault(c => appName == c.AppName);
        }

        public IEnumerable<CfgLocation> GetLocation(string locationNumber)
        {
            return _context.CfgLocation.Where(c => c.LocationNumber == locationNumber).ToList<CfgLocation>();
        }

        public CfgLocation GetLocationByAppId(int appId, string locationNumber)
        {
            return _context.CfgLocation.FirstOrDefault(c => appId == c.AppId && locationNumber == c.LocationNumber);
        }

        public CfgLocation GetLocationRoutes(string locationNumber)
        {
            return _context.CfgLocation.Include(c => c.CfgRoute)
                .Where(l => l.LocationNumber == locationNumber).FirstOrDefault();
        }

        public IEnumerable<CfgLocation> GetLocations()
        {
            return _context.CfgLocation.OrderBy(c => c.LocationNumber).ToList();
        }


        public CfgRoute GetRoute(int locationId, string routeNumber)
        {
            return _context.CfgRoute.FirstOrDefault(c => c.LocationId == locationId && c.RouteNumber == routeNumber);

        }

        public CfgParameters GetParameters(int appId, int locationId, int routeId, DateTime updatedDate)
        {
            var results = _context.CfgParameters
                .Include(c => c.Location).Include(c => c.Route)
                .Where(c => c.AppId == appId && c.LocationId == locationId && c.RouteId == routeId && c.UpdatedDate == updatedDate).FirstOrDefault();

            return results;
        }
    }
}

