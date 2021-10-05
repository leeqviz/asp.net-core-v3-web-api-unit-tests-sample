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
        private readonly IRepository _repository;

        public FactorialsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("factorials/{value}")]
        [ServiceFilter(typeof(ValidateParametersActionFilter))]
        public async Task<IActionResult> GetResultByValue(int? value)
        {
            var factorial = await _repository.GetByValue(value);

            if (factorial == null)
            {
                long result = 1;
                for (int i = 1; i <= value; i++)
                {
                    result *= i;
                }

                await _repository.Create(new Factorial
                {
                    Value = value,
                    Result = result
                });
                await _repository.Save();

                return Ok(result);
            }

            return Ok(factorial.Result);
        }

        [HttpGet("factorials/{value}/nearest-value")]
        [ServiceFilter(typeof(ValidateParametersActionFilter))]
        public async Task<IActionResult> GetNearestValueByValue(int? value)
        {
            var factorials = await _repository.GetAll();

            int? left = 0;
            int? right = 20;
            foreach (var item in factorials)
            {
                if (item.Value < value && item.Value > left)
                    left = item.Value;
                if (item.Value > value && item.Value < right)
                    right = item.Value;
            }

            var leftFact = await _repository.GetByValue(left);
            var rightFact = await _repository.GetByValue(right);

            var leftResult = leftFact?.Value == null ? "null" : leftFact?.Value.ToString();
            var rightResult = rightFact?.Value == null ? "null" : rightFact?.Value.ToString();

            return Ok("{" + leftResult + ", " + rightResult + "}");
        }

        [HttpGet("values/{result}/nearest-factorials")]
        [ServiceFilter(typeof(ValidateParametersActionFilter))]
        public async Task<IActionResult> GetNearestValueByResult(long? result)
        {
            var factorials = await _repository.GetAll();

            long? left = 0;
            long? right = 2432902008176640000;
            foreach (var item in factorials)
            {
                if (item.Result < result && item.Result > left)
                    left = item.Result;
                if (item.Result > result && item.Result < right)
                    right = item.Result;
            }

            var leftFact = await _repository.GetByResult(left);
            var rightFact = await _repository.GetByResult(right);

            var leftResult = leftFact?.Value == null ? "null" : leftFact?.Value.ToString();
            var rightResult = rightFact?.Value == null ? "null" : rightFact?.Value.ToString();

            return Ok("{" + leftResult + ", " + rightResult + "}");
        }
    }
}
