using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApi.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            //Session["OperatorID"] = 1;
            ViewBag.OperatorId = Session["OperatorId"].ToString();
            ViewBag.Operator_DisplayName = Session["Operator_DisplayName"].ToString();
            return View();
        }
    }
}