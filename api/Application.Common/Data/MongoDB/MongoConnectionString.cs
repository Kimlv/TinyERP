namespace App.Common.Data.MongoDB
{
    public class MongoConnectionString : ConnectionString
    {
        public MongoConnectionString(string connectionName = "")
            : base(RepositoryType.MongoDb, connectionName)
        {
        }
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this.UserName))
            {
                return string.Format(
                "mongodb://{0}:{1}/{2}",
                this.Server,
                this.Port,
                this.Database
                );
            }
            return string.Format(
                "mongodb://{3}:{4}@{0}:{1}/{2}",
                this.Server,
                this.Port,
                this.Database,
                this.UserName,
                this.Password
                );
        }
    }
}
