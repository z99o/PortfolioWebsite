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
	private static IMongoClient _DBClient = null;

	private static IMongoClient GetClient()
	{
		//throw if connection failed
		if (_DBClient == null)
		{
			_DBClient = CreateClient();
		}
		return _DBClient;
	}

	public static IMongoDatabase GetDatabase()
	{
		try
		{
			return GetClient().GetDatabase("PortfolioWebsiteDB");
		}
		catch (System.Exception ex)
		{
			throw new System.Exception("Error connecting to the database: " + ex.Message);
		}
	}

	private static IMongoClient CreateClient()
	{
		var settings = MongoClientSettings.FromConnectionString(connectionUri);
		settings.ServerApi = new ServerApi(ServerApiVersion.V1);
		IMongoClient client = new MongoClient(settings);
		//if connection failed
		if (client == null)
		{
			throw new System.Exception("Connection to the database failed");
		}
		return client;
	}
}