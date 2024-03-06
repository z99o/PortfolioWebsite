using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Events;

public static class DatabaseConnectionTemplate
{
            
    private static string userName = "";
    private static string password = "";
    private static string dbName = "";
    private static string clusterName = "";
    private static string appName = "";

    private static string connectionUri = "mongodb+srv://" + userName + ":" + password + "@" + clusterName + ".wztsur8.mongodb.net/" + dbName + "?retryWrites=true&w=majority&appName=" + appName;


    // Set the ServerApi field of the settings object to set the version of the Stable API on the client
    public static IMongoClient _DBClient = null;

    public static IMongoClient GetClient()
    {
        if(_DBClient == null)
        {
            _DBClient = CreateClient();
        }
        return _DBClient;
    }

    private static IMongoClient CreateClient()
    {
        var settings = MongoClientSettings.FromConnectionString(connectionUri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        return new MongoClient(settings);
    }
}