using System;
using System.Collections.Generic;

namespace MobileConfigServices.Entities
{
    public partial class CfgParameters
    {
        public int ParameterId { get; set; }
        public int AppId { get; set; }
        public int LocationId { get; set; }
        public int RouteId { get; set; }
        public DateTime? ShouldRefreshAfter { get; set; }
        public DateTime? ShouldFlushAfter { get; set; }
        public int? LoggingLevel { get; set; }
        public DateTime? LoggingStartTime { get; set; }
        public DateTime? LoggingEndTime { get; set; }
        public int? RefreshConfigTableFreq { get; set; }
        public bool? ForceFullDownload { get; set; }
        public DateTime? FullDownloadStartTime { get; set; }
        public DateTime? FullDownloadEndTime { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public CfgApplication App { get; set; }
        public CfgLocation Location { get; set; }
        public CfgRoute Route { get; set; }
    }
}
