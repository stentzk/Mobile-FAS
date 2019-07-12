using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileConfigServices.Models
{
    public class LocationRoutesDto
    {
        public int AppId { get; set; }

        public int LocationId { get; set; }

        public String LocationNumber { get; set; }

        public Boolean Active { get; set; }

        public DateTime UpdatedDate { get; set; }

        public List<RouteDto> ListOfRoutes { get; set; }

        
    }
}
