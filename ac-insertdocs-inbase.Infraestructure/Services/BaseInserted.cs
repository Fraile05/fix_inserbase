using ac_insertdocs_inbase.Domain.Contracts;
using ac_insertdocs_inbase.Domain.Dto;
using ac_insertdocs_inbase.Infraestructure.Entities.Atlas;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ac_insertdocs_inbase.Infraestructure.Services
{
    public class BaseInserted : IBaseInserted
    {
        private readonly ILogger<BaseInserted> _logger;
        private readonly IServiceScopeFactory _resolver;
        private readonly AtlasdbContext _atlasdbContext;

        public BaseInserted(ILogger<BaseInserted> logger, IServiceScopeFactory resolver, AtlasdbContext atlasdbContext)
        {
            _logger = logger;
            _resolver = resolver;
            _atlasdbContext = atlasdbContext;
        }
        public async Task<InsertBaseResult> InsertedInfo(IEnumerable<InsertArgs> args)
        {
            try
            {
                using (var scope = _resolver.CreateScope())
                {
                    using (var context = scope.ServiceProvider.GetRequiredService<AtlasdbContext>())
                    {
                        List<Paramscargue> paramscargues = new List<Paramscargue>();
                        foreach (var arg in args)
                        {
                            new Paramscargue
                            {
                                TipoNegocio = arg.TipoNegocio,
                                Docnamevalide = arg.Docnamevalide,
                                Docnamefields = arg.Docnamefields,
                                Contentvalide = arg.Contentvalide,
                                Contentfields = arg.Contentfields,
                            };
                        };

                        await context.AddRangeAsync(paramscargues);
                        int rowsInserted = await context.SaveChangesAsync();

                        return new InsertBaseResult
                        {
                            IdTipoNegocio = rowsInserted,
                            Success = rowsInserted > 0,
                            Message = rowsInserted > 0 ? "" : "No se logro insertar la informacion en base de datos"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion InsertedInfo");
                return new InsertBaseResult
                {
                    IdTipoNegocio = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
