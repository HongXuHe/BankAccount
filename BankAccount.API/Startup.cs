using Autofac;
using BankAccount.API.Middlewares;
using BankAccount.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;

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
            //use in memory db
            services.AddDbContext<AccountDbContext>(options =>
            {
                options.UseInMemoryDatabase("BankAccount");
            });

            //use auto mapper
            services.AddAutoMapper(typeof(Startup));
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
            else
            {
                //only use error handler when in release mode
                app.UseErrorHandlerMiddleware();
            }

            app.UseRouting();

            //app.UseAuthorization();
            app.UseFakeAuthMiddleware();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            InitData(app);
        }

        //give some initialize data to work with
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
