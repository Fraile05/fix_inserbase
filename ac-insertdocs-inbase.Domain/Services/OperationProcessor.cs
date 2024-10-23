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
        public OperationProcessor( ILogger<OperationProcessor> logger, IFolderRepository folderRepository, IReaderRepository readerRepository)
        {
            _logger = logger;
            _folderRepository = folderRepository;
            _readerRepository = readerRepository;   
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
        public InsertArgs BuildArgs(string pathFolder)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion BuildArgs");
                return new InsertArgs();
            }
        }
    }
}
