using ac_insertdocs_inbase.Domain.Dto;

namespace ac_insertdocs_inbase.Domain.Contracts
{
    public interface IReaderDocs
    {
        OperationResult GetDocs(string pathFolder);
        OperationResult ReadDocs(string[] docs);
    }
}
