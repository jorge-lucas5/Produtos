using System.Diagnostics;
using Estudos.App.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Estudos.App.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id}")]
        public IActionResult Error(int id)
        {
            var erro = new ErrorViewModel();
            if (id == 404)
            {
                erro.Mensagem = "A página que você está procurando não existe. <br/>" +
                                "Em caso de dúvidas entre em contato com o suporte.";
                erro.Titulo = "Ops! Página não encontrada";
            }
            else if (id == 403)
            {
                erro.Mensagem = "Você não tem permissão para fazer isso!";
                erro.Titulo = "Acesso Negado";
            }
            erro.ErroCode = id;

            return View(erro);
        }
    }
}
