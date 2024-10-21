﻿using ac_insertdocs_inbase.Domain.Contracts;
using ac_insertdocs_inbase.Domain.Helpers;
using ac_insertdocs_inbase.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace ac_insertdocs_inbase.UnitTests
{
    public class ReaderDocsTests
    {
        private readonly ITestOutputHelper _output;

        public ReaderDocsTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private static void BuildRequiredObjects(out IReaderDocs reader)
        {
            var builder = new ConfigurationBuilder();
            UtToolkit.BuildConfig(builder);
            var config = builder.Build();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<ConfigurationValues>();
                    services.AddTransient<IReaderDocs, ReaderDocs>();
                })
                .Build();

            reader = ActivatorUtilities.CreateInstance<ReaderDocs>(host.Services);
        }

        [Fact]
        public void ShouldGetDocs()
        {
            BuildRequiredObjects(out IReaderDocs reader);

            string pathFolder = @"C:\Users\jose.fraile\Documents\appperson\pruebadocs";

            var data = reader.GetDocs(pathFolder);

            Assert.True(data.Success);

            _output.WriteLine("Contenido de la lista de archivos obtenidos");
            _output.WriteLine("-------------------------------------------");
            foreach (var item in data.Files)
            {
                _output.WriteLine($"Nombre: {item.Name}, Tamaño: {item.Length} bytes, Ruta: {item.FullName}");
            };
        }

        [Fact]
        public void ShouldReadDocs()
        {
            BuildRequiredObjects(out IReaderDocs reader);

            string pathFolder = @"C:\Users\jose.fraile\Documents\appperson\pruebadocs";

            var getDocs = reader.GetDocs(pathFolder);

            var data = reader.ReadDocs(getDocs.Files);

            Assert.True(data.Success);

            _output.WriteLine("Contenido de la lista de archivos obtenidos");
            _output.WriteLine("-------------------------------------------");
            foreach (var item in data.Docs)
            {
                _output.WriteLine($"Nombre: {item.NameFile}");
                _output.WriteLine("-------------------------------------------");
                _output.WriteLine("Contenido:");
                _output.WriteLine("-------------------------------------------");
                _output.WriteLine($"{item.ContentFile}");
            };
        }
    }
}
