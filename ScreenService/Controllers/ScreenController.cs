using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ScreenService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScreenController : ControllerBase
    {
        const int _PIN = 17;

        private readonly ILogger<ScreenController> _logger;
        private readonly Connector _connector;

        public ScreenController(ILogger<ScreenController> logger, Connector connector)
        {
            this._logger = logger;
            this._connector = connector;

            _connector.Setup(_PIN, System.Device.Gpio.PinMode.InputPullUp);
        }

        [HttpGet]
        public bool Get()
        {
            return _connector.Status(_PIN);
        }
    }
}
