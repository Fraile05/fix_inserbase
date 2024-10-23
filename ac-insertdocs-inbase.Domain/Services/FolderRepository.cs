using ac_insertdocs_inbase.Domain.Contracts;
using ac_insertdocs_inbase.Domain.Dto;
using Microsoft.Extensions.Logging;

namespace ac_insertdocs_inbase.Domain.Services
{
    public class FolderRepository : IFolderRepository
    {
        private readonly ILogger<FolderRepository> _logger;
        public FolderRepository(ILogger<FolderRepository> logger)
        {
            _logger = logger;
        }

        public GetNamesResult GetFolderNames(string pathNamesFolder)
        {
            try
            {
                List<FoldersName> foldersNames = new List<FoldersName>();
                string[] namesFolders = File.ReadAllLines(pathNamesFolder);

                foreach (var nameFolder in namesFolders)
                {
                    foldersNames.Add(new FoldersName
                    {
                        FolderName = nameFolder
                    });
                };
                return new GetNamesResult
                {
                    FoldersNames = foldersNames,
                    Success = foldersNames.Any(),
                    Message = foldersNames.Any() ? "" : "No se logro leer la lista de nombres en la ruta"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion GetFolderNames");
                return new GetNamesResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public CreateFoldersResult CreateFoldersByName(IEnumerable<FoldersName> foldersNames)
        {
            try
            {
                CreateFoldersResult folders = new CreateFoldersResult();
                string pathFoldersCreate = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";

                foreach (var folderName in foldersNames)
                {
                    string pathFolder = Path.Combine(pathFoldersCreate, folderName.FolderName!);
                    if (!Directory.Exists(pathFolder))
                    {
                        Directory.CreateDirectory(pathFolder);
                        folders.FoldersCreate = folders.FoldersCreate + 1;
                    };
                };
                return new CreateFoldersResult
                {
                    FoldersCreate = folders.FoldersCreate,
                    Success = folders.FoldersCreate > 0,
                    Message = folders.FoldersCreate > 0 ? "" : "No se logro crear ninguna carpeta"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en funcion CreateFoldersByName");
                return new CreateFoldersResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
