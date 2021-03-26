using System.Collections.Generic;
using System.Linq;
using Estudos.App.Business.Interfaces;

namespace Estudos.App.Business.Notificacoes
{
    public class Notificacao
    {
        public string Mensagen { get; }

        public Notificacao(string mensagen)
        {
            Mensagen = mensagen;
        }
    }

    public class Notificador : INotificador
    {
        private readonly List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }
    }
}