using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileConfigServices.Services;
using MobileConfigServices.Models;
using MobileConfigServices.Constants;
using AutoMapper;

namespace MobileConfigServices.Controllers
{
    [Route("api/ConfigParameters")]
    [ApiController]
    public class ConfigParametersController : ControllerBase
    {
        private ILogger<ConfigParametersController> _logger;

        private IConfigurationRepository _configurationRepository;

        public ConfigParametersController(IConfigurationRepository configurationRepository, ILogger<ConfigParametersController> logger)
        {
            _configurationRepository = configurationRepository;
            _logger = logger;
        }

        [HttpGet("{appName}/{locationNumber}/{routeNumber}/{updatedDate}")]
        public IActionResult GetConfigParameters(String appName, String locationNumber, String routeNumber, DateTime updatedDate)
        {
            ResponseDto<ParametersDto> response = new ResponseDto<ParametersDto>();
            try
            {
                var app = _configurationRepository.GetApplication(appName);
                if (app == null)
                {
                    ErrorMessageDto errorMessage = new ErrorMessageDto(string.Format(MessageConstants.AppNotExist, appName));
                    response.Error = errorMessage;
                    return NotFound(response);
                }
                var location = _configurationRepository.GetLocationByAppId(app.AppId, locationNumber);
                if (location == null)
                {
                    ErrorMessageDto errorMessage = new ErrorMessageDto(string.Format(MessageConstants.LocationNotExist, locationNumber));
                    response.Error = errorMessage;
                    return NotFound(response);
                }
                var route = _configurationRepository.GetRoute(location.LocationId, routeNumber);
                if (route == null)
                {
                    ErrorMessageDto errorMessage = new ErrorMessageDto(string.Format(MessageConstants.RouteNotExist, routeNumber));
                    response.Error = errorMessage;
                    return NotFound(response);
                }
                var parameters = _configurationRepository.GetParameters(app.AppId, location.LocationId, route.RouteId, updatedDate);
                if (parameters == null)
                {
                    return Ok(response);
                }
                else
                {
                    var results = Mapper.Map<ParametersDto>(parameters);
                    results.LocationStatus = parameters.Location.Active;
                    results.RouteStatus = parameters.Route.Active;
                    return Ok(results);
                }
            }
            catch (Exception es)
            {
                return StatusCode(500, MessageConstants.ExLocation);
            }


        }
    }
}