using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using PS.Utils;
using System.Threading.Tasks;
using System;

namespace PS
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
            services.AddControllers();
            ConfigurationHelper.Initialize(Configuration);
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //configure jwt authentication
            //var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var key = "Precipio solutions is my secret passkey. It should be kept secretly.";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
                };
                //x.TokenValidationParameters = new TokenValidationParameters
                //{
                //    ValidateIssuerSigningKey = true,
                //    IssuerSigningKey = new SymmetricSecurityKey(key),
                //    ValidateIssuer = false,
                //    ValidateAudience = false
                //};
            });

            services.AddSingleton<IAuth>(new Auth(key));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PS", Version = "v1" });
                //c.AddSecurityDefinition("Bearer",
                //new ApiKeyScheme
                //{
                //    In = "header",
                //    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                //    Name = "Authorization",
                //    Type = "apiKey"
                //});
                //c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                //    { "Bearer", Enumerable.Empty<string>() },
                //});
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
            services.AddDbContext<GeneralContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DevConnection")));
            services.AddCors();
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            //app.UseCors(options =>
            //options.WithOrigins("http://localhost:4200/", "http://ps.etoups.in", "http://ps-dev.ap-south-1.elasticbeanstalk.com/")
            //.AllowAnyMethod()
            //.AllowAnyHeader());

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            //lifetime.ApplicationStarted.Register(OnApplicationStartedAsync(env).Wait);

            if (env.IsDevelopment())  
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PS v1"));
            }

            app.UseHttpsRedirection();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //                Path.Combine(env.ContentRootPath, "Image")),
            //    RequestPath = new PathString("/image")
            //});

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("started !");
            });
        }
    }
}
