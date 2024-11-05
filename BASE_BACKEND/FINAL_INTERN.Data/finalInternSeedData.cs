using FINAL_INTERN.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Data
{
    public class finalInternSeedData
    {
        public static async Task SeedData(finalInternDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new List<Role>
        {
            new Role { Id = 1, NameOfRole = "Admin" },
            new Role { Id = 2, NameOfRole = "Customer" }
        });
                await context.SaveChangesAsync();
            }

            if (!context.Accounts.Any())
            {
                context.Accounts.AddRange(new List<Account>
        {
            new Account { Id = 1, Email = "admin@example.com", Username = "admin1", Password = "password1", FirstName = "Admin", LastName = "User", RoleId = 1, IsActive = true },
            new Account { Id = 2, Email = "customer1@example.com", Username = "customer1", Password = "password2", FirstName = "John", LastName = "Doe", RoleId = 2, IsActive = true },
            new Account { Id = 3, Email = "customer2@example.com", Username = "customer2", Password = "password3", FirstName = "Jane", LastName = "Smith", RoleId = 2, IsActive = true },
            new Account { Id = 4, Email = "customer3@example.com", Username = "customer3", Password = "password4", FirstName = "Michael", LastName = "Johnson", RoleId = 2, IsActive = true },
            new Account { Id = 5, Email = "customer4@example.com", Username = "customer4", Password = "password5", FirstName = "Emily", LastName = "Williams", RoleId = 2, IsActive = true }
        });
                await context.SaveChangesAsync();
            }

            if (!context.CategoriesOfCars.Any())
            {
                context.CategoriesOfCars.AddRange(new List<CategoriesOfCar>
        {
            new CategoriesOfCar { Id = 1, CategoryName = "SUV" },
            new CategoriesOfCar { Id = 2, CategoryName = "Sedan" },
            new CategoriesOfCar { Id = 3, CategoryName = "Truck" },
            new CategoriesOfCar { Id = 4, CategoryName = "Convertible" },
            new CategoriesOfCar { Id = 5, CategoryName = "Hatchback" }
        });
                await context.SaveChangesAsync();
            }

            if (!context.CarInfos.Any())
            {
                context.CarInfos.AddRange(new List<CarInfo>
        {
            new CarInfo { Id = 1, Model = "Toyota Corolla", Years = new DateTime(2020, 1, 1), Price = 20000, Transmission = "Automatic", FuelType = "Petrol", CategoriesOfCarId = 2, StockQuantit = 10, IsActive = true },
            new CarInfo { Id = 2, Model = "Ford Ranger", Years = new DateTime(2019, 1, 1), Price = 30000, Transmission = "Manual", FuelType = "Diesel", CategoriesOfCarId = 3, StockQuantit = 7, IsActive = true },
            new CarInfo { Id = 3, Model = "Honda Civic", Years = new DateTime(2021, 1, 1), Price = 25000, Transmission = "Automatic", FuelType = "Petrol", CategoriesOfCarId = 2, StockQuantit = 8, IsActive = true },
            new CarInfo { Id = 4, Model = "Chevrolet Camaro", Years = new DateTime(2022, 1, 1), Price = 40000, Transmission = "Automatic", FuelType = "Petrol", CategoriesOfCarId = 4, StockQuantit = 5, IsActive = true },
            new CarInfo { Id = 5, Model = "Nissan Altima", Years = new DateTime(2018, 1, 1), Price = 23000, Transmission = "Manual", FuelType = "Petrol", CategoriesOfCarId = 2, StockQuantit = 9, IsActive = true }
        });
                await context.SaveChangesAsync();
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(new List<Order>
        {
            new Order { Id = 1, AccountId= 1, NameOfCustomer = "John Doe", Date = "2024-01-10", Email = "customer1@example.com", Status = 1 },
            new Order { Id = 2, AccountId= 2, NameOfCustomer = "Jane Smith", Date = "2024-01-15", Email = "customer2@example.com", Status = 2 },
            new Order { Id = 3, AccountId= 3, NameOfCustomer = "Michael Johnson", Date = "2024-01-20", Email = "customer3@example.com", Status = 1 },
            new Order { Id = 4, AccountId= 4, NameOfCustomer = "Emily Williams", Date = "2024-01-25", Email = "customer4@example.com", Status = 3 },
            new Order { Id = 5, AccountId= 5, NameOfCustomer = "Admin User", Date = "2024-01-30", Email = "admin@example.com", Status = 1 }
        });
                await context.SaveChangesAsync();
            }

            if (!context.OrderDetails.Any())
            {
                context.OrderDetails.AddRange(new List<OrderDetail>
        {
            new OrderDetail { Id = 1, OrderId = 1, AccountId= 1, CarId = 1, NameOfCar = "Toyota Corolla", Price = 20000, Total = 20000 },
            new OrderDetail { Id = 2, OrderId = 2, AccountId= 2, CarId = 2, NameOfCar = "Ford Ranger", Price = 30000, Total = 30000 },
            new OrderDetail { Id = 3, OrderId = 3, AccountId= 3, CarId = 3, NameOfCar = "Honda Civic", Price = 25000, Total = 25000 },
            new OrderDetail { Id = 4, OrderId = 4, AccountId= 4, CarId = 4, NameOfCar = "Chevrolet Camaro", Price = 40000, Total = 40000 },
            new OrderDetail { Id = 5, OrderId = 5, AccountId= 5, CarId = 5, NameOfCar = "Nissan Altima", Price = 23000, Total = 23000 }
        });
                await context.SaveChangesAsync();
            }
        }
    }
}
