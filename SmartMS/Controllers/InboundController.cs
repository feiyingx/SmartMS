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
            //Get the incoming message's information passed in by Twilio as a query string
            String incomingNumber = Request.QueryString["From"];
            String incomingMessage = Request.QueryString["Body"];

            //Check to make sure we have a message and a number
            if (incomingMessage != null && incomingNumber != null)
            {
                //Parse command
                //Normalize the message string by removing extra spaces and change to lower case
                incomingMessage = incomingMessage.Trim().ToLower();
                //Try to find the command of the message, if we have trouble parsing it then return invalid command error message
                int commandEndIndex = incomingMessage.IndexOf(' ');
                if (commandEndIndex < 0)
                {
                    String invalidCommandMessage = String.Format("<Response><Message>I couldn't understand your command. Please text 'menu' to see the list of commands.</Message></Response>");
                    return this.Content(invalidCommandMessage, Constants.ContentType.XML); 
                }

                //Extract the command string and update the remaining part of the message
                String command = incomingMessage.Substring(0, commandEndIndex);
                String remainingMessage = incomingMessage.Substring(commandEndIndex + 1).Trim(); //Trim again to remove any extra spaces
                switch (command)
                {
                    case Constants.SMSCommands.Bart:
                        break;
                    case Constants.SMSCommands.Food:
                        break;
                    case Constants.SMSCommands.Menu:
                        break;
                    case Constants.SMSCommands.Navigation:
                        break;
                    case Constants.SMSCommands.Score:
                        break;
                    case Constants.SMSCommands.Set:
                        break;
                    case Constants.SMSCommands.Tip:
                        return ProcessTip(remainingMessage);
                    default:
                        break;
                }
            }
            
            //If we don't have any incoming message or number then return some kind of notice
            String errorMessage = String.Format("<Response><Message>Uh oh, I don't think we are meant to be. So let's just let it be.</Message></Response>");
            return this.Content(errorMessage, Constants.ContentType.XML);
        }

        private ActionResult ProcessTip(String message)
        {
            //Tip command strings should look like:
            //"tip xxx.xx"
            //The message parameter should already have the command part removed
            //So let's try to extract the amount that we are calculating the tip for
            decimal billAmount = 0;
            bool canParseBillAmount = Decimal.TryParse(message, out billAmount);
            //Default to an error message
            String resultMessage = "I couldn't read the amount, please text me again in the example format 'tip 120.68'.";
            if (canParseBillAmount)
            {
                //Format tip amounts in currency format
                resultMessage = String.Format(
@"18% - {0:c}
15% - {1:c}
13% - {2:c} :(
10% - {3:c} :'(", billAmount*.18m, billAmount*.15m, billAmount*.13m, billAmount*.10m);
            }
            
            return this.Content(BuildTwiML(resultMessage), Constants.ContentType.XML); 
        }
        private String BuildTwiML(String response)
        {
            return String.Format("<Response><Message>{0}</Message></Response>", response);
        }
    }
}
