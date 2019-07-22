# API REST web de banca online
## Descripción
API REST de ejemplo para simular un sencillo servicio de banca online. El objetivo es utilizar una aplicación de muestra para su despliegue de manera serverless (AWS Lambda o Azure Functions). Por sencillez se deja sin implementar todo el tema de autenticación.

Se desarrollará también un cliente de una sola página (SPA) para interactuar con la API. Dicho cliente está disponible en https://github.com/pedroprieto/cliente-bank-app

## Instalación
1. Clonar repositorio
2. Instalar dependencias de .NET
```bash
dotnet restore
```
3. Instalar base de datos MySQL en el sistema
4. Actualizar los datos de conexión a la base de datos local en archivo `api-bank-app/appsettings.json`
```json
  "ConnectionStrings": {
    "BankDatabase": "server=localhost;port=3306;user=root;password=;database=bank"
  },
```
5. Actualizar base de datos
```bash
dotnet ef database update
```
6. Lanzar aplicación
7. Acceder a la API en `http://localhost:5000/api/clients`


## Instrucciones de despliegue

### AWS ElasticBeanstalk
1. Instalar proyecto y dependencias de .NET
2. Crear una nueva aplicación en ElasticBeanstalk
3. Crear un entorno dentro de la aplicación de ElasticBeanstalk
4. Crear una base de datos en AWS RDS de tipo MySQL. Se puede utilizar AWS Aurora.
5. Anotar los datos de conexión a la base de datos.
6. Configurar el grupo de seguridad de la base de datos para que permita el acceso desde cualquier IP de la VPC para que la aplicación Beanstalk tenga acceso
7. Crear archivo `api-bank-app/.ebextensions/db.config` a partir del que se proporciona como ejemplo en `api-bank-app/.ebextensions/db.config.example` utilizando los datos de conexión de la base de datos de RDS
8. Publicar la aplicación en AWS Beanstalk
9. Actualizar base de datos en RDS. (Nota: este punto se actualizará para que se pueda realizar de manera automática mediante las migraciones de .NET. Así, cada vez que se suba una nueva versión de la aplicación se actualizará la base de datos automáticamente)
  1. Conceder acceso público a la base de datos de RDS
  2. Crear en el sistema operativo una variable de entorno con los datos de conexión de la base de datos de RDS
  ```bash
setx ConnectionStrings__BankDatabase "server=HOST_BASEDEDATOS_RDS;port=PUERTO_BASEDATOS_RDS;user=USUARIO_BASEDATOS_RDS;password=PASSWORD_BASEDATOS_RDS;database=NOMBRE_BASEDATOS_RDS" /M
  ```
  De esta manera también se puede testear la aplicación local con la base de datos remota
  3. Actualizar la base de datos desde Visual Studio o mediante el comando `dotnet ef database update`. Al existir la variable de entorno se actualizará la base de datos remota.

### AWS Lambda

### Azure Functions

## Pasos seguidos para la creación de la aplicación (dotnet CLI)

```bash

mkdir api-bank-app

cd api-bank-app

dotnet new sln

Creación del proyecto
dotnet new webapi -o api-bank-app

Añadir proyecto a solución
dotnet sln api-bank-app.sln add api-bank-app/api-bank-app.csproj


Creación de archivo .gitignore

Creación de repositorio Git
git init

Añadir paquete MySQL
dotnet add package Pomelo.EntityFrameworkCore.MySql

Cadena de conexión en archivo appsettings.json

Creación del contexto de DB con MySQL

Creación de un modelo (cliente)

Crear migración de DB
dotnet ef migrations add InitialCreate

Actualizar BD
dotnet ef database update

Añadir paquete de Generación de Código
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

Crear controlador para modelo de clientes
dotnet aspnet-codegenerator controller -p api-bank-app.csproj -api -m Client -name ClientsController -outDir Controllers/ -dc BankContext


```
