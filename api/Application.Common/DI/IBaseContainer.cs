namespace App.Common.DI
{
    using System;
    using System.Collections.Generic;
    using App.Common.Data;
    using App.Common.Event;

    public interface IBaseContainer
    {
        object Instance { get; }
        void RegisterPerRequest(Type type);
        void RegisterPerRequest<IInstance>() where IInstance : class;
        void RegisterPerRequest<IInterface, TInstance>()
            where IInterface : class
            where TInstance : IInterface;
        IList<Type> ResolveAll(Type type);
        IList<IInterface> ResolveAll<IInterface>() where IInterface : class;
        void RegisterSingleton<IInterface, IInstance>() where IInstance : IInterface where IInterface : class;
        void RegisterSingleton<IInterface, IInstance>(string refName) where IInstance : IInterface where IInterface : class;
        void RegisterTransient(Type type);
        void RegisterTransient<IInterface, IInstance>()
            where IInstance : IInterface
            where IInterface : class;
        void RegisterTransient<IInterface, IInstance>(string refName)
            where IInstance : IInterface
            where IInterface : class;
        IInterface Resolve<IInterface>(string refName) where IInterface : class;
        IInterface Resolve<IInterface>() where IInterface : class;
        object Resolve(Type type);
        IInterface Resolve<IInterface>(IUnitOfWork unitOfWork) where IInterface : class;
    }
}