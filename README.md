# AGRAJA-BACKEND: API para la contratación de agricultores y venta de cajas de productos

Agraja es mi proyecto de final de Bootcamp Fullstack y esta es la parte backend, puedes ver la parte fonted desde aquí 👉 https://github.com/aimarmun/AGRAJA-FRONT.
Se trata de una aplicación pensada para el administrador de una cooperativa de agricultura ecológica. 
El administrador o vendedor puede hacer ventas de cajas de productos o puede hacer contrataciones de agricultores con clientes.

Puedes ver una demo de Agraja en acción desde aquí 👉 https://demo.aimarmun.com

###### Características:

- Agraja Backend es una API para una aplicación que permite la contratación de agricultores y la venta de cajas de productos del campo. 

- La API está construida con .NET Core 6.0 Web API y utiliza Jwt Bearer tokens para autenticación. 

- Las contraseñas se encriptan con AES256. 

- Los agricultores pueden trabajar en cuatro tipos de campos: hortalizas, cereales, leguminosas y frutales. 

- Las cajas pueden ser de frutas, hortalizas, cereales o legumbres. 

- También se permite la inserción y modificación de nuevos clientes. Estos clientes pueden contratar los servicios de los agricultores o pueden comprar cajas. 

- Para el manejo de la aplicación existen dos tipos de perfiles: Administrador y Vendedor:
  
  - El **vendedor** puede hacer contrataciones de agricultores y ventas de cajas, pero no puede añadir ni modificar agricultores o cajas. El **administrador** puede hacer contrataciones y ventas de cajas, pero además puede modificar y añadir nuevas.

- La aplicación permite **MSSQL** o **MySQL** simplemente configurando el archivo `appsetings.json`. Para faciliar la implementación se añaden scripts de creación de base de datos en el directorio `scripts`. Si se prefiere tambíen se pueden crear migraciones con EF.

# Instalación

Para instalar y configurar el proyecto, sigue estos pasos:

1. Instala .NET Core 6.0.
2. Clona el repositorio de GitHub.
3. Abre el proyecto en Visual Studio.
4. Ejecuta el proyecto.

# Uso

Para utilizar esta API puedes elegir las siguientes alternativas:

- Utiliza el frontend de agraja https://github.com/aimarmun/AGRAJA-FRONT

- Utiliza Swagger  (se inicia en modo depuración y está documentado).

- Utiliza Postman.

- Curl

- ...

###### Ejemplos de uso:

Obtener ciudades con curl:

```bash
curl -X 'GET' \
  'http://localhost:5145/api/City' \
  -H 'accept: */*'
```

###### Configuraciones:

Se pueden reiniciar las credenciales configurando *"reset"* del objeto *"Users"* en true del archivo `appsetings.json`:

```json
"Users": {
  "reset": false
}
```

> ⚠️¡Cuidado! si lo dejas en *true*, cada vez que arranque el proyecto, se reiniciarán.



Configura tu base de datos con Microsoft SQL Server o MySQL:

```json
  "DatabaseCore": {
    "_comentario_core_": "Indica SQLSERVER para SQL Server o MYSQL para utilizar MySQL",
    "core": "SQLSERVER"
  },
  "ConnectionStrings": {
    "SQLconnectionString": "Server = localhost\\SQLEXPRESS; Database = Agraja; TrustServerCertificate=True; Integrated Security = true;",
    "MYSQLconnectionString": "Server=<servidor>;Port=<puerto>;Database=<base_de_Datos>;Uid=<usuario>;Pwd=<contraseña>;"
  },
```



Configura CORS facilmente con tu dominio:

```json
 "CORS": {
   "AllowAnyHeader": true,
   "AllowAnyMethod": true,
   "AllowAnyOrigin": true,
   "WithOrigins": "https://agraja-domain.com"
 },
```



> Nota: El archivo de configuración contiene comentarios descriptivos de casi todos los campos.

¡Gracias por visitar mi repositorio! ☺️
