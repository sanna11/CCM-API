using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCM.API.Configuration;
using CCM.Data.Models.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using CCM.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using CCM.Data.Repository;
using AutoMapper;
using CCM.Service.Mapping;
using CCM.Service.Interface;
using CCM.Service.Implementation;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CCM.Service.ViewModels;
using Microsoft.Extensions.FileProviders;
using CCM.Data.UOW;
using Microsoft.OpenApi.Models;

namespace CCM.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DBConfiguration>(Configuration.GetSection("ConnectionStrings"));

            services.AddDbContext<CCBDBContext>((provider, options) =>
            options.UseSqlServer(provider.GetRequiredService<IOptions<DBConfiguration>>().Value.DefaultConnectionString));
            services.Configure<BearerTokensOptions>(options => Configuration.GetSection("Bearer").Bind(options));

            services.AddIdentity<AuthUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<CCBDBContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                    o.TokenLifespan = TimeSpan.FromHours(12));

            RsaSecurityKey signingCredentials;
            RSA publicRsa = RSA.Create();
            String publicXml = Path.Combine(Directory.GetCurrentDirectory(), Configuration["Bearer:RSAPublicKey"]);
            var publicKeyXml = File.ReadAllText(publicXml);
            publicRsa.FromXmlString(publicKeyXml);
            signingCredentials = new RsaSecurityKey(publicRsa);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = CurrentEnvironment.IsProduction();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        RequireSignedTokens = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken,
                                     TokenValidationParameters validationParameters) =>
                        {
                            return notBefore <= DateTime.UtcNow &&
                                   expires >= DateTime.UtcNow;
                        },

                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Bearer:Issuer"],
                        ValidAudience = Configuration["Bearer:Audience"],
                        IssuerSigningKey = signingCredentials
                    };
                });

            services.AddHttpContextAccessor();

            services.AddScoped<IBaseRepository<Theatre>, BaseRepository<Theatre>>((provider) => 
            new BaseRepository<Theatre>(provider.GetService<CCBDBContext>().Set<Theatre>()));

            services.AddScoped<IBaseRepository<TheatreHall>, BaseRepository<TheatreHall>>((provider) =>
            new BaseRepository<TheatreHall>(provider.GetService<CCBDBContext>().Set<TheatreHall>()));

            services.AddScoped<IBaseRepository<Movie>, BaseRepository<Movie>>((provider) =>
            new BaseRepository<Movie>(provider.GetService<CCBDBContext>().Set<Movie>()));

            services.AddScoped<IBaseRepository<Actor>, BaseRepository<Actor>>((provider) =>
            new BaseRepository<Actor>(provider.GetService<CCBDBContext>().Set<Actor>()));

            services.AddScoped<IBaseRepository<MovieCast>, BaseRepository<MovieCast>>((provider) =>
            new BaseRepository<MovieCast>(provider.GetService<CCBDBContext>().Set<MovieCast>()));

            services.AddScoped<IBaseRepository<ContactPerson>, BaseRepository<ContactPerson>>((provider) =>
            new BaseRepository<ContactPerson>(provider.GetService<CCBDBContext>().Set<ContactPerson>()));

            services.AddScoped<IBaseRepository<TheatreSession>, BaseRepository<TheatreSession>>((provider) =>
            new BaseRepository<TheatreSession>(provider.GetService<CCBDBContext>().Set<TheatreSession>()));

            services.AddScoped<IBaseRepository<Ticket>, BaseRepository<Ticket>>((provider) =>
            new BaseRepository<Ticket>(provider.GetService<CCBDBContext>().Set<Ticket>()));

            services.AddScoped<IUnitOfWork, UnitOfWork>((provider) => new UnitOfWork(provider.GetService<CCBDBContext>()));

            services.AddSingleton((provider) => new MapperConfiguration(cfg => 
            cfg.AddProfile(new TransformationDataMappingProfile())).CreateMapper());

            services.AddScoped<IReportService, ReportService>();

            services.AddScoped<ITheatreService, TheatreService>();

            services.AddScoped<IMovieService, MovieService>();

            services.AddScoped<ITheatreSessionService, TheatreSessionService>();

            services.AddScoped<ITicketService, TicketService>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            var cachePeriod = env.IsDevelopment() ? "600" : "604800";

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                         Directory.GetCurrentDirectory()),
                OnPrepareResponse = ctx =>
                {
                    // Requires the following import:
                    // using Microsoft.AspNetCore.Http;
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //movieDataInitializer.Seed().Wait();
        }
    }
}
