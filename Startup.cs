using LibraryManagementSystem.BLL.ContractsBLL;
using LibraryManagementSystem.BLL.RepositoriesBLL;
using LibraryManagementSystem.DBAccessor;
using LibraryManagementSystem.DBAccessor.Contracts;
using LibraryManagementSystem.DBAccessor.Reposities;
using LibraryManagementSystem.Extensions;
using LibraryManagementSystem.Extensions.Logging_Request_Responses;
using LibraryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Extensions.JWTConfigure.AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryManagementSystem", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                      <br>Enter your <b>'Token'</b> in the text input below.<br>
                      Example: <i><b>'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9'</b></i><br>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                      },
                      new List<string>()
                    }
                });
            });
            //in memory db
            services.AddDbContext<LMSDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("InMemoryTestDB");
            });

            services.AddScoped<Extensions.JWTConfigure.IUserService, Extensions.JWTConfigure.UserService>();
            services.AddScoped<IBookBLL,BookBLL>();
            services.AddScoped<IBookProvider,BookProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryManagementSystem v1"));
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LMSDbContext>();
                AddTestData(context);
            }

            app.UseMiddleware<LoggingMiddleware>();//for logging the request and reponse 

            app.UseRouting();

            app.UseMiddleware<Extensions.JWTConfigure.JwtMiddleware>(); //for adding jwt 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private static void AddTestData(LMSDbContext context)
        {
            var testBook1 = new Book
            {
                Id = 1,
                Name = "Luke",
                PublisherName = "Skywalker",
                TotalSold = 0,
                SoldDate = DateTime.Now
            };
            var testBook2 = new Book
            {
                Id = 2,
                Name = "Luke2",
                PublisherName = "Skywalker2",
                TotalSold = 0,
                SoldDate = DateTime.Now
            };

            context.Books.Add(testBook1);
            context.Books.Add(testBook2);

            var testAuthor1 = new Author
            {
                Id = 1,
                Name = "test author Name",
                Address = "test address",
                BookId = testBook1
            };
            var testAuthor2 = new Author
            {
                Id = 2,
                Name = "test author Name2",
                Address = "test address2",
                BookId = testBook2
            };

            context.Authors.Add(testAuthor1);
            context.Authors.Add(testAuthor2);

            var testSale1 = new Sale
            {
                Id = 1,
                TotalBookSale = 0,
                BookId = 1,
                StoreId = 1
            };

            var bookStore1 = new BookStore
            {
                Id = 1,
                Name = "Wallstreet Journal",
            }; var bookStore2 = new BookStore
            {
                Id = 2,
                Name = "Wallstreet Journal2",
            };
            bookStore1.Book.Add(testBook1);
            bookStore1.Book.Add(testBook2);

            bookStore2.Sale.Add(testSale1);

            context.SaveChanges();
        }

    }
}
