const Promise = require("bluebird");
const redis = require("redis");
require("dotenv").config();


Promise.promisifyAll(redis);

const client = redis.createClient(
    process.env.REDISPORT,
    process.env.REDISHOSTNAME,
    {
        password: process.env.REDISKEY,
        tls: { servername: process.env.REDISHOSTNAME }
    }
);

/**
 * Metodo asincrono para realizar una prueba
 * del llamado del redis por medio de JS
 */
const CallRedisTest = async () => {

    console.log("Adding value to the cache");
    await client.setAsync("mi_llave", "esta es mi llave");

    console.log("Reading value back:");
    console.log(await client.getAsync("mi_llave"));

    console.log("Pinging the cache");
    console.log(await client.pingAsync());

    //Elimine todas las claves de la caché con flushdbAsync
    await client.flushdbAsync();

    //Por último, cierre la conexión con quitAsync
    await client.quitAsync();
}

CallRedisTest();

