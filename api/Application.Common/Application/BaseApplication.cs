namespace App.Common.Application
{
    using System;
    using App.Common.Helpers;
    using App.Common.Tasks;

    public class BaseApplication<TContext> : IApplication
    {
        public TContext Context { get; private set; }
        public ApplicationType Type { get; private set; }
        public BaseApplication(TContext context, ApplicationType type)
        {
            this.Context = context;
            this.Type = type;
        }

        public virtual void OnApplicationStarted()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IApplicationStartedTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg, true);
            AssemblyHelper.ExecuteTasks<IApplicationReadyTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg, true);
        }

        public virtual void OnApplicationEnded()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IApplicationEndedTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg, true);
        }

        public virtual void OnApplicationRequestStarted()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IApplicationRequestStartedTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg, true);
        }

        public virtual void OnApplicationRequestEnded()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IApplicationRequestEndedTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg, true);
        }

        public virtual void OnUnHandledError()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IUnHandledErrorTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg, true);
        }

        public virtual void Config<IApp>(IApp app)
        {
            TaskArgument<IApp> taskArg = new TaskArgument<IApp>(app, this.Type);
            AssemblyHelper.ExecuteTasks<IConfigAppTask<TaskArgument<IApp>>, TaskArgument<IApp>>(taskArg, true);
        }

        public void OnApplicationInitialized()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IApplicationInitializedTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg, true);
        }
    }
}