namespace App.Common.Tasks.Mapping
{
    using System;
    using System.Linq;
    using App.Common.Mapping;
    using App.Common.Helpers;
    using AutoMapper;

    public class AutoMapperConfigurationTask : BaseTask<TaskArgument<System.Web.HttpApplication>>, IApplicationStartedTask<TaskArgument<System.Web.HttpApplication>>
    {
        public AutoMapperConfigurationTask() : base(ApplicationType.All)
        {
        }

        public override void Execute(TaskArgument<System.Web.HttpApplication> arg)
        {
            if (!this.IsValid(arg.Type)) { return; }
            var maps = AssemblyHelper.GetAllMappingRegistrations();
            this.ConfigStandardMappings(maps.ToArray());
        }

        private void ConfigStandardMappings(IMappingRegistration[] maps)
        {
            //var maps = (from type in types
            //            from i in type.GetInterfaces()
            //            where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMappedFrom<>)
            //            && !type.IsAbstract && !type.IsInterface
            //            select new
            //            {
            //                Source = i.GenericTypeArguments[0],
            //                Dest = type,
            //                isCustomMap = typeof(ICustomMap<>).IsAssignableFrom(type)
            //            }).ToArray();
            foreach (var map in maps)
            {
                AutoMapper.Mapper.CreateMap(map.From, map.To);
                AutoMapper.Mapper.CreateMap(map.To, map.From);
            }
        }
    }
}