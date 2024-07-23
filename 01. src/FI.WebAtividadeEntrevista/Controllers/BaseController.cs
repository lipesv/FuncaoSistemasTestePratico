using FI.AtividadeEntrevista.BLL.Exceptions;
using FI.WebAtividadeEntrevista.Configuration.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public abstract class BaseController : Controller
    {
        protected JsonResult JsonResponse(HttpStatusCode statusCode, object data = null, List<string> mensagens = null)
        {
            var response = new
            {
                Sucesso = statusCode != HttpStatusCode.BadRequest && statusCode != HttpStatusCode.InternalServerError,
                Dados = data,
                Mensagens = mensagens ?? new List<string>()
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonResponse(bool success, List<Notification> notifications, string html = "")
        {
            var response = new JsonResponse
            {
                Success = success,
                Notifications = notifications,
                Html = html
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonResponse(bool success, string message, string type, string html = "")
        {
            return JsonResponse(success, new List<Notification> { new Notification { Message = message, Type = type } }, html);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        protected JsonResult JsonErrorResponse(string viewName)
        {
            var errorHtml = RenderPartialViewToString(viewName, ViewData.Model);

            var errorResponse = new JsonResponse
            {
                Success = false,
                Notifications = ModelState.Values.SelectMany(v => v.Errors)
                                                 .Select(e => new Notification { Message = e.ErrorMessage, Type = "error" })
                                                 .ToList(),
                Html = errorHtml
            };

            return Json(errorResponse, JsonRequestBehavior.AllowGet);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is ValidationException validationException)
            {
                filterContext.Result = JsonResponse(validationException.StatusCode, mensagens: new List<string> { validationException.Message });
            }
            else
            {
                filterContext.Result = JsonResponse(HttpStatusCode.InternalServerError, mensagens: new List<string> { $"<strong>Ocorreu um erro inesperado</strong>!<br/ ><br/ >{filterContext.Exception.Message}" });
            }

            filterContext.ExceptionHandled = true;

            base.OnException(filterContext);
        }
    }
}