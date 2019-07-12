using System;
using System.Collections.Generic;

namespace MobileConfigServices.Entities
{
    public partial class CfgNotification
    {
        public int NotificationId { get; set; }
        public int AppId { get; set; }
        public int LocationId { get; set; }
        public int RouteId { get; set; }
        public string NotificationData { get; set; }
        public DateTime? NotificationStartTime { get; set; }
        public DateTime? NotificationEndTime { get; set; }
        public DateTime? UpdateDate { get; set; }

        public CfgApplication App { get; set; }
        public CfgLocation Location { get; set; }
        public CfgRoute Route { get; set; }
    }
}
