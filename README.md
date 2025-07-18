# 🕹️ Game Keys Store API

This is a personal project developed for learning purposes. It's a **RESTful API** that simulates the backend of a digital game key e-commerce platform. Users can register, log in, browse available game keys, and simulate purchases.

---

## 🚀 Technologies Used

- **C#**
- **.NET 8**
- **Entity Framework Core**
- **ASP.NET Identity**
- **MySQL**
- **AutoMapper**

---

## 📦 Features

- 🔐 **User Authentication and Authorization** (login, registration via ASP.NET Identity)
- 🛒 **CRUD operations** for:
  - Users
  - Games
  - Game Keys
  - Orders (Pedidos)
- 🎮 Keys are assigned uniquely per purchase
- 🔎 Filtering and pagination support
- 🔄 DTO structure for better separation of concerns
- 🗄️ MySQL database integration with Entity Framework Migrations

---

## 🧪 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/LucasRaphaelM/game-store-api-restful.git

2. Create a MySQL database and update your appsettings.json with your connection string.

3. Apply migrations:<br>
   dotnet ef database update

## 🧑‍💻 Author
Made by Lucas Rahpael.<br>
This is a learning project, feel free to fork and experiment!
