using ac_insertdocs_inbase.Domain.Contracts;
using ac_insertdocs_inbase.Domain.Dto;
using ac_insertdocs_inbase.Infraestructure.Entities.Atlas;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ac_insertdocs_inbase.Infraestructure.Services
{
    public class BaseInserted : IBaseInserted
    {
        private readonly ILogger<BaseInserted> _logger;
        private readonly IServiceScopeFactory _resolver;
        private readonly AtlasContext _atlasContext;

        public BaseInserted(ILogger<BaseInserted> logger, IServiceScopeFactory resolver, AtlasContext atlasContext)
        {
            _logger = logger;
            _resolver = resolver;
            _atlasContext = atlasContext;
        }
        public async Task<InsertBaseResult> InsertedInfo(List<Docs> docsList)
        {
            try
            {
                return new InsertBaseResult
                {
                    IdSeccion = 0,
                    Success = true,
                    Message = ""
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion InsertedInfo");
                return new InsertBaseResult
                {
                    IdSeccion = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
