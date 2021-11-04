const redis = require("redis")
const Bluebird = require("bluebird")
require("dotenv").config();

Bluebird.promisifyAll(redis.RedisClient.prototype);
Bluebird.promisifyAll(redis.Multi.prototype);

const redisPort = process.env.REDISPORT
const host = process.env.REDISHOSTNAME
const redisKey = process.env.REDISKEY
const client = redis.createClient(redisPort, host,
        {
            auth_pass: redisKey,
            tls: {
                servername: host
            }
        })

const testCache = async () => {

    console.log("Adding value to the cache");
    await client.setAsync("mi_llave", "esta es mi llave");

    console.log("Reading value back:");
    console.log(await client.getAsync("mi_llave"));

    console.log(await client.pingAsync())
    
    //Elimine todas las claves de la caché con flushdbAsync
    await client.flushdbAsync();

    //Por último, cierre la conexión con quitAsync
    await client.quitAsync();

}


testCache()
