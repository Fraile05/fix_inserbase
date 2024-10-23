using ac_insertdocs_inbase.Domain.Contracts;
using ac_insertdocs_inbase.Domain.Dto;
using Microsoft.Extensions.Logging;

namespace ac_insertdocs_inbase.Domain.Services
{
    public class OperationProcessor : IOperationProcessor
    {
        private readonly ILogger<OperationProcessor> _logger;
        private readonly IFolderRepository _folderRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly IBaseInserted _baseInserted;
        public OperationProcessor( ILogger<OperationProcessor> logger, IFolderRepository folderRepository, IReaderRepository readerRepository, IBaseInserted baseInserted)
        {
            _logger = logger;
            _folderRepository = folderRepository;
            _readerRepository = readerRepository;
            _baseInserted = baseInserted;
        }
        public OperationResult BuildFolders(string pathNamesFolder)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(pathNamesFolder)) 
                {
                    var listNamesFolders = _folderRepository.GetFolderNames(pathNamesFolder);
                    _folderRepository.CreateFoldersByName(listNamesFolders.FoldersNames);
                }
                return new OperationResult { Success = true, Message = "" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion BuildFolders");
                return new OperationResult { Success = false, Message = ex.Message };
            }
        }
        public OperationResult ProcessorFiles(string pathFolder)
        {
            try
            {
                List<InsertArgs> args = new List<InsertArgs>();
                int countId = 0;
                var folders = _readerRepository.GetFolders(pathFolder);
                foreach (var folder in folders.Folder)
                {
                    var docs = _readerRepository.GetDocs(folder.FolderName!);
                    var mapsdocs = _readerRepository.ReadDocs(docs.Files);

                    string wordValide = DomainConstants.NAMEVALIDE;
                    string nameFileValide = "";
                    string contentFileValide = "";
                    foreach (var doc in mapsdocs.Docs)
                    {
                        if(doc.NameFile!.ToLower().Contains(wordValide.ToLower()))
                        {
                            nameFileValide = doc.NameFile;
                            contentFileValide = doc.ContentFile!;
                            break;
                        }
                    }

                    string wordFields = DomainConstants.NAMEFIELDS;
                    string nameFileFields = "";
                    string contentFileFields = "";
                    foreach (var doc in mapsdocs.Docs)
                    {
                        if (doc.NameFile!.ToLower().Contains(wordFields.ToLower()))
                        {
                            nameFileFields = doc.NameFile;
                            contentFileFields = doc.ContentFile!;
                            break;
                        }
                    }
                    countId++;
                    args.Add(new InsertArgs
                    {
                        IdArgs = countId,
                        TipoNegocio = folder.FolderName!,
                        Docnamevalide = nameFileValide,
                        Docnamefields = nameFileFields,
                        Contentvalide = contentFileValide,
                        Contentfields = contentFileFields
                    });                
                }
                var insertArgsResult = new InsertArgsResult
                {
                    InsertArgs = args,
                    Success = args.Any(),
                    Message = args.Any() ? "" : "No se logro crear argumentos para la insercion"
                };

                var insertBaseResult = _baseInserted.InsertedInfo(insertArgsResult.InsertArgs);

                return new OperationResult
                {
                    Success = insertArgsResult.Success,
                    Message = insertArgsResult.Message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion ProcessorFiles");
                return new OperationResult
                {
                    Success = false,
                    Message = ex.Message   
                };
            }
        }
    }
}
