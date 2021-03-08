using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BankAccount.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddDbContext<AccountDbContext>(options =>
            {
                options.UseInMemoryDatabase("BankAccount");
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<BankAccountModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();
            InitData(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InitData(IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
                db.Database.EnsureCreated();
                if (!db.UserEntities.Any())
                {
                    db.UserEntities.Add(new UserEntity()
                    {
                        FirstName = "Matt",
                        LastName = "He",
                        UserName = "MattHe",
                        AccountEntities = new List<AccountEntity>()
                        {
                            new AccountEntity()
                            {
                                AccountName = "Advantage",
                                CurrentBalance = 1000,
                            },
                            new AccountEntity()
                            {
                                AccountName = "Savings",
                                CurrentBalance = 1000,
                            }
                        }
                        
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
