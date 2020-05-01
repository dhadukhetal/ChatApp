using OperatorWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApi.Controllers
{
    public class OperatorController : Controller
    {
        // GET: Operator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetVariable(vmOperator vmOperator)
        {
            Session["OperatorId"] = vmOperator.OperatorId;
            Session["Operator_DisplayName"] = vmOperator.DisplayName;
            Session["ChatSessionId"] = vmOperator.ChatSessionId;
            return this.Json(new { success = true });
        }

    }
}