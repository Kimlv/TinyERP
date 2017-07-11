namespace App.Service.Impl.Common
{
    using System.Collections.Generic;
    using App.Entity.Common;
    using App.Common.Data;
    using App.Common;
    using App.Repository.Common;

    internal class LanguageService : Service.Common.ILanguageService
    {
        public void Add(IList<Language> languages)
        {
            using (IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                ILanguageRepository repository = App.Common.DI.IoC.Container.Resolve<ILanguageRepository>();
                foreach (Language item in languages)
                {
                    repository.AddIfNotExist(item);
                }

                uow.Commit();
            }
        }
    }
}
