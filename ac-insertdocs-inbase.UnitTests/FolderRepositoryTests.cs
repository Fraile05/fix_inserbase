using ac_insertdocs_inbase.Domain.Contracts;
using ac_insertdocs_inbase.Domain.Dto;
using ac_insertdocs_inbase.Domain.Helpers;
using ac_insertdocs_inbase.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace ac_insertdocs_inbase.UnitTests
{
    public class FolderRepositoryTests
    {
        private readonly ITestOutputHelper _output;

        public FolderRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private static void BuildRequiredObjects(out IFolderRepository repository)
        {
            var builder = new ConfigurationBuilder();
            UtToolkit.BuildConfig(builder);
            var config = builder.Build();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IFolderRepository, FolderRepository>();
                })
                .Build();

            repository = ActivatorUtilities.CreateInstance<FolderRepository>(host.Services);
        }

        [Fact]
        public void ShouldGetFolderNames()
        {
            BuildRequiredObjects(out IFolderRepository repository);

            string pathNamesFolder = @"C:\Users\jose.fraile\Downloads\nombrescarpeta.txt";

            var data = repository.GetFolderNames(pathNamesFolder);

            Assert.True(data.Success);

            _output.WriteLine("Contenido del archivo con nombres de carpeta");
            _output.WriteLine("-------------------------------------------");
            foreach (var item in data.FoldersNames)
            {
                _output.WriteLine($"Nombre: {item.FolderName}");
            };
        }

        [Fact]
        public void ShouldCreateFolders()
        {
            BuildRequiredObjects(out IFolderRepository repository);

            string pathNamesFolder = @"C:\Users\jose.fraile\Downloads\nombrescarpeta.txt";
            GetNamesResult getNamesResult = repository.GetFolderNames(pathNamesFolder);

            var data = repository.CreateFoldersByName(getNamesResult.FoldersNames);

            Assert.True(data.Success);

            _output.WriteLine("Resultado de creacion de carpetas por nombre");
            _output.WriteLine("-------------------------------------------");
            _output.WriteLine($"Cantidad carpetas creadas : {data.FoldersCreate}");
            _output.WriteLine(data.Message);
        }

        [Fact]
        public void NotShouldCreateFolders()
        {
            BuildRequiredObjects(out IFolderRepository repository);

            string pathNamesFolder = @"C:\Users\jose.fraile\Downloads\nombrescarpeta.txt";
            GetNamesResult getNamesResult = repository.GetFolderNames(pathNamesFolder);

            var data = repository.CreateFoldersByName(getNamesResult.FoldersNames);

            Assert.False(data.Success);

            _output.WriteLine("Resultado de creacion de carpetas por nombre");
            _output.WriteLine("-------------------------------------------");
            _output.WriteLine($"Cantidad carpetas creadas : {data.FoldersCreate}");
            _output.WriteLine(data.Message);
        }
    }
}
