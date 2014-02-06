using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartMS.Code;

namespace SmartMS.Controllers
{
    public class InboundController : Controller
    {
        public ActionResult Index()
        {
            String incomingNumber = Request.QueryString["From"];
            String incomingMessage = Request.QueryString["Body"];

            if (incomingMessage != null)
            {
                incomingMessage = incomingMessage.Trim();
                if (incomingMessage.ToLower() == "menu")
                {
                    String responseXml = String.Format("<Response><Message>I'm still a baby computer. I only know how to:</Message></Response>");
                    return this.Content(responseXml, Constants.ContentType.XML);
                }
            }
            String responseXml2 = String.Format("<Response><Message>Booya! I know your number: {0}</Message></Response>", incomingNumber);
            return this.Content(responseXml2, Constants.ContentType.XML);
        }
    }
}
