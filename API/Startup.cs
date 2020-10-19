using Application.Activities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistance;

namespace API
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
            services.AddDbContext<DataContext>(option => 
                option.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddCors(opt => {
                opt.AddPolicy("ReactClientCORS", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            services.AddCors(opt => {
                opt.AddPolicy("AngularClientCORS", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });

            services.AddMediatR(typeof(List.Handeler).Assembly);
            
            // Added to solve duplicate schema type issue for swagger.
            services.AddSwaggerGen(options => {
                options.CustomSchemaIds(type => type.ToString());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("ReactClientCORS");
            app.UseCors("AngularClientCORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
