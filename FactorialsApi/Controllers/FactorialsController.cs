using FactorialsApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialsApi.Controllers
{
    [ApiController]
    public class FactorialsController : ControllerBase
    {
        private readonly FactorialsContext _context;

        public FactorialsController(FactorialsContext context)
        {
            _context = context;
        }

        [HttpGet("factorials/{value}")]
        [ServiceFilter(typeof(ValidateParametersActionFilter))]
        public IActionResult GetResultByValue(int? value)
        {
            return Ok(value);
        }

        [HttpGet("factorials/{value}/nearest-value")]
        [ServiceFilter(typeof(ValidateParametersActionFilter))]
        public IActionResult GetNearestValueByValue(int? value)
        {
            return Ok(value);
        }

        [HttpGet("value/{result}/nearest-factorials")]
        [ServiceFilter(typeof(ValidateParametersActionFilter))]
        public IActionResult GetNearestValueByResult(long? result)
        {
            return Ok(result);
        }
    }
}
