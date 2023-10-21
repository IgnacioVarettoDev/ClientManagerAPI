# ClientManagerAPI
# API de Gestión de Clientes

Este proyecto es una API de gestión de clientes desarrollada en .NET. Permite realizar operaciones básicas de creación, actualización y obtención de datos de clientes. La API está diseñada siguiendo buenas prácticas de la industria y utiliza una base de datos SQL Lite para el almacenamiento de datos.

## Instalación y Ejecución

Para ejecutar este proyecto en tu entorno local, sigue los siguientes pasos:

1. Clona este repositorio en tu máquina local:

git clone https://github.com/tu-usuario/client-management-api.git

2. Abre el proyecto en tu IDE de desarrollo favorito, como Visual Studio o Visual Studio Code.

3. Asegúrate de que tengas instalado .NET Core en tu sistema.

4. En el archivo de configuración `appsettings.json`, configura la cadena de conexión a tu base de datos SQL Lite.

5. Abre una terminal en el directorio del proyecto y ejecuta los siguientes comandos:

dotnet restore
dotnet build
dotnet run

Esto iniciará la API en tu máquina local.

| Tareas               | Estado    |
| -------------------- | --------- |
| Uso de SQLite        | &#9745;    |
| Crear Tabla Client   | &#9745;    |
| Generar endpoints    |           |
| - `POST /api/client` | &#9745;    |
| - `PUT /api/client/{id}` | &#9745; |
| - `GET /api/client`  | &#9745;    |
| - `DELETE /api/client/{id}` |      |
| - `GET /api/client/{id}` | &#9745; |
| Manejo de excepciones |          |
| Control de logs      |          |
| Compatible con Postman | &#9745;   |
