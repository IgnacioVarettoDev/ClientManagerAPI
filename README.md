# ClientManagerAPI
# API de Gestión de Clientes

Este proyecto es una API de gestión de clientes desarrollada en .NET. Permite realizar operaciones básicas de creación, actualización y obtención de datos de clientes. La API está diseñada siguiendo buenas prácticas de la industria y utiliza una base de datos SQL Lite para el almacenamiento de datos.

## Tecnologías Utilizadas
- **AutoMapper**: Utilizamos AutoMapper para mapear los objetos DTO (Data Transfer Objects) a las entidades del modelo de datos y viceversa.

- **Entity Framework SQLite**: Sistema de gestión de bases de datos.

- **Entity Framework Tools**: Para crear y administrar la base de datos SQLite que respalda esta API. Estas herramientas facilitan la generación y gestión de esquemas de base de datos a través de migraciones y scripts.


## Instalación y Ejecución

Para ejecutar este proyecto en tu entorno local, sigue los siguientes pasos:

1. Clona este repositorio en tu máquina local:

git clone https://github.com/IgnacioVarettoDev/ClientManagerAPI

2. Abre el proyecto en tu IDE de desarrollo favorito, como Visual Studio o Visual Studio Code.

3. Asegúrate de que tengas instalado .NET Core en tu sistema.

4. En el archivo de configuración `appsettings.json`, se encuentra la cadena de conexión a la base de datos SQL Lite.

Esto iniciará la API en tu máquina local.

| Tareas               | Estado    |
| -------------------- | --------- |
| Uso de SQLite        | &#9745;    |
| Crear Tabla Client   | &#9745;    |
| Generar endpoints    |   &#9745;         |
| - `POST /api/client` | &#9745;    |
| - `PUT /api/client/{id}` | &#9745; |
| - `GET /api/client`  | &#9745;    |
| - `DELETE /api/client/{id}` | &#9745;  |
| - `GET /api/client/{id}` | &#9745; |
| Manejo de excepciones |   &#9745;        |
| Control de logs      |    &#9745;       |
| Compatible con Postman | &#9745;   |
