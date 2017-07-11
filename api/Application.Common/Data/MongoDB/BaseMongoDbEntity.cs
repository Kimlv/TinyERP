namespace App.Common.Data.MongoDB
{
    using global::MongoDB.Bson;
    using global::MongoDB.Kennedy;
    using System;

    public class BaseMongoDbEntity : IBaseEntity<ObjectId>, IMongoEntity
    {
        public ObjectId Id { get; set; }
        public string _accessId { get; set; }
        public ObjectId _id { get { return this.Id; } }
        public DateTime CreatedDate { get; set; }
        public BaseMongoDbEntity()
        {
            this.CreatedDate = DateTime.UtcNow;
        }
    }
}
