using ac_insertdocs_inbase.Domain.Contracts;
using ac_insertdocs_inbase.Domain.Dto;
using Microsoft.Extensions.Logging;

namespace ac_insertdocs_inbase.Domain.Services
{
    public class ReaderDocs : IReaderDocs
    {
        private readonly ILogger<ReaderDocs> _logger;

        public ReaderDocs(ILogger<ReaderDocs> logger)
        {
            _logger = logger;
        }

        public OperationResult GetDocs(string pathFolder)
        {
            try
            {
                string searchPattern = DomainConstants.PATTERN_TMD;
                string[] docs = Directory.GetFiles(pathFolder, searchPattern);
                if (docs.Length < 1)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "No se logro obtener los archivos en la ruta especificada"
                    };
                }
                return new OperationResult
                {
                    Success = true,
                    Message = ""
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion GetDocs");
                return new OperationResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public OperationResult ReadDocs(string[] docs)
        {
            try
            {
                List<Docs> docsList = new List<Docs>();
                foreach (string doc in docs)
                {
                    string fileName = Path.GetFileName(doc);
                    string contentDoc = File.ReadAllText(doc);

                    docsList.Add(new Docs
                    {
                        NameFile = fileName,
                        ContentFile = contentDoc
                    });
                }
                if (!docsList.Any())
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "No se logro leer los archivos almacenados"
                    };
                }
                return new OperationResult
                {
                    Success = true,
                    Message = ""
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion ReadDocs");
                return new OperationResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
