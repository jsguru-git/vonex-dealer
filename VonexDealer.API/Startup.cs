using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VonexDealer.API.Data;
using VonexDealer.API.Services;
using VonexDealer.API.Repository;
using VonexDealer.API.Repository.OMS;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using VonexDealer.API.Repository.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using VonexDealer.API.Extensions;
using System.Collections.Generic;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using VonexDealer.API.Repository.Supporting;

namespace VonexDealer.API
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));


            services.AddCors(options =>
            {
                options.AddPolicy("Vonex",
                      builder =>
                      {
                          builder
                          .AllowAnyOrigin() //.WithOrigins("https://localhost:44309", "https://signonglass.vonex.com.au")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();

                      });
            });
            services.AddOptions();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })

            //.AddCookie()
            // .AddOpenIdConnect("Auth0", options =>
            // {
            //     // Set the authority to your Auth0 domain
            //     options.Authority = $"https://{Configuration["Auth0:Domain"]}";

            //     // Configure the Auth0 Client ID and Client Secret
            //     options.ClientId = Configuration["Auth0:ClientId"];
            //     options.ClientSecret = Configuration["Auth0:ClientSecret"];

            //     // Set response type to code
            //     options.ResponseType = "code";

            //     // Configure the scope
            //     options.Scope.Clear();
            //     options.Scope.Add("openid");

            //     // Set the callback path, so Auth0 will call back to http://localhost:5000/signin-auth0 
            //     // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
            //     options.CallbackPath = new PathString("/signin-auth0");

            //     // Configure the Claims Issuer to be Auth0
            //     options.ClaimsIssuer = "Auth0";

            //     options.Events = new OpenIdConnectEvents
            //     {
            //         // handle the logout redirection 
            //         OnRedirectToIdentityProviderForSignOut = (context) =>
            //         {
            //             var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

            //             var postLogoutUri = context.Properties.RedirectUri;
            //             if (!string.IsNullOrEmpty(postLogoutUri))
            //             {
            //                 if (postLogoutUri.StartsWith("/"))
            //                 {
            //                     // transform to absolute
            //                     var request = context.Request;
            //                     postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
            //                 }
            //                 logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
            //             }

            //             context.Response.Redirect(logoutUri);
            //             context.HandleResponse();

            //             return Task.CompletedTask;
            //         }
            //     };
            // })

            .AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth0:Domain"];
                options.Audience = Configuration["Auth0:ClientId"];
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", Configuration["Auth0:Domain"])));
                options.AddPolicy("edit:dealers", policy => policy.Requirements.Add(new HasScopeRequirement("edit:dealers", Configuration["Auth0:Domain"])));
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<ITCRepository, TCRepository>();
            services.AddScoped<IOMS, OMSRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUtilibill, UtilibillRepository>();
            services.AddScoped<ILandline, LandlineRepository>();
            services.AddScoped<ISupport, Supporting>();

            CustomAssemblyLoadContext ctx = new CustomAssemblyLoadContext();
            string path = Directory.GetCurrentDirectory();
            ctx.LoadUnmanagedLibrary(path + "\\lib\\wkhtmltox\\64 bit\\libwkhtmltox.dll");

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddMvc()
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.AllowAnonymousToFolder("/Account");
            });

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new Info { Title = "VonexDealer.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"

                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            services.ConfigureSwaggerGen(options =>
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Set the comments path for the swagger json and ui.

                options.IncludeXmlComments(basePath + "\\VonexDealer.API.xml");

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.ServiceProvider.GetService<ApplicationDbContext>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().EnsureSeeded();
                }
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().EnsureSeeded();

            }

            if (env.IsDevelopment())
            {

                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseCors("Vonex");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VonexDealer.API V1");

            });



        }
    }
}
