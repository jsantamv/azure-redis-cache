using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using StackExchange.Redis;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SportsStatsTracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Program prg = new Program();

            
            Console.WriteLine("Salvado Datos");
            await prg.SaveBigDataAsync();

            Console.WriteLine("Leyendo Datos");
            await prg.ReadDataAsync();

            Console.WriteLine("----------------------------");
            Console.WriteLine("");


            Console.WriteLine("Alamacenando un Objeto Json con GUID");
            await prg.SaveObjectJson();


            Console.WriteLine("----------------------------");
            Console.WriteLine("");
            //Ejemplo de la practica.
            await prg.ParcticeExampleAsync();

        }

        public async Task ReadDataAsync()
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            var devicesCount = 10;
            for (int i = 0; i < devicesCount; i++)
            {
                var value = await cache.StringGetAsync($"Device_Status:{i}");
                Console.WriteLine($"Valor={value}");
            }
        }

        public async Task SaveBigDataAsync()
        {
            var devicesCount = 10;
            var rnd = new Random();
            var cache = RedisConnectorHelper.Connection.GetDatabase();

            for (int i = 1; i < devicesCount; i++)
            {
                var value = rnd.Next(0, 100);
                await cache.StringSetAsync($"Device_Status:{i}", value);
            }
        }

        public async Task SaveObjectJson()
        {
            var person = new Person();

            Guid g = Guid.NewGuid();
            Console.WriteLine(g);

            var cache = RedisConnectorHelper.Connection.GetDatabase();

            await cache.StringSetAsync(g.ToString(), JsonConvert.SerializeObject(person));

            var value = await cache.StringGetAsync(g.ToString());
            Console.WriteLine($"Valor={value}");



        }


        public async Task ParcticeExampleAsync()
        {
            //Aca el ejemplo tal cual Microsoft Learn
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //Esta responsabilidad debe estar en una unica clase como singleton
            string connectionString = config["CacheConnection"];

            using (var cache = ConnectionMultiplexer.Connect(connectionString))
            {
                IDatabase db = cache.GetDatabase();

                bool setValue = db.StringSet("test:key", "Juan Carlos");
                Console.WriteLine($"SET: {setValue}");

                string getValue = db.StringGet("test:key");
                Console.WriteLine($"GET: {getValue}");


                // Simple get and put of integral data types into the cache
                setValue = await db.StringSetAsync("test", "100");
                Console.WriteLine($"SET: {setValue}");

                getValue = await db.StringGetAsync("test");
                Console.WriteLine($"GET: {getValue}");

                long newValue = await db.StringIncrementAsync("counter", 50);
                Console.WriteLine($"INCR new value = {newValue}");

                var result = await db.ExecuteAsync("ping");
                Console.WriteLine($"PING = {result.Type} : {result}");

                result = await db.ExecuteAsync("flushdb");
                Console.WriteLine($"FLUSHDB = {result.Type} : {result}");
            }
        }

    }
}