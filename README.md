# ☕ El Punto Rojo - Sistema de Punto de Venta

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-purple)](https://dotnet.microsoft.com/)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-blue)](https://www.mysql.com/)
[![JavaScript](https://img.shields.io/badge/JavaScript-ES6-yellow)](https://developer.mozilla.org/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

---
## 👨‍💻 Demostracion en YouTube
https://www.youtube.com/watch?v=cwacqWyOyCA


## 📋 Descripción del Proyecto

**El Punto Rojo** es un sistema de punto de venta (POS) desarrollado para la gestión integral de una cafetería. Permite controlar ventas, inventario y generar reportes en tiempo real, eliminando errores del control manual y optimizando la operación del negocio.

---

## 🎯 Problema que Soluciona

La cafetería realizaba sus procesos de forma manual:

* ❌ Errores en el cálculo de existencias
* ❌ Facturas a mano sin formato profesional
* ❌ Falta de información para tomar decisiones
* ❌ Tiempo perdido en inventarios manuales

---

## ✅ Solución Implementada

Sistema web con:

* ✔️ Interfaz intuitiva tipo caja registradora
* ✔️ Actualización automática del inventario
* ✔️ Generación de facturas imprimibles
* ✔️ Reportes de ventas y productos más vendidos
* ✔️ Gestión completa de productos (CRUD)

---

## 🚀 Tecnologías Utilizadas

| Capa              | Tecnología              | Versión |
| ----------------- | ----------------------- | ------- |
| **Backend**       | ASP.NET Core            | 8.0     |
| **Base de Datos** | MySQL / SQL Server      | 8.0     |
| **Frontend**      | HTML5, CSS3, JavaScript | ES6     |
| **ORM**           | Entity Framework Core   | 8.0     |
| **API**           | RESTful                 | -       |

---

## 📊 Diagrama Entidad-Relación

<p align="center">
<img width="562" height="2123" alt="mermaid-diagram" src="https://github.com/user-attachments/assets/edc3e6a3-6738-4a43-a7df-0a047e0046f1" /></p>

---

## 🎨 Diseño de la Aplicación

**Paleta de colores:**

* Rojo principal: `#D32F2F`
* Gris fondo: `#F5F5F5`
* Blanco: `#FFFFFF`
* Verde éxito: `#4CAF50`

---

## 📁 Estructura del Proyecto

```text
CafeteriaElPuntoRojo/
│
├── Controllers/
│   ├── CategoriasController.cs
│   ├── ProductosController.cs
│   ├── ReportesController.cs
│   ├── TestController.cs
│   └── VentasController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Models/
│   ├── Categoria.cs
│   ├── DetalleVenta.cs
│   ├── Producto.cs
│   ├── Venta.cs
│   │
│   └── DTOs/
│       ├── DetalleVentaDto.cs
│       └── VentaDto.cs
│
├── Migrations/
│   ├── 20240101000000_InitialCreate.cs
│   └── ApplicationDbContextModelSnapshot.cs
│
├── Properties/
│   └── launchSettings.json
│
├── wwwroot/
│   ├── index.html
│   ├── inventario.html
│   ├── reportes.html
│   │
│   ├── Assets/
│   │   └── logo.png
│   │
│   ├── css/
│   │   ├── style.css
│   │   └── inventario.css
│   │
│   └── js/
│       ├── ventas.js
│       ├── inventario.js
│       └── reportes.js
│
├── .gitignore
├── appsettings.json
├── appsettings.Development.json
├── CafeteriaElPuntoRojo.csproj
├── Program.cs
└── README.md
```

---

## 🔧 Instalación y Configuración

### Requisitos Previos

* .NET 8 SDK
* MySQL Server o SQL Server
* Git
* Navegador moderno

---

### Paso 1: Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/elpuntorojo-pos.git
cd elpuntorojo-pos
```

---

### Paso 2: Configurar la base de datos

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### Paso 3: Ejecutar la aplicación

```bash
dotnet run
```

Abrir en el navegador:

```
http://localhost:5045/
```

---

## 📡 Endpoints de la API

| Método | Endpoint                      | Descripción                 |
| ------ | ----------------------------- | --------------------------- |
| GET    | /api/productos                | Obtener todos los productos |
| GET    | /api/productos/{id}           | Obtener un producto         |
| POST   | /api/productos                | Crear nuevo producto        |
| PUT    | /api/productos/{id}           | Actualizar producto         |
| PATCH  | /api/productos/{id}/stock     | Actualizar stock            |
| DELETE | /api/productos/{id}           | Eliminar producto           |
| POST   | /api/ventas                   | Registrar venta             |
| GET    | /api/ventas                   | Obtener ventas              |
| GET    | /api/reportes/ventas-diarias  | Reporte diario              |
| GET    | /api/reportes/inventario-bajo | Productos con stock bajo    |

---

## 👨‍💻 Diseño 
<img width="1619" height="972" alt="62cbe7f5-620d-43b0-9968-778432091378" src="https://github.com/user-attachments/assets/4fbf27f4-2a0d-4053-b0e9-36ce4433ec2c" />


## 👨‍💻 Autor

**Axel Jiménez Martich**
Ingeniero de Software
📧 [axljimenez045@gmail.com](mailto:axljimenez045@gmail.com)

---

## 📄 Licencia

Este proyecto está bajo la licencia MIT.
