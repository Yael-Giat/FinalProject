using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using DAL.Interfaces;
using DAL.Data;
using MODELS;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add MongoDB configuration from appsettings.json
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("MongoDB");
            var databaseName = configuration.GetValue<string>("DatabaseName");

            // Configure MongoDB client and database
            var mongoClient = new MongoClient(connectionString);

            // Register MongoDB services
            builder.Services.AddSingleton<IMongoClient>(mongoClient);
            builder.Services.AddSingleton<IMongoDatabase>(mongoClient.GetDatabase(databaseName));

            // Register UserData as IUserInterface implementation
            builder.Services.AddSingleton<IUserInterface, UserData>();

            builder.Services.AddControllers();

            // Add Swagger configuration
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
