using ac_insertdocs_inbase.Domain.Dto;

namespace ac_insertdocs_inbase.Domain.Contracts
{
    public interface IFolderRepository
    {
        GetNamesResult GetFolderNames(string pathNamesFolder);
        CreateFoldersResult CreateFoldersByName(IEnumerable<FoldersName> foldersNames);
    }
}
