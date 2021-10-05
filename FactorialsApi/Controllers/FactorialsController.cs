using FactorialsApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetResultByValue(int? value)
        {
            var factorial = await _context.Factorials.FirstOrDefaultAsync(f => f.Value == value);

            if (factorial == null)
            {
                long result = 1;
                for (int i = 1; i <= value; i++)
                {
                    result *= i;
                }

                await _context.Factorials.AddAsync(new Factorial
                {
                    Value = value,
                    Result = result
                });
                await _context.SaveChangesAsync();

                return Ok(result);
            }

            return Ok(factorial.Result);
        }

        [HttpGet("factorials/{value}/nearest-value")]
        [ServiceFilter(typeof(ValidateParametersActionFilter))]
        public async Task<IActionResult> GetNearestValueByValue(int? value)
        {
            var factorials = await _context.Factorials.ToListAsync();

            int? left = 0;
            int? right = 20;
            foreach (var item in factorials)
            {
                if (item.Value < value && item.Value > left)
                    left = item.Value;
                if (item.Value > value && item.Value < right)
                    right = item.Value;
            }

            var leftFact = await _context.Factorials.FirstOrDefaultAsync(f => f.Value == left);
            var rightFact = await _context.Factorials.FirstOrDefaultAsync(f => f.Value == right);

            return Ok(new { left = leftFact == null ? null : leftFact.Value, right = rightFact == null ? null : rightFact.Value });
        }

        [HttpGet("value/{result}/nearest-factorials")]
        [ServiceFilter(typeof(ValidateParametersActionFilter))]
        public async Task<IActionResult> GetNearestValueByResult(long? result)
        {
            var factorials = await _context.Factorials.ToListAsync();

            long? left = 0;
            long? right = 2432902008176640000;
            foreach (var item in factorials)
            {
                if (item.Result < result && item.Result > left)
                    left = item.Result;
                if (item.Result > result && item.Result < right)
                    right = item.Result;
            }

            var leftFact = await _context.Factorials.FirstOrDefaultAsync(f => f.Result == left);
            var rightFact = await _context.Factorials.FirstOrDefaultAsync(f => f.Result == right);

            return Ok(new { left = leftFact == null ? null : leftFact.Value, right = rightFact == null ? null : rightFact.Value });
        }
    }
}
