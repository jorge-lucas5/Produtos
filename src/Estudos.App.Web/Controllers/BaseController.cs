using Estudos.App.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected void NotificacaoSucesso(string mensagem)
        {
            TempData["Sucesso"] = mensagem;
        }
    }
}