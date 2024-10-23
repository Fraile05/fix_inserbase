using ac_insertdocs_inbase.Domain.Dto;

namespace ac_insertdocs_inbase.Domain.Contracts
{
    public interface IReaderRepository
    {
        GetFolderResult GetFolders(string pathFolders);
        GetDocsResult GetDocs(string pathFolder);
        ReadDocsResult ReadDocs(IEnumerable<FileInfo> files);
    }
}
