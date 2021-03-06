using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MilkService.API.Controllers.Config;
using MilkService.API.Domain.Models.Queries.Response.User;
using MilkService.API.Domain.Repositories;
using MilkService.API.Domain.Services;
using MilkService.API.Extensions;
using MilkService.API.Persistence.Contexts;
using MilkService.API.Persistence.Repositories;
using MilkService.API.Services;
using Newtonsoft.Json;

namespace MilkService.API
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
            services.AddMemoryCache();
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });
            services.AddCustomApiVersioning();
            services.AddDbContext<MilkServiceContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("default"), x => x.ServerVersion("8.0.19-mysql"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDetails, UserDetails>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseUserDetailsMiddleware();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}



//services.AddControllers().AddNewtonsoftJson(options =>
//{
//    //options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
//    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
//});
//    services.AddMvc()
//.AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = false; });
//services.AddMvc().AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
//    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
//});
//services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson();