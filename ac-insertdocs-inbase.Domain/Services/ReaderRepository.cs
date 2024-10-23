using ac_insertdocs_inbase.Domain.Contracts;
using ac_insertdocs_inbase.Domain.Dto;
using Microsoft.Extensions.Logging;

namespace ac_insertdocs_inbase.Domain.Services
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly ILogger<ReaderRepository> _logger;

        public ReaderRepository(ILogger<ReaderRepository> logger)
        {
            _logger = logger;
        }

        public GetDocsResult GetDocs(string pathFolder)
        {
            try
            {
                string searchPattern = DomainConstants.PATTERN_TMD;
                List<FileInfo> docs  = Directory.GetFiles(pathFolder, searchPattern).Select(x => new FileInfo(x)).ToList();
                return new GetDocsResult
                {
                    Files = docs,
                    Success = docs.Any(),
                    Message = docs.Any() ? "" : "No se logro obtener los archivos en la ruta especificada"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion GetDocs");
                return new GetDocsResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public ReadDocsResult ReadDocs(IEnumerable<FileInfo> files)
        {
            try
            {
                List<Docs> docsList = new List<Docs>();
                foreach (FileInfo doc in files)
                {
                    docsList.Add(new Docs
                    {
                        NameFile = doc.Name,
                        ContentFile = File.ReadAllText(doc.FullName)
                    });
                }
                return new ReadDocsResult
                {
                    Docs = docsList,
                    Success = docsList.Any(),
                    Message = docsList.Any() ? "" : "No se logro leer los archivos almacenados"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion ReadDocs");
                return new ReadDocsResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
