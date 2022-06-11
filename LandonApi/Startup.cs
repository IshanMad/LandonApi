using LandonApi.Data;
using LandonApi.Filters;
using LandonApi.Infrastructure;
using LandonApi.Models;
using LandonApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi
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
            //add db context
            services.AddDbContext<HotelAPIDbContext>(options => options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")
            ));

            /* THIS LINE OF CODE DOES COUPLE OF THINGS
               IT PULLS THE PROPERTIES OUT OF THE INFO SECTION
               IN APPSETTINGS.JSON FILE,AND CREATE A NEW INSTANCE
               OF HOTELINFO WITH THOSE VALUES THEN IT WRAPS
               THAT INSTANCE IN AN INTERFACE CALLED ioptions INTERFACE
               INTO THE SERVICE CONTAINER WHIC MEANS IT CAN BE INJECTED INTO CONTROLLERS.
              */
              services.Configure<HotelInfo>(
                Configuration.GetSection("Info")
            );
            services.AddScoped<IRoomService, DefaultRoomService>();
           /* services.AddMvc(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<RequireHttpsOrCloseAttribute>();
            });*/

            //controllers
            services.AddControllers();
             services.AddControllers(options =>
             {
                 options.Filters.Add<JsonExceptionFilter>();
                 options.Filters.Add<RequireHttpsOrCloseAttribute>();
                 options.Filters.Add<LinkRewritingFilter>();
             });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LandonApi", Version = "v1" });
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            //add api version
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });
            /*
                “CORS” stands for Cross-Origin Resource Sharing. 
                It allows you to make requests from one website to another website in the browser, 
                which is normally prohibited by another browser policy called the Same-Origin Policy (SOP).
            */
            services.AddCors(options =>
            {
                /*
                    dp you use any police name and build policy also can be use browse request 
                    make origin by using policy=>policy.WithOrigins("https://example.com") method here 
                    testing purpose use
                    Alloany origin method.
                */
                options.AddPolicy("AllowMyApp",policy=>policy.AllowAnyOrigin());

            });
            services.AddAutoMapper(options => options.AddProfile<MappingProfile>());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LandonApi v1"));
            }
            else
            {
                //use hstc to secure api
                /*
                    Per OWASP, HTTP Strict Transport Security (HSTS) is an 
                    opt-in security enhancement that's specified by a
                    web app through the use of a response header. 
                    When a browser that supports HSTS receives this header: 
                    The browser stores configuration for the domain that prevents sending any communication
                    over HTTP. 
                */
                app.UseHsts();
            }

            app.UseHttpsRedirection();// get clinet request by http it redirect request to https
           
            app.UseRouting();
            app.UseCors("AllowMyApp");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMvc();
            
        }
    }
}
