using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LW5.Controllers
{
    public class Home : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // текст для перевода в двоичный код
        public ActionResult TransferToBinary(string textToSend)
        {
            return View();
        }

        // проверка текста на правильность передачи
        public ActionResult Check(string textToCheck, bool isCheck)
        {
            return View();
        }
    }
}
