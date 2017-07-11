namespace App.Common
{
    public interface IClonable<TEntity> where TEntity : class
    {
        TEntity Clone();
    }
}
