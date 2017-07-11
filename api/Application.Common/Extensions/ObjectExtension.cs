namespace App.Common.Extensions
{
    using App.Common.Helpers;
    public static class ObjectExtension
    {
        /// <summary>
        /// Need to have mapp config for source and TEntity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TEntity To<TEntity>(this object source)
        {
            return ObjectHelper.Convert<TEntity>(source);
        }
    }
}
