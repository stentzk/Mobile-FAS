using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MobileConfigServices.Models
{

    public class RouteDto
    {
        
        public int RouteId { get; set; }

        public String RouteNumber { get; set;  }

        public Boolean Active { get; set; }

        public DateTime UpdatedDate { get; set; }


    }
}
