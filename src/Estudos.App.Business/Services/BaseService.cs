using System.Linq;
using Estudos.App.Business.Interfaces;
using Estudos.App.Business.Models;
using Estudos.App.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace Estudos.App.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(string mensagem)
        {
           _notificador.Handle(new Notificacao(mensagem));
        }

        protected void Notificar(ValidationResult validationResult)
        {
            validationResult.Errors.ToList()
                .ForEach(a => Notificar(a.ErrorMessage));
        }

        protected bool ExecutarValidacao<TValidacao, TEntidade>(TValidacao validacao, TEntidade entidade)
            where TValidacao : AbstractValidator<TEntidade>
            where TEntidade : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);
            return false;
        }
    }
}