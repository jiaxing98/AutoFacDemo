namespace AutoFacDemo.Data
{
    public interface IMongoDBSettings
    {
        string Host { get; set; }
        int Port { get; set; }
        string User { get; set; }
        string Password { get; set; }
        string DatabaseName { get; set; }
        string ConnectionString { get; }
    }

    public class MongoDBSettings : IMongoDBSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString
        {
            get
            {
                if(User != null && Password != null)
                    return $"mongodb://{User}:{Password}@{Host}:{Port}";
                else
                    return $"mongodb://{Host}:{Port}";
            }
        }
    }
}
