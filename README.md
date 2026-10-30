# API de gestión de productos

Web API RESTful en ASP.NET Core con arquitectura en tres capas:

- **Controllers:** reciben las solicitudes HTTP y devuelven respuestas `IActionResult`.
- **Services:** aplican la lógica de negocio y convierten entidades a DTOs.
- **Repositories:** gestionan los productos en memoria.

## Requisitos

- .NET 10 SDK

## Ejecutar

Desde la carpeta del proyecto:

```powershell
dotnet run
```

El perfil `http` ejecuta la API en `http://localhost:5000` y abre Swagger UI en `http://localhost:5000/swagger`. Desde esa interfaz se pueden consultar y probar todos los endpoints con **Try it out**. La especificación OpenAPI que utiliza Swagger UI está disponible en `http://localhost:5000/openapi/v1.json`.

Swagger UI y el documento OpenAPI se habilitan en el entorno `Development`, que es el entorno configurado por el perfil local `http`. Al clonar el proyecto, ejecuta `dotnet run` desde la carpeta que contiene `ProductosApi.csproj`.

## Endpoints

| Método | Ruta | Respuesta |
|---|---|---|
| GET | `/api/productos` | `200 OK` |
| GET | `/api/productos/{id}` | `200 OK` o `404 Not Found` |
| POST | `/api/productos` | `201 Created` o `400 Bad Request` |
| PUT | `/api/productos/{id}` | `200 OK`, `404 Not Found` o `400 Bad Request` |
| DELETE | `/api/productos/{id}` | `204 No Content` o `404 Not Found` |

El cuerpo de POST y PUT usa `CreateProductoDto`:

```json
{
  "nombre": "Teclado",
  "precio": 15000,
  "categoria": "Periféricos"
}
```

El nombre debe tener entre 3 y 100 caracteres y el precio debe ser mayor que cero. Los DTOs de respuesta incluyen el ID y el estado `activo`; los controladores no exponen la entidad de dominio.

Los datos se guardan en memoria y se pierden al detener la aplicación. El repositorio conserva una lista compartida para que los productos permanezcan disponibles entre solicitudes HTTP, aunque su ciclo de vida sea `Scoped`.
