using ac_insertdocs_inbase.Domain.Contracts;
using ac_insertdocs_inbase.Domain.Helpers;
using ac_insertdocs_inbase.Domain.Services;
using ac_insertdocs_inbase.Infraestructure.Entities.Atlas;
using ac_insertdocs_inbase.Infraestructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace ac_insertdocs_inbase.UnitTests
{
    public class BaseInsertedTests
    {
        private readonly ITestOutputHelper _output;

        public BaseInsertedTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private static void BuildRequiredObjects(out IBaseInserted repository, out IReaderRepository reader)
        {
            var builder = new ConfigurationBuilder();
            UtToolkit.BuildConfig(builder);
            var config = builder.Build();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<AtlasdbContext>();
                    services.AddSingleton<ConfigurationValues>();
                    services.AddTransient<IBaseInserted, BaseInserted>();
                    services.AddTransient<IReaderRepository, ReaderRepository>();
                })
                .Build();

            repository = ActivatorUtilities.CreateInstance<BaseInserted>(host.Services);
            reader = ActivatorUtilities.CreateInstance<ReaderRepository>(host.Services);
        }

        [Fact]
        public void ShouldInserted()
        {
            BuildRequiredObjects(out IBaseInserted repository, out IReaderRepository reader);

            string pathFolder = @"C:\Users\jose.fraile\Documents\appperson\pruebadocs";
            var getFolder = reader.GetFolders(pathFolder);
            foreach (var folder in getFolder.Folder) 
            {
                var fileinfo = reader.GetDocs(folder.FolderPath);
                reader.ReadDocs(fileinfo.Files);
            }


            var data = repository.InsertedInfo(args);

            Assert.True(data.Success);

            _output.WriteLine("Contenido de la lista de archivos obtenidos");
            _output.WriteLine("-------------------------------------------");
            foreach (var item in data.Files)
            {
                _output.WriteLine($"Nombre: {item.Name}, Tamaño: {item.Length} bytes, Ruta: {item.FullName}");
            };
        }
    }
}
