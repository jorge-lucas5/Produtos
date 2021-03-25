using System.Linq;
using Estudos.App.Business.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Estudos.App.Business.Services
{
    public abstract class BaseService
    {
        protected void Notificar(string mensagem)
        {
            //propagar erro até a camada de apresentação
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