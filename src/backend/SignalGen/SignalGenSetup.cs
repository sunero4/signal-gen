using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using SignalGen.Interfaces;
using SignalGen.Models;
using MethodInfo = SignalGen.Models.MethodInfo;

namespace SignalGen
{
    public static class SignalGenSetup
    {
        private static readonly ITypeMapper TypeMapper = new TypeMapper();
        private static readonly IHubManager HubManager = new HubManager(TypeMapper);

        public static IApplicationBuilder UseSignalGen(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var fullPath = Path.Combine(env.ContentRootPath, "/signalGen");

            Directory.CreateDirectory(fullPath);

            var hubClassesWithMethods = MapHubClassesAndMethods();
            
            var signalGenDiscoveryDoc = new SignalGenDiscoveryDoc
            {
                HubInfo = hubClassesWithMethods,
                Types = TypeMapper.CustomTypes
            };

            var json = JsonSerializer.Serialize(signalGenDiscoveryDoc);
            File.WriteAllText(Path.Combine(fullPath, "discovery.json"), json);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "signalGen")),
                RequestPath = "/signalGen"
            });

            return app;
        }

        private static List<HubInfo> MapHubClassesAndMethods()
        {
            var c = HubManager.GetHubClasses();
            return HubManager.GetHubClasses().Select(x => new HubInfo
            {
                Name = x.Name,
                Methods = x.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Select(m => new MethodInfo
                {
                    Name = m.Name,
                    Arguments = m.GetParameters().Select(p => HubManager.MapHubMethods(p.Name, p.ParameterType)).ToList()
                }).ToList()
            }).ToList();
        }
    }
}
