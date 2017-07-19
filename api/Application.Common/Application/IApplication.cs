namespace App.Common.Application
{
    public interface IApplication
    {
        void OnApplicationInitialized();
        void OnApplicationStarted();
        void OnApplicationRequestStarted();
        void OnApplicationRequestEnded();
        void OnUnHandledError();
        void OnApplicationEnded();
        void Config<IApp>(IApp app);
    }
}