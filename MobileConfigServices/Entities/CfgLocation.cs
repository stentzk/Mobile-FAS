using System;
using System.Collections.Generic;

namespace MobileConfigServices.Entities
{
    public partial class CfgLocation
    {
        public CfgLocation()
        {
            CfgNotification = new HashSet<CfgNotification>();
            CfgParameters = new HashSet<CfgParameters>();
            CfgRoute = new HashSet<CfgRoute>();
        }

        public int LocationId { get; set; }
        public int AppId { get; set; }
        public string LocationNumber { get; set; }
        public bool Active { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CfgApplication App { get; set; }
        public ICollection<CfgNotification> CfgNotification { get; set; }
        public ICollection<CfgParameters> CfgParameters { get; set; }
        public ICollection<CfgRoute> CfgRoute { get; set; }
    }
}
