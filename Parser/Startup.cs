using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parser.Infrastructure.Sql;
using Parser.Instagram;
using Parser.Instagram.Interfaces;
using Parser.Middleware;

namespace Parser
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ParserDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=Pasrser;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

            services.AddMvc();
                
            services.AddSingleton<IInstagramService>(factory => new InstagramService(Infrastructure.Builders.InstaApiBuilder.Build()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionMiddleware();
            }
            else
            {
                app.UseExceptionMiddleware();
                app.UseExceptionHandler();
            }

            app.UseMvc();
        }
    }
}
