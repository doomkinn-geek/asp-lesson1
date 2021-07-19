using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_lesson1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly List<Temperature> _holder;        

        private readonly ILogger<TemperatureController> _logger;

        public TemperatureController(ILogger<TemperatureController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public IActionResult Send([FromQuery] string date, int temperature)
        {
            try
            {
                _holder.Add(new Temperature { Date = Convert.ToDateTime(date), Value = temperature });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
        [HttpPut]
        public IActionResult Change([FromQuery] string date, int temperaature)
        {
            try
            {
                _holder[_holder.FindIndex(index => index.Date == Convert.ToDateTime(date))].Value = temperaature;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

    }
}
