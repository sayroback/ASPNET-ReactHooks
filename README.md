# .NET

# DotNet

## Comandos DotNet

Crear una nueva solución:

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

Agregar proyectos a la solución:

```csharp
dotnet sln add Proyecto/
```

Agregar referencias de un proyecto a otro. 

Debes posicionarte desde la terminal a la carpeta del proyecto al que agregaras las referencias:

```csharp
dotnet add reference ../Proyecto/
```