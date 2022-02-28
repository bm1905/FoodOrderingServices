namespace Catalog.API.DataAccess.Persistence.Configurations
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProductCollectionName { get; set; }
    }
}
