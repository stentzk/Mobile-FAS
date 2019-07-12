using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileConfigServices.Models
{
    public class ParametersDto
    {
        public int ParameterId { get; set; }
        public int AppId { get; set; }
        public int LocationId { get; set; }
        public bool LocationStatus { get; set; }
        public int RouteId { get; set; }
        public bool RouteStatus { get; set; }
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
    }
}
