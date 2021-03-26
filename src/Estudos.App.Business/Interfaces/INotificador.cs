using System.Collections.Generic;
using Estudos.App.Business.Notificacoes;

namespace Estudos.App.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacoes();
        void  Handle(Notificacao notificacao);
    }
}