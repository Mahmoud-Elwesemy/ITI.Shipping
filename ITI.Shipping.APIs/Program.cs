using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Auth;
using ITI.Shipping.Core.Application.Abstraction.User;
using ITI.Shipping.Core.Application.Mapping;
using ITI.Shipping.Core.Application.Services;
using ITI.Shipping.Core.Application.Services.AuthServices;
using ITI.Shipping.Core.Application.Services.UserServices;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using ITI.Shipping.Infrastructure.Presistence.Repositories;
using ITI.Shipping.Infrastructure.Presistence.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ITI.Shipping.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddInfrastructure(builder.Configuration);

            #region Configure Services

            #region Public Services
            //// Add DB Connection
            //builder.Services.AddDbContext<ApplicationContext>(options =>
            //{
            //    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("con"));
            //});
            ////builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            //builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            //builder.Services.AddAutoMapper(typeof(MappingProfile));
            //builder.Services.AddScoped(typeof(IServiceManager),typeof(ServiceManager));
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins",
            //        policy =>
            //        {
            //            policy.AllowAnyOrigin()
            //                  .AllowAnyMethod()
            //                  .AllowAnyHeader();
            //        });
            //});
            //builder.Services.AddControllers();

            //builder.Services.AddOpenApi();

            #endregion


            #region Identity Configrations

           // builder.Services.AddScoped<IRoleService,RoleService>();
           // builder.Services.AddScoped<IUserService,UsersService>();
           // builder.Services.AddSingleton<IJWTProvider,JWTProvider>();
           // builder.Services.AddIdentity<ApplicationUser,ApplicationRole>()
           //.AddEntityFrameworkStores<ApplicationContext>();
           // builder.Services.AddTransient<IAuthorizationHandler,PermissionAuthorizationHandler>();
           // builder.Services.AddTransient<IAuthorizationPolicyProvider,PermissionAuthorizationPolicyProvider>();


           // builder.Services.AddAuthentication(options =>
           // {
           //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           // }).AddJwtBearer(options =>
           // {
           //     options.SaveToken = true;
           //     options.TokenValidationParameters = new()
           //     {
           //         ValidateIssuer = true,
           //         ValidateAudience = true,
           //         ValidateLifetime = true,
           //         ValidateIssuerSigningKey = true,
           //         ValidIssuer = "ShippingProject",
           //         ValidAudience = "ShippingProject users",
           //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lEQCxTrFYTOsyFtbtoWwPdDJ3066bWiP"))
           //     };
           // });
           // builder.Services.Configure<IdentityOptions>(options =>
           // {
           //     options.Password.RequiredLength = 8;
           //     //options.SignIn.RequireConfirmedEmail = true;
           //     options.User.RequireUniqueEmail = true;
           // });
            #endregion


            #region Swagger Configrations

            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen(options =>
            //{
            //    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,new OpenApiSecurityScheme
            //    {
            //        Name = "Authorization",
            //        Description = "Enter the Bearer authorization : 'Bearer Generate-JWT-Token'",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer"
            //    });

            //    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //         {
            //             new OpenApiSecurityScheme
            //             {
            //                 Reference = new OpenApiReference
            //                 {
            //                     Type = ReferenceType.SecurityScheme,
            //                     Id = JwtBearerDefaults.AuthenticationScheme
            //                 }
            //             },
            //             new string[] { }
            //         }
            //    });
            //});
            #endregion

            #endregion

            var app = builder.Build();

            #region Configure MiddleWare

            if(app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                //Enable Swagger
                //app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json","v1"));

                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(txt);
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}