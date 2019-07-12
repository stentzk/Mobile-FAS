using System;
using System.Collections.Generic;

namespace MobileConfigServices.Entities
{
    public partial class CfgApplication
    {
        public CfgApplication()
        {
            CfgLocation = new HashSet<CfgLocation>();
            CfgNotification = new HashSet<CfgNotification>();
            CfgParameters = new HashSet<CfgParameters>();
        }

        public int AppId { get; set; }
        public string AppName { get; set; }
        public string AppDescription { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<CfgLocation> CfgLocation { get; set; }
        public ICollection<CfgNotification> CfgNotification { get; set; }
        public ICollection<CfgParameters> CfgParameters { get; set; }
    }
}
