﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MobileConfigServices.Controllers
{

    public class DefaultController : Controller
    {
        // GET: api/<controller>
        [HttpGet("")]
        public IActionResult Get()
        {
            return StatusCode(200, "Service is Ready");

        }


    }
}
