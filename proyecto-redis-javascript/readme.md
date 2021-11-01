# Inicializar el programa
 mkdir redisapp
 cd redisapp
 npm init -y
 touch app.js

# Libreria para trabajar con JS

[Doc](https://docs.microsoft.com/es-es/learn/modules/optimize-your-web-apps-with-redis/6-exercise-connect-an-app-to-the-cache?pivots=javascript)

```
npm install redis bluebird dotenv
```

- redis: el paquete de JavaScript que se usa con más frecuencia para conectarse a Redis.
- bluebird: se usa para convertir los métodos de estilo de devolución de llamada del paquete redis en promesas que se pueden esperar.
- dotenv: carga variables de entorno desde un archivo .env, que es donde se almacenará la información de conectividad de Redis.

