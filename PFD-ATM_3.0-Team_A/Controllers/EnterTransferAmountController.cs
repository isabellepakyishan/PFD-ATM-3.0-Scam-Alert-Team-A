using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class EnterTransferAmountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CheckPIN()
        {
            var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
            var storedPin = HttpContext.Session.GetString("PinNo");

            if (enteredPin.Length == 6)
            {
                if (enteredPin == storedPin)
                {
                    return RedirectToAction("Index", "TransferFunds");
                }
                else
                {
                    return RedirectToAction("Index", "IncorrectPinNo");
                }
            }
            else
            {
                char lastPin = enteredPin[enteredPin.Length - 1];
                string first6Pin = enteredPin.Remove(6);
                if (lastPin == '*' && first6Pin == storedPin)
                {
                    return RedirectToAction("Index", "AuthenticationError");
                }
                else
                {
                    return RedirectToAction("Index", "IncorrectPinNo");
                }
            }
            return null;
        }
    }
}

