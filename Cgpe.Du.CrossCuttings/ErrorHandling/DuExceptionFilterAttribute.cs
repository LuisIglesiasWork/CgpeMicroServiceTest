//using Cgpe.LogClient;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;
using System.Text;

namespace Cgpe.Du.CrossCuttings
{

    public class DuExceptionFilterAttribute : ExceptionFilterAttribute
    {
        
        public override void OnException(ExceptionContext context)
        {
           //CgpeLogClient logService = new CgpeLogClient(DuMessageBusManager.MessageBus);
            XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo("log4net.config"));
           // logService.Error(context.Exception.Message, null, context.HttpContext.User.Identity.Name, context.Exception);
            if(context.Exception is SecurityException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Result = new ForbidResult("DuAuth");
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(context.Exception);
            }
            base.OnException(context);
        }

    }

}
