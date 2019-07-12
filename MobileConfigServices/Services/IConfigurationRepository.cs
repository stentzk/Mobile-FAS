using MobileConfigServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileConfigServices.Services
{
    public interface IConfigurationRepository
    {
        IEnumerable<CfgLocation> GetLocations();

        IEnumerable<CfgLocation> GetLocation(String locationNumber);

        CfgLocation GetLocationRoutes(String locationNumber);

        CfgRoute GetRoute(int locationId, String routeNumber);

        CfgApplication GetApplication(String appName);

        CfgParameters GetParameters(int appId, int locationId, int routeId, DateTime updatedDate);

        CfgLocation GetLocationByAppId(int appId, string locationNumber);

    }
}
