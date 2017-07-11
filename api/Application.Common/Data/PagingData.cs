namespace App.Common.Data
{
    using System.Collections.Generic;

    public class PagingData<TEntity>
    {
        public int TotalPages { get; set; }
        public IList<TEntity> Items { get; set; }
        public PagingData()
        {
            this.TotalPages = 0;
            this.Items = new List<TEntity>();
        }
    }
}