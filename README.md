# PokeAPI Explorer Fullstack

Proyecto de portafolio construido con **ASP.NET Core Web API (.NET 10)** + **Angular 21**, consumiendo datos de **PokeAPI** desde el backend.

---

## ✨ Características principales

- 🔌 Backend desacoplado de PokeAPI
- ⚡ Caché en memoria para optimizar respuestas
- 📦 DTOs internos para una arquitectura limpia
- 🧩 Uso de interfaces y servicios
- 🛡️ Manejo centralizado de errores
- 📘 Documentación interactiva con Swagger
- 🖥️ Frontend Angular standalone
- 🔎 Búsqueda por nombre o ID
- ✅ Tests unitarios y base para tests de integración

## 🗂️ Estructura del proyecto

```text
pokemon-fullstack-portfolio/
  backend/
  frontend/
  IMPLEMENTATION_ORDER.md
  README.md
```

## 🧰 Versiones recomendadas

- .NET SDK: **10.0.x**
- Node.js: **20.19+**
- Angular CLI: **21.x**

## 🚀 Puesta en marcha

### Backend (Terminal 1)

```bash
cd backend
dotnet restore
dotnet build
dotnet test
dotnet run --project src/PokemonPortfolio.Api
```

También puedes ejecutar:

```bash
dotnet run --project ./src/PokemonPortfolio.Api/PokemonPortfolio.Api.csproj
```

### Rutas útiles del backend

- Swagger: `https://localhost:5001/swagger`
- Swagger: `http://localhost:5000/swagger`
- Health check: `https://localhost:5001/health`

### Frontend (Terminal 2)

```bash
cd frontend/pokemon-app
npm install
ng serve
```

### URL de la aplicación

- `http://localhost:4200`

## 📡 Endpoints disponibles

- `GET /api/pokemon/{nameOrId}`
- `GET /api/pokemon/{nameOrId}/basic`
- `GET /api/pokemon/{nameOrId}/evolution`
- `GET /api/pokemon/search?name=pika`
- `GET /health`

## 🧪 Evidencias de funcionamiento

Puedes validar que todo funciona correctamente con estas pruebas rápidas:

1. **Backend activo**  
   Abre Swagger en `https://localhost:5001/swagger` y verifica que carga la documentación.
   
   ![Evidencia de Swagger activo](docs/swagger.png)
2. **Consulta de Pokémon por ID o nombre**  
   Prueba `GET /api/pokemon/25` o `GET /api/pokemon/pikachu` y confirma que devuelve información.
3. **Frontend conectado al backend**  
   Abre `http://localhost:4200`, realiza una búsqueda y comprueba que muestra resultados.
   
   ![Evidencia del frontend conectado al backend](docs/Poke.png)
4. **Salud del servicio**  
   Consulta `GET /health` y valida una respuesta satisfactoria.

## 🤝 Contribución

¡Las contribuciones son bienvenidas!  
Si deseas colaborar:

1. Haz un fork del repositorio.
2. Crea una rama para tu cambio (`feature/nueva-funcionalidad`).
3. Realiza tus commits con mensajes claros.
4. Abre un Pull Request con la descripción de tu aporte.

## 📄 Licencia

Este proyecto se distribuye bajo la licencia **MIT**.  
Puedes ajustar esta sección si usas otra licencia.

## 👨‍💻 Autor

Desarrollado por **Juan Felipe España Arias** como parte de un proyecto de portafolio fullstack.

## ⭐ Nota final

Si te gusta este proyecto, no olvides darle una estrella en GitHub:

[![⭐ Dale una estrella en GitHub](https://img.shields.io/badge/%E2%AD%90-Dale_una_estrella_en_GitHub-yellow?style=for-the-badge)](https://github.com/)

