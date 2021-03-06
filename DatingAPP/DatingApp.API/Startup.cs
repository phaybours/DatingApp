using BlowFishCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using DatingApp.API.Helpers;
using AutoMapper;

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private BlowFish blowFish = new BlowFish("@dm1n1z7r@t0r!!!");

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDbContext<DataContext>(x=>x.UseSqlServer(Configuration.GetConnectionString("TempCon")));
            services.AddCors();// Make sure you call this previous to AddMvc
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<Seed>();
            services.AddDbContext<DataContext>(x => x.UseSqlServer(GetConnectionString()));
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IDatingRepository, DatingRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("Appsettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        string GetConnectionString()
        {
            string isEncrypted = Configuration.GetValue<string>("ConnectionStrings:IsEncD");
            string servername = Configuration.GetValue<string>("ConnectionStrings:Server");
            string dbName = Configuration.GetValue<string>("ConnectionStrings:DBName");
            string username = Configuration.GetValue<string>("ConnectionStrings:Username");
            string password = Configuration.GetValue<string>("ConnectionStrings:Password");

            if (isEncrypted == "Y")
            {
                // Call Method to Decrypt Details
                servername = blowFish.Decrypt_CBC(servername);
                dbName = blowFish.Decrypt_CBC(dbName);
                username = blowFish.Decrypt_CBC(username);
                password = blowFish.Decrypt_CBC(password);
            }
            // Create Instance of ConnectionString Builder Class
            // SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            // Specify the provider name, server,database, username and password. Set the properties for the data source.
            // sqlBuilder.DataSource =servername;
            // sqlBuilder.InitialCatalog = dbName;
            // sqlBuilder.UserID = username;
            // sqlBuilder.Password = password;    
            string constr = $"Data Source={servername};Initial Catalog={dbName};User Id={username};Password={password};Trusted_Connection=False;MultipleActiveResultSets=true;";

            // return sqlBuilder.ConnectionString;
            return constr;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            // app.UseHttpsRedirection();
            // seeder.SeedUsers();
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
