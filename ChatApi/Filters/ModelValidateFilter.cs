using ChatApi.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ChatApi.Filters
{
    public class ModelValidateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);

                actionContext.Response =  actionContext.Request.CreateResponse<ApiResponse>(new ApiResponse { IsValidUser = true, Message = "Invalid Model", MessageType = -2,DataList = actionContext.ModelState });


            }
        }
    }
}