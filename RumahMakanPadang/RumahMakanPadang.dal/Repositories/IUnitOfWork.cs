using Microsoft.EntityFrameworkCore.Storage;
using RumahMakanPadang.dal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RumahMakanPadang.dal.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Masakan> MasakanRepository { get; }
        //IBaseRepository<Author> AuthorRepository { get; }
        void Save();
        Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
        IDbContextTransaction StartNewTransaction();
        Task<IDbContextTransaction> StartNewTransactionAsync();
        //Task<int> ExecuteSqlCommandAsync(string sql, object[] parameters, CancellationToken cancellationToken = default(CancellationToken));
    }
}
