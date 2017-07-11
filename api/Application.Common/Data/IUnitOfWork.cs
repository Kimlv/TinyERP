namespace App.Common.Data
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        RepositoryType RepositoryType { get; }
        IDbContext Context { get; }
        void Commit();
    }
}