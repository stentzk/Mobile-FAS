using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileConfigServices.Models
{
    public class ErrorMessageDto
    {
        public ErrorMessageDto(String aMessage)
        {
            isError = true;
            Message = aMessage;
        }

        public String Message { get; set; }

        public bool isError { get; set; }

        public string errorCode { get; set; }


    }
}
