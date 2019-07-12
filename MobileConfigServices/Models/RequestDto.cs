using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileConfigServices.Models
{

    public class RequestDto<T>
    {
        public string username { get; set; }

        public T requestData { get; set; }

    }
}
