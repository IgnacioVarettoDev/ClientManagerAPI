# Release Notes - v0.0.0

## Novedades
- **Lanzamiento Inicial:** Este es el primer lanzamiento del proyecto. ¡Bienvenido!

## Caracteristicas
- **Conexion a una BD SQLite:** Se conecta a la BD ClientManager, generada en el mismo proyecto mediante EntityFramework.
- **API para manejar clientes:** Disponible el CRUD completo clientes, CREATE, READ, DELETE y UPDATE, LIST ALL CLIENTs.
- **Validadores:** Los campos del cliente tiene validadores y requerimientos para ser ingresados.
- **Guid:** El ID es Guid unico, y no es autoincrementable.

## Mejoras
- **Modularidad:** Mejorar la estructura del proyecto.
- **Validador RUT:** Crear la validacion para el RUT del Cliente.
- **Control de Logs:** Crear un sistema para loguear las acciones dentro de la API.
- **Excepciones:** Manejo de excepciones en la API.
- **Mapping:**  Realizar mapeo entre modelo de la BD y de la API, esto para evitar exponer nombres de la BD en los response.
