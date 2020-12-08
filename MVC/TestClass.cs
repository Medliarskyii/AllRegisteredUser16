using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCoreKino5.Models;
using WebCoreKino5.Controllers;
using Microsoft.AspNetCore.Http;
using System.IO;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace MyTest
{
    public class MyTest
    {
        private  PZ_KContext _context;
  
        void Connect()
        {

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
        
            builder.AddJsonFile("appsettings.json");
        
            var config = builder.Build();
        
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<PZ_KContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            _context = new PZ_KContext(options);
        
        }

        [Fact]
        public void UsersIndexViewResultNotNull()
        {

            Connect();

            // Arrange
            UsersController controller = new UsersController(_context);
            // Act
            Task<IActionResult> result = controller.Index() as Task<IActionResult>;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddUserReturnsARedirectAndAddsUser()
        {
            Connect();
            // Arrange

            UsersController controller = new UsersController(_context);
            var newUser = new User() { Name = "Bob", Surname = "Dilan", Mail = "BD@gmail.com", Password = "123", RoleId = 1 };

            // Act
            var result = controller.Create(newUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task UsersLogin()
        {
            Connect();
            var users = _context.Users;
            UsersController controller = new UsersController(_context);
            User user = new User();
            user.Mail = "jd@gmail.com";
            user.Password = "1234";
            user.Id = 1;
            // Act
            Task<IActionResult> result = controller.Login(user);

            Task<IActionResult> resultTest = controller.Details(1);
            // Assert
            Assert.Equal(result, resultTest);



        }
    }
}