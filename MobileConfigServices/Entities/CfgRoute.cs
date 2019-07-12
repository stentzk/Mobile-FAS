using System;
using System.Collections.Generic;

namespace MobileConfigServices.Entities
{
    public partial class CfgRoute
    {
        public CfgRoute()
        {
            CfgNotification = new HashSet<CfgNotification>();
            CfgParameters = new HashSet<CfgParameters>();
        }

        public int RouteId { get; set; }
        public int LocationId { get; set; }
        public string RouteNumber { get; set; }
        public bool Active { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CfgLocation Location { get; set; }
        public ICollection<CfgNotification> CfgNotification { get; set; }
        public ICollection<CfgParameters> CfgParameters { get; set; }
    }
}
