using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Estudos.App.Business.Models;

namespace Estudos.App.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<bool> Existe(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity,bool>> expression);
        Task<int> SaveChanges();
    }
}