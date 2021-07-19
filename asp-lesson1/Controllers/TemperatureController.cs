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
        private readonly TemperaturesHolder _holder;
        private string errMessage;
        public TemperatureController(TemperaturesHolder holder)
        {
            _holder = holder;
            errMessage = "";
        }
    
        [HttpGet]
        public IEnumerable<Temperature> Get(string inDate, string outDate)
        {
            try
            {
                return _holder.Get(Convert.ToDateTime(inDate), Convert.ToDateTime(outDate), out errMessage).ToArray();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        
        [HttpPost]
        public IActionResult Send([FromQuery] string date, int temperature)
        {            
            if(!_holder.Add(date, temperature, out errMessage))
            {
                return BadRequest(errMessage);
            }
            return Ok();
        }
        [HttpPut]
        public IActionResult Change([FromQuery] string date, int temperature)
        {            
            int errCode = _holder.Change(date, temperature, out errMessage);
            if (errCode == -1)
            {
                return BadRequest(errMessage);
            }            
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete([FromQuery] string inDate = "", string outDate = "")
        {
            return Ok();
        }

    }
}
