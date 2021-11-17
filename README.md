# `Tienda-Zapatilals`
Tienda Zapatillas - Proyecto para la materia Nuevas Tecnologias 1 - ORT

 Importante: Para poder cargar la DB (sqlite)

En appsettings.json
Configurar la ruta propia a la base de datos (eshop.db)

"DefaultConnection": "filename=/User/dir1/dir2/.../Data/eshop.db"

Ejemplo:

 ```
 {
  "ConnectionStrings": {
    "DefaultConnection": "filename=/User/dir1/dir2/.../Data/eshop.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```
