using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;

namespace BankAccount.API
{
    public class BankAccountModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var uowAssembly = Assembly.Load("BankAccount.UOW");
            builder.RegisterAssemblyTypes(uowAssembly)
                .Where(x => !x.IsInterface).AsImplementedInterfaces();

            var assembly = Assembly.Load("BankAccount.Repo");
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => !x.IsAbstract)
                .AsImplementedInterfaces().PropertiesAutowired();
        }
    }
}
