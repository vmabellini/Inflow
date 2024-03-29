﻿using Inflow.Shared.Abstractions.Modules;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Modules
{
    public static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            const string MODULE_PART = "Inflow.Modules.";

            //load all assemblies in the current domain
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            //list all directories of the existing assemblies not dynamically generated
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();

            //list all dll files in the base dir which aren't loaded into the current domain
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            var disabledModules = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(MODULE_PART))
                {
                    continue;
                }

                var moduleName = file.Split(MODULE_PART)[1].Split(".")[0].ToLowerInvariant();
                var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
                if (!enabled)
                {
                    disabledModules.Add(file);
                }
            }

            foreach (var disabledModule in disabledModules)
            {
                files.Remove(disabledModule);
            }

            //loads all scanned assemblies
            files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
            => assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .OrderBy(x => x.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>()
                .ToList();
    }
}
