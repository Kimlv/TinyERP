namespace App.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using App.Common.Tasks;
    using Mapping;

    public class AssemblyHelper
    {
        public static string GetBinDirectory()
        {
            var binFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().EscapedCodeBase).Replace("file:\\", string.Empty);
            return binFolderPath;
        }

        public static void ExecuteTasks<TaskType, ContextType>(ContextType context, AssemblyType lookupIn) where TaskType : IOrderedExecutable<ContextType>
        {
            string assemblyName = AssemblyHelper.GetAssemblyNameBaseOnAssemblyType(lookupIn);
            IEnumerable<Type> tasksToRun = AssemblyHelper.GetTypes<TaskType>(assemblyName);
            foreach (var task in tasksToRun)
            {
                TaskType instance = (TaskType)Activator.CreateInstance(task);
                instance.Execute(context);
            }
        }

        public static void ExecuteTasks<TaskType, ContextType>(ContextType context, bool runTasksInOrder = false) where TaskType : IExecutable<ContextType>
        {
            IEnumerable<Type> tasksToRun = AssemblyHelper.GetTypes<TaskType>();
            if (runTasksInOrder)
            {
                AssemblyHelper.RunTasksInOrder<TaskType, ContextType>(tasksToRun, context);
                return;
            }

            foreach (var task in tasksToRun)
            {
                TaskType instance = (TaskType)Activator.CreateInstance(task);
                instance.Execute(context);
            }
        }

        private static void RunTasksInOrder<TaskType, ContextType>(IEnumerable<Type> tasks, ContextType context) where TaskType : IExecutable<ContextType>
        {
            IList<IOrderedExecutable<ContextType>> taskInstances = new List<IOrderedExecutable<ContextType>>();
            foreach (var task in tasks)
            {
                IOrderedExecutable<ContextType> instance = (IOrderedExecutable<ContextType>)Activator.CreateInstance(task);
                taskInstances.Add(instance);
            }

            taskInstances = taskInstances.OrderBy(task => task.Order).ToList();
            foreach (var instance in taskInstances)
            {
                instance.Execute(context);
            }
        }

        public static IEnumerable<Type> GetTypes<ExpectedType>()
        {
            return GetTypes<ExpectedType>(string.Empty);
        }

        public static IEnumerable<Type> GetTypes<ExpectedType>(string filePattern = "*.dll")
        {
            filePattern = string.IsNullOrWhiteSpace(filePattern) ? "*.dll" : filePattern;
            IEnumerable<Type> types = new List<Type>();

            IList<string> dlls = AssemblyHelper.GetApplicationDlls(filePattern);
            foreach (string assemblyName in dlls)
            {
                var taskInFile = Assembly.Load(assemblyName).GetTypes()
                    .Where(t => !t.IsAbstract && t.IsClass && typeof(ExpectedType).IsAssignableFrom(t)).ToList();
                types = types.Union(taskInFile);
            }

            return types;
        }

        public static IEnumerable<IMappingRegistration> GetAllMappingRegistrations(string filePattern = "*.dll")
        {
            IEnumerable<IMappingRegistration> types = new List<IMappingRegistration>();
            IList<string> dlls = GetApplicationDlls(filePattern);
            foreach (string assemblyName in dlls)
            {
                var taskInFile = Assembly.Load(assemblyName).GetTypes()
                    .Where(type => 
                        !type.IsAbstract && 
                        !type.IsInterface && 
                        type.GetInterfaces().Any(iinterface=> iinterface.IsGenericType && iinterface.GetGenericTypeDefinition()==typeof(IMappedFrom<>)))
                    .Select(item=>new MappingRegistration(item))
                    .ToList();
                types = types.Union(taskInFile);
            }
            return types;
        }

        public static IList<string> GetApplicationDlls(string filePattern)
        {
            var binFolderPath = AssemblyHelper.GetBinDirectory();
            IList<string> files =
                Directory.GetFiles(binFolderPath, filePattern)
                    .Where(file => (Path.GetFileNameWithoutExtension(file) ?? string.Empty).StartsWith(Constants.APPLICATION_NAMESPACE_START_WITH))
                    .Select(fileItem => Path.GetFileNameWithoutExtension(fileItem))
                    .ToList();
            return files;
        }

        private static string GetAssemblyNameBaseOnAssemblyType(AssemblyType lookupIn)
        {
            string assemblyName;
            switch (lookupIn)
            {
                case AssemblyType.Common:
                    assemblyName = "App.Common.dll";
                    break;
                case AssemblyType.Context:
                    assemblyName = "App.Context.dll";
                    break;
                case AssemblyType.Repository:
                    assemblyName = "App.Repository.dll";
                    break;
                case AssemblyType.Service:
                    assemblyName = "App.Service.dll";
                    break;
                default:
                    assemblyName = "App.Api.dll";
                    break;
            }

            return assemblyName;
        }
    }
}