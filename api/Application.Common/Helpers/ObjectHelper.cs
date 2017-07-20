namespace App.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Event;
    using Validation.Attribute;
    using Newtonsoft.Json;

    public class ObjectHelper
    {
        public static TEntity Convert<TEntity>(object obj)
        {
            return AutoMapper.Mapper.Map<TEntity>(obj);
        }

        internal static void Invoke<TEventType>(object handler, string methodName, TEventType arg) where TEventType : IEvent
        {
            MethodInfo method = handler.GetType().GetMethod(methodName, new Type[] { arg.GetType() });
            method.Invoke(handler, new object[] { arg });
        }

        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj); ;
        }

        internal static string GetClassName<TEventType>(TEventType ev) where TEventType : IEvent
        {
            return ev == null ? String.Empty : ev.GetType().FullName;
        }

        internal static TEntity CreateInstance<TEntity>() where TEntity : class
        {
            return Activator.CreateInstance<TEntity>();
        }
        internal static object CreateInstance(Type type, object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        public static IList<ValidationRequest> GetPropertyAttributes<TValidator>(object obj) where TValidator : BaseAttribute
        {
            IList<ValidationRequest> validators = new List<ValidationRequest>();
            var properties = obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty).Select(
                    item => item.GetCustomAttributes<TValidator>()
                            .Select(validator => new ValidationRequest(validator, item.GetValue(obj, null), item.PropertyType.FullName))
                            .ToList());
            foreach (IEnumerable<ValidationRequest> attrs in properties)
            {
                if (attrs.Count() == 0) { continue; }
                validators = validators.Concat(attrs.ToList()).ToList();
            }

            return validators;
        }
    }
}