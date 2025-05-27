# EstudiantesWebApp

Este repositorio contiene la solución completa para la gestión de estudiantes, profesores y materias, compuesta por dos proyectos principales: el backend ([EstudiantesWebAppBackend](EstudiantesWebApp/EstudiantesWebAppBackend/)) y el frontend ([EstudiantesWebAppFrontend](EstudiantesWebApp/EstudiantesWebAppFrontend/)).

---

## Conceptos y Arquitectura

### 1. **Arquitectura en Capas**
El backend sigue una arquitectura en capas, separando la lógica de negocio (BLL), acceso a datos (Data), modelos (Models) y utilidades (Utilities). Esto facilita el mantenimiento, escalabilidad y pruebas.

### 2. **API RESTful**
El backend expone una API RESTful en .NET para gestionar operaciones CRUD sobre estudiantes, profesores y materias. Utiliza controladores para manejar las rutas y servicios para la lógica de negocio.

### 3. **Frontend SPA con Angular**
El frontend es una Single Page Application (SPA) desarrollada en Angular, que consume la API del backend para mostrar y gestionar la información de la plataforma educativa.

### 4. **Angular Material**
Se utiliza Angular Material en el frontend para una interfaz moderna y responsiva, con componentes como tablas, formularios, diálogos modales y paginación.

### 5. **Reactive Forms**
El frontend implementa formularios reactivos para validación y manejo eficiente de datos de entrada del usuario.

### 6. **Autenticación**
El sistema incluye autenticación de usuarios, gestionando sesiones y permisos desde el frontend y backend.

---

## Estructura de Carpetas

- [`EstudiantesWebAppBackend`](EstudiantesWebApp/EstudiantesWebAppBackend/): Solución backend en .NET.
  - `API/`: Controladores y configuración de la API.
  - `BLL/`: Lógica de negocio.
  - `Data/`: Acceso a datos y contexto de base de datos.
  - `Models/`: Definición de entidades y modelos de datos.
  - `Utilities/`: Funciones auxiliares y utilidades.
- [`EstudiantesWebAppFrontend`](EstudiantesWebApp/EstudiantesWebAppFrontend/): Aplicación frontend en Angular.
  - `src/app/teachers`: Gestión de profesores.
  - `src/app/class-subjects`: Gestión de materias.
  - `src/app/user`: Autenticación y gestión de usuarios.
  - `src/app/shared`: Componentes y servicios compartidos.
  - `src/app/material`: Módulo centralizado de Angular Material.

---

## Principales Librerías y Tecnologías

### Backend
- **.NET Core / ASP.NET Core**: Framework principal para la API.
- **Entity Framework Core**: ORM para acceso y gestión de base de datos.
- **AutoMapper**: Mapeo entre entidades y DTOs.
- **Swashbuckle/Swagger**: Documentación interactiva de la API.

### Frontend
- **Angular**: Framework SPA.
- **Angular Material**: Componentes UI.
- **RxJS**: Programación reactiva.
- **@angular/forms**: Formularios reactivos.
- **@angular/router**: Ruteo y navegación.
