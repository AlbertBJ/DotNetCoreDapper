using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DotNetCore.Entities;
using DotNetCore.Repository.Interfaces;
using DotNetCore.Repository;

namespace DotNetCoreDapper
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

            services.AddOptions();

            services.Configure<Option>(Configuration.GetSection("Option"));

            #region 注册
            //全局注册
            foreach (var itemClass in GetClassInterfacePairs("DotNetCore.Business"))
            {
                foreach (var itemInterface in itemClass.Value)
                {
                    services.AddTransient(itemInterface, itemClass.Key);
                }
            }

            foreach (var itemClass in GetClassInterfacePairs("DotNetCore.Repository"))
            {
                foreach (var itemInterface in itemClass.Value)
                {
                    services.AddTransient(itemInterface, itemClass.Key);
                }
            }
            //注册泛型
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));    
            #endregion
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        #region 辅助类
        private Dictionary<Type, List<Type>> GetClassInterfacePairs(string assemblyName)
        {
            //存储 实现类 以及 对应接口 
            Dictionary<Type, List<Type>> dic = new Dictionary<Type, List<Type>>();
            Assembly assembly = GetAssembly(assemblyName);
            Type[] types = assembly.GetTypes();
            foreach (var item in types.AsEnumerable().Where(x => !x.IsAbstract && !x.IsInterface && !x.IsGenericType))
            {
                dic.Add(item, item.GetInterfaces().Where(x => !x.IsGenericType).ToList());
            }
            return dic;
        }

        private List<Assembly> GetAllAssemblies()
        {
            var list = new List<Assembly>();
            var deps = DependencyContext.Default;
            var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");//排除所有的系统程序集、Nuget下载包
            foreach (var lib in libs)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(assembly);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return list;
        }


        private Assembly GetAssembly(string assemblyName)
        {
            return GetAllAssemblies().FirstOrDefault(assembly => assembly.FullName.Contains(assemblyName));
        }
        #endregion

    }
}
