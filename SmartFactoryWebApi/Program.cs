
using DotNetEnv;

namespace SmartFactoryWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowEverything", policy =>
                {
                    policy
                        .SetIsOriginAllowed(x => _ = true) // Allow all origins
                        .AllowAnyMethod()  // Allow all methods (GET, POST, etc.)
                        .AllowAnyHeader()  // Allow all headers
                        .AllowCredentials();  // Allow credentials (cookies, headers, etc.)
                });
            });

            Env.Load();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowEverything");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
