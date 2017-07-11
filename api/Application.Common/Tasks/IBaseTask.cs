namespace App.Common.Tasks
{
    public interface IBaseTask<ContextType>: IOrderedExecutable<ContextType>
    {
        
        bool IsValid(ApplicationType type);
    }
}