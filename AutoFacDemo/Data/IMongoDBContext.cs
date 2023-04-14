using MongoDB.Driver;

namespace AutoFacDemo.Data
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}