using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileConfigServices.Models
{
    public class ResponseDto<T>
    {
        public ErrorMessageDto Error { get; set; }
        public T Response { get; set; }
    }
}
