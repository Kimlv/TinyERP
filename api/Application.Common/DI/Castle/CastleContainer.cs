namespace App.Common.DI.Castle
{
    using System;
    using global::Castle.Windsor;
    using global::Castle.MicroKernel.Registration;
    using App.Common.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class CastleContainer : IBaseContainer
    {
        #region Constructor and Properties
        public object Instance
        {
            get
            {
                return this.windsorContainer;
            }
        }

        private readonly IWindsorContainer windsorContainer;
        public CastleContainer(IWindsorContainer windsorContainer)
        {
            this.windsorContainer = windsorContainer;
        }

        #endregion

        #region Singleton
        public void RegisterSingleton<TInterface, TInstance>()
            where TInterface : class
            where TInstance : TInterface
        {
            this.windsorContainer.Register(Component.For<TInterface>().ImplementedBy<TInstance>());
        }

        public void RegisterSingleton<TInterface, TInstance>(string refName)
            where TInterface : class
            where TInstance : TInterface
        {
            this.windsorContainer.Register(Component.For<TInterface>().ImplementedBy<TInstance>().Named(refName));
        }

        #endregion

        #region Transient
        public void RegisterTransient(Type type)
        {
            this.windsorContainer.Register(Component.For(type).LifestyleTransient());
        }

        public void RegisterTransient<TInterface, TInstance>()
            where TInterface : class
            where TInstance : TInterface
        {
            this.windsorContainer.Register(Component.For<TInterface>().ImplementedBy<TInstance>().LifestyleTransient());
        }

        public void RegisterTransient<TInterface, TInstance>(string refName)
            where TInterface : class
            where TInstance : TInterface
        {
            this.windsorContainer.Register(Component.For<TInterface>().ImplementedBy<TInstance>().Named(refName).LifestyleTransient());
        }

        #endregion

        #region PerRequest
        public void RegisterPerRequest<TInstance>() where TInstance : class
        {
            this.windsorContainer.Register(Component.For<TInstance>().LifestylePerWebRequest());
        }

        public void RegisterPerRequest(Type type)
        {
            this.windsorContainer.Register(Component.For(type).LifestylePerWebRequest());
        }

        public void RegisterPerRequest<TInterface, TInstance>()
            where TInterface : class
            where TInstance : TInterface
        {
            this.windsorContainer.Register(Component.For<TInterface>().ImplementedBy<TInstance>().LifestylePerWebRequest());
        }

        #endregion

        #region Resolve
        public TInterface Resolve<TInterface>(string refName) where TInterface : class
        {
            return this.windsorContainer.Resolve<TInterface>(refName);
        }

        public TInterface Resolve<TInterface>() where TInterface : class
        {
            return this.windsorContainer.Resolve<TInterface>();
        }

        public object Resolve(Type type)
        {
            return this.windsorContainer.Resolve(type);
        }

        public TInterface Resolve<TInterface>(IUnitOfWork unitOfWork) where TInterface : class
        {
            return this.windsorContainer.Resolve<TInterface>(new { uow = unitOfWork });
        }

        public IList<IInterface> ResolveAll<IInterface>() where IInterface : class
        {
            return this.windsorContainer.ResolveAll<IInterface>();
        }

        public IList<Type> ResolveAll(Type type)
        {
            return new List<Type>((IEnumerable<Type>)this.windsorContainer.ResolveAll(type));
        }

        #endregion
    }
}