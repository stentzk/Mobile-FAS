using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MobileConfigServices.Entities;
using MobileConfigServices.Models;
using MobileConfigServices.Services;
using MobileConfigServices.Constants;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MobileConfigServices.Controllers
{
    [Route("api/location")]
    public class LocationsController : Controller
    {

        private ILogger<LocationsController> _logger;

        private IConfigurationRepository _configurationRepository;

        public LocationsController(IConfigurationRepository configurationRepository, ILogger<LocationsController> logger)
        {
            _configurationRepository = configurationRepository;
            _logger = logger;
        }

        [HttpGet("{locationNumber}")]
        public IActionResult GetLocation(String locationNumber, String appName)
        {
            ResponseDto<List<LocationDto>> response = new ResponseDto<List<LocationDto>>();
            try
            {
        
                _logger.LogInformation("GetLocation()is called");

                var location = _configurationRepository.GetLocation(locationNumber);
                if (location == null || location.Count() < 1)
                {
                    ErrorMessageDto errorMessage = new ErrorMessageDto(string.Format(MessageConstants.LocationNotExist, locationNumber));
                    //errorMessage.errorCode = "404";
                    _logger.LogInformation(errorMessage.Message);
                    response.Error = errorMessage;
                    return NotFound(response);
                }
                else if (appName != null)
                {
                    var app = _configurationRepository.GetApplication(appName);
                    if (app == null)
                    {
                        ErrorMessageDto errorMessage = new ErrorMessageDto(string.Format(MessageConstants.AppNotExist, appName));                      
                        _logger.LogInformation(errorMessage.Message);
                        response.Error = errorMessage;
                        return NotFound(response);
                    }
                    else
                    {
                        var locationApp = location.Where(c => app.AppId == c.AppId).ToList();
                        var results = Mapper.Map<IEnumerable<LocationDto>>(locationApp);
                        response.Response = (List<LocationDto>)results;
                        return Ok(response);
                    }
                }
                else
                {
                    var results = Mapper.Map<IEnumerable<LocationDto>>(location);
                    response.Response = (List<LocationDto>) results;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageDto errorMessage = new ErrorMessageDto(string.Format(MessageConstants.ExLocation, locationNumber));
                _logger.LogCritical(errorMessage.Message, ex);
                response.Error = errorMessage;
                return StatusCode(500, response);
            }
        }

        [HttpGet("")]
        public IActionResult GetLocations()
        {
            ResponseDto<List<LocationDto>> response = new ResponseDto<List<LocationDto>>();
            try
            {
                var locations = _configurationRepository.GetLocations();
                var results = Mapper.Map<IEnumerable<LocationDto>>(locations);
                if (results == null)
                {
                    ErrorMessageDto errorMessage = new ErrorMessageDto(MessageConstants.NoLocations);
                    _logger.LogCritical(errorMessage.Message);
                    response.Error = errorMessage;
                    return NotFound(response);

                }
                else
                {
                    response.Response = (List<LocationDto>) results;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
               
                ErrorMessageDto errorMessage = new ErrorMessageDto(MessageConstants.NoLocations);
                _logger.LogCritical(errorMessage.Message, ex);
                // errorMessage.errorCode = "404";
                response.Error = errorMessage;
                return StatusCode(500,  response);
            }
            
        }

        [HttpGet("routes/{locationNumber}")]
        public IActionResult GetLocationRoutes(String locationNumber)
        {
            ResponseDto<LocationRoutesDto> response = new ResponseDto<LocationRoutesDto>();
            try
            {
                var location = _configurationRepository.GetLocationRoutes(locationNumber);
                if (location == null)
                {
                    ErrorMessageDto errorMessage = new ErrorMessageDto(string.Format(MessageConstants.LocationNotExist, locationNumber));
                    _logger.LogCritical(errorMessage.Message);
                    response.Error = errorMessage;
                    return NotFound(response);
                }
                else
                {
                    var locationResults = Mapper.Map<LocationRoutesDto>(location);
                    var routeResults = Mapper.Map<IEnumerable<RouteDto>>(locationResults.ListOfRoutes);
                    locationResults.ListOfRoutes = (List<RouteDto>) routeResults;
                    response.Response = (LocationRoutesDto) locationResults;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageDto errorMessage = new ErrorMessageDto(string.Format(MessageConstants.ExLocation, locationNumber));
                _logger.LogCritical(errorMessage.Message, ex);
                response.Error = errorMessage;
                return StatusCode(500, response);
            }
        }
    }
}
