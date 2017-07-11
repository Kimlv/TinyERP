namespace App.Common.Tasks
{
    public interface IExecutable<ContentType>
    {
        void Execute(ContentType context);
    }
}
