using System;
using System.Linq;

namespace App.Common.Mapping
{
    public class MappingRegistration : IMappingRegistration
    {
        private Type item;

        public Type From { get; protected set; }
        public Type To { get; protected set; }
        /// <summary>
        /// item must be type of IMappedFrom<>
        /// </summary>
        /// <param name="item"></param>
        public MappingRegistration(Type item)
        {
            this.From = item;
            this.To = item.GetInterfaces().FirstOrDefault(iinterface => iinterface.IsGenericType && iinterface.GetGenericTypeDefinition() == typeof(IMappedFrom<>)).GenericTypeArguments[0];
        }
    }
}
