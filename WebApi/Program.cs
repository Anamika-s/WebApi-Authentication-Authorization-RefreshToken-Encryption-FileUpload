using ChustaSoft.Tools.SecureConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.IRepo;
using WebApi.Policy;
using WebApi.Repository;
using WebApi.Repository.Context;
using WebApi.Security;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.

            //for Data protection
            builder.Services.AddDataProtection();
            //builder.Services.EncryptSettings<AppSettings>(true);
            builder.Services.SetUpSecureConfig<AppSettings>(builder.Configuration, builder.Configuration["SECRET_KEY"]);
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<StudentDapperRepo>();
            //builder.Services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();    
            builder.Services.AddScoped<IAuthenticationManager, AuthenticateManager>();
            //builder.Services.AddScoped<IAuthenticationManager1, AuthenticateManager1>();
            //builder.Configure(builder.Configuration.GetSection("MySection"));
            // Register that class
            builder.Services.AddSingleton<DataProtectionSecuritySettings>();
           string connectionString = builder.Configuration.GetSection("cn").Value;
            string str = builder.Configuration.GetSection("Connection1:str1").Value;

             //builder.Services.AddDbContext<StudentDbContext>();
          

            string str1 = builder.Configuration.GetSection("cn:cn1").Value;
            builder.Services.AddDbContext<StudentDbContext>
              (options => options.UseSqlServer(str1));
            builder.Services.AddControllers();

            // Add Jwt Settings

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters

               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateIssuerSigningKey = true,
                   //ValidIssuer = builder.Configuration["Jwt:Issuer"],
                   //ValidAudience = builder.Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
               };
           });

            // Add Policy
            //builder.Services.AddAuthorization();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAndManager", policy =>
                {
                    policy.RequireRole("Admin", "Manager");
                });
                options.AddPolicy("EmployeeWIthMoreThan20YearsExperience", policy =>
                {
                    policy.Requirements.Add(new EmployeeWIthMoreThan20YearsExperience(20));
                });
            });

            builder.Services.AddSingleton<IAuthorizationHandler, EmployeeWithMoreThan20YearsHandler>();
            builder.Services.AddSingleton<EmployeeNumberofYears>();


            //builder.Services.AddSingleton<ITokenRefresher>(x =>  new TokenRefresher(builder.Configuration, x.GetService<IAuthenticationManager1>()));
            //builder.Services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>(); 

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
           app.UseAuthorization();

            

            


            app.MapControllers();

            app.Run();
        }
    }
}