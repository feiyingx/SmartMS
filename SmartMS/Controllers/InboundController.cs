using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartMS.Controllers
{
    public class InboundController : Controller
    {
        public ActionResult Index()
        {
            String incomingNumber = Request.QueryString["From"];
            String incomingMessage = Request.QueryString["Body"];
            return View();
        }

    }
}
