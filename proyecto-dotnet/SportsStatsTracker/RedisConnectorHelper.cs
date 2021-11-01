using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

// Summary: 
//      ConnectionMultiplexer was designed for code sharing by the whole application, 
//      is not necessary to create a new instance every time you need a simple operation. 
//      Creating a new instance every time we need a cache, the performance may be affected. 
//      In addition, if we are using Redis cache from Azure, there is a limit connection 
//      so if we create a new connection each time we need the cache, this limit can be exceeded.
public class RedisConnectorHelper  
{                  
    static RedisConnectorHelper()  
    {  
        var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //Esta responsabilidad debe estar en una unica clase como singleton
            string connectionString = config["CacheConnection"];

        RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>  
        {  
            return ConnectionMultiplexer.Connect(connectionString);  
        });  
    }  
      
    private static Lazy<ConnectionMultiplexer> lazyConnection;          
  
    public static ConnectionMultiplexer Connection  
    {  
        get  
        {  
            return lazyConnection.Value;  
        }  
    }  
}  