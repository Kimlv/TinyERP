namespace App.Common.Tasks
{
    public interface IOrderedExecutable<ContentType>: IExecutable<ContentType>
    {
        int Order { get; }
    }
}
