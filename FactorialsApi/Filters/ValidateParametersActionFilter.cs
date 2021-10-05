using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialsApi.Filters
{
    public class ValidateParametersActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var arguments = context.ActionArguments;

            var param = arguments.SingleOrDefault().Value;

            if (arguments.ContainsKey("value"))
            {
                int value = Convert.ToInt32(param);
                if (value > 20 || value < 0)
                {
                    context.Result = new BadRequestResult();
                    return;
                }
            }

            if (arguments.ContainsKey("result"))
            {
                long result = Convert.ToInt64(param);
                if (result > 2432902008176640000 || result < 0)
                {
                    context.Result = new BadRequestResult();
                    return;
                }
            }
        }
    }
}
