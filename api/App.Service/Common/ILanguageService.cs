namespace App.Service.Common
{
    using System.Collections.Generic;
    using App.Entity.Common;

    public interface ILanguageService
    {
        void Add(IList<Language> languages);
    }
}
