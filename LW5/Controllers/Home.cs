using LW5.BackgroundModules;
using LW5.Views.ViewModels;
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
            string converted = Hamming.ConvertToBits(textToSend);
            return View(new ConvertOnlyViewModel() { ConvertedText = converted, NotConvertedText = textToSend });
        }

        // проверка текста на правильность передачи
        public ActionResult Check(string textToCheck, bool isCheck)
        {
            return View();
        }
    }
}
