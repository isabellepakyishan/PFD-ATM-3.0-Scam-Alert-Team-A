using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class AuthenticationErrorController : Controller
    {
        public IActionResult Index()
        {
            //int? counter = HttpContext.Session.GetInt32("PinEntryCount");

            //if (counter == null)
            //    HttpContext.Session.SetInt32("PinEntryCount", 1);
            //else HttpContext.Session.SetInt32("PinEntryCount", (int)(counter + 1));

            int count = HttpContext.Session.GetInt32("PinEntryCount") ?? 0;
            var pin = HttpContext.Session.GetString("EnteredPinNo");
            var hasStar = pin.EndsWith('*');

            string first6Pin = pin.Remove(6);

            if (hasStar)
            {
                count += 1;
                HttpContext.Session.SetInt32("PinEntryCount", count);
            }

            if (count > 3) return RedirectToAction("Index", "ServiceUnavailable");

            return View();
        }
        public ActionResult CheckPINAgain(IFormCollection form)
        {
            string pin1 = form["pin1"];
            string pin2 = form["pin2"];
            string pin3 = form["pin3"];
            string pin4 = form["pin4"];
            string pin5 = form["pin5"];
            string pin6 = form["pin6"];
            string pin7 = form["pin7"];

            string reenteredPin = pin1 + pin2 + pin3 + pin4 + pin5 + pin6 + pin7;

            var storedPin = HttpContext.Session.GetString("PinNo");

            if (reenteredPin.Length == 7)
            {
                char lastPin = reenteredPin[reenteredPin.Length - 1];
                string first6Pin = reenteredPin.Remove(6);

                if (lastPin == '*' && first6Pin == storedPin)
                {
                    return RedirectToAction("Index", "AuthenticationError");
                }
                else
                {
                    return RedirectToAction("Index", "IncorrectPinNo");
                }
            }
            else
            {
                if (reenteredPin == storedPin)
                {
                    return RedirectToAction("Index", "DispenseCash");
                }
            }

            return View();
        }
    }
}
