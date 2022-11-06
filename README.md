# .NET

# DotNet

Los archivos que recibe la API se guardan en una carpeta "\WebAPI\Uploads\", es ignorada por Git, de debe crear manualmente.

## Comandos DotNet

Crear una nueva soluci�n:

```csharp
dotnet new sln
```

Crear una biblioteca de clases:

```csharp
 dotnet new classlib -o NombreBiblioteca
```

Crear templete WebApi ASP:

```csharp
dotnet new webapi -o NombreWebAPI
```

Agregar proyectos a la soluci�n:

```csharp
dotnet sln add Proyecto/
```

Agregar referencias de un proyecto a otro. 

Debes posicionarte desde la terminal a la carpeta del proyecto al que agregaras las referencias:

```csharp
dotnet add reference ../Proyecto/
```

Restaurar bibliotecas del proyecto.

```csharp
dotnet restore
```

Compilar el proyecto.

```csharp
dotnet build
```