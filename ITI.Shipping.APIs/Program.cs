using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Mapping;
using ITI.Shipping.Core.Application.Services;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using ITI.Shipping.Infrastructure.Presistence.Repositories;
using ITI.Shipping.Infrastructure.Presistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITI.Shipping.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add DB Connection
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("con"));
            });
            //builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped(typeof(IServiceManager),typeof(ServiceManager));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });
            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            #endregion

            var app = builder.Build();

            #region Configure MiddleWare

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                //Enable Swagger
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1")); 
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();


            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}
