using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace CompanyEmployees.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public ValidationFilterAttribute()
        {
            
        }
       
       

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // RouteData.Values dictionary, we can 
            // get the values produced by routes on the current routing pathSince we 

            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            // we use the ActionArguments dictionary to extract the DTO 
            //parameter that we send to the POST and PUT actions.
            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if (param != null)
            {
                context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller},action:{action}");
                return;
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
