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
            new Account { Email = "locnthe170659@fpt.edu.vn", Username = "hai", Password = "123", Gender=1, FirstName = "Admin", LastName = "User", Address="HaNoi" , RoleId = 1, IsActive = true },
            new Account { Email = "customer1@example.com", Username = "customer1", Password = "password2", Gender=1, FirstName = "John", LastName = "Doe", RoleId = 2, Address="HaNoi" , IsActive = true },
            new Account { Email = "customer2@example.com", Username = "customer2", Password = "password3", Gender=0, FirstName = "Jane", LastName = "Smith", RoleId = 2, Address="HaNoi" , IsActive = true },
            new Account { Email = "customer3@example.com", Username = "customer3", Password = "password4", Gender=0, FirstName = "Michael", LastName = "Johnson", RoleId = 2, Address="HaNoi" , IsActive = true },
            new Account { Email = "customer4@example.com", Username = "customer4", Password = "password5",  Gender=1,FirstName = "Emily", LastName = "Williams", RoleId = 2, Address="HaNoi" , IsActive = true }
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
            new CarInfo { Model = "Toyota Corolla", Years = new DateTime(2020, 1, 1), Price = 20000, Transmission = "Automatic", FuelType = "Petrol", CategoriesOfCarId = 2, StockQuantit = 10, IsActive = true },
            new CarInfo { Model = "Ford Ranger", Years = new DateTime(2019, 1, 1), Price = 30000, Transmission = "Manual", FuelType = "Diesel", CategoriesOfCarId = 3, StockQuantit = 7, IsActive = true },
            new CarInfo { Model = "Honda Civic", Years = new DateTime(2021, 1, 1), Price = 25000, Transmission = "Automatic", FuelType = "Petrol", CategoriesOfCarId = 2, StockQuantit = 8, IsActive = true },
            new CarInfo { Model = "Chevrolet Camaro", Years = new DateTime(2022, 1, 1), Price = 40000, Transmission = "Automatic", FuelType = "Petrol", CategoriesOfCarId = 4, StockQuantit = 5, IsActive = true },
            new CarInfo { Model = "Nissan Altima", Years = new DateTime(2018, 1, 1), Price = 23000, Transmission = "Manual", FuelType = "Petrol", CategoriesOfCarId = 2, StockQuantit = 9, IsActive = true }
        });
                await context.SaveChangesAsync();
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(new List<Order>
        {
            new Order { AccountId= 1, NameOfCustomer = "John Doe", Dates = DateTime.Parse("2024-01-10"), Email = "locnthe170659@fpt.edu.vn", Status = 2 },
            new Order { AccountId= 2, NameOfCustomer = "Jane Smith", Dates = DateTime.Parse("2024-01-15"), Email = "customer2@example.com", Status = 2 },
            new Order { AccountId= 3, NameOfCustomer = "Michael Johnson", Dates = DateTime.Parse("2024-01-20"), Email = "customer3@example.com", Status = 1 },
            new Order { AccountId= 4, NameOfCustomer = "Emily Williams", Dates = DateTime.Parse("2024-01-25"), Email = "customer4@example.com", Status = 3 },
            new Order { AccountId= 5, NameOfCustomer = "Admin User", Dates = DateTime.Parse("2024-01-30"), Email = "admin@example.com", Status = 1 }

        });
                await context.SaveChangesAsync();
            }

            if (!context.OrderDetails.Any())    
            {
                context.OrderDetails.AddRange(new List<OrderDetail>
        {
            new OrderDetail { OrderId = 1, AccountId= 1, CarId = 1, NameOfCar = "Toyota Corolla", Price = 20000, Total = 20000 },
            new OrderDetail { OrderId = 2, AccountId= 2, CarId = 2, NameOfCar = "Ford Ranger", Price = 30000, Total = 30000 },
            new OrderDetail { OrderId = 3, AccountId= 3, CarId = 3, NameOfCar = "Honda Civic", Price = 25000, Total = 25000 },
            new OrderDetail { OrderId = 4, AccountId= 4, CarId = 4, NameOfCar = "Chevrolet Camaro", Price = 40000, Total = 40000 },
            new OrderDetail { OrderId = 5, AccountId= 5, CarId = 5, NameOfCar = "Nissan Altima", Price = 23000, Total = 23000 }
        });
                await context.SaveChangesAsync();
            }
        }
    }
}
