using Autofac;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace BankAccount.API
{
    public class BankAccountModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var uowAssembly = Assembly.Load("BankAccount.UOW");
            var assembly = Assembly.Load("BankAccount.Repo");
            if (uowAssembly == null || assembly == null)
            {
                throw new ArgumentNullException();
            }
            builder.RegisterAssemblyTypes(uowAssembly)
                .Where(x => !x.IsInterface).AsImplementedInterfaces();

           
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => !x.IsAbstract)
                .AsImplementedInterfaces().PropertiesAutowired();
        }
    }
}
