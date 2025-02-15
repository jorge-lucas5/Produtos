﻿using System.Threading.Tasks;
using Estudos.App.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.Web.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());

            notificacoes.ForEach(c=> ViewData.ModelState.AddModelError(string.Empty, c.Mensagen));

            return View();
        }
    }
}