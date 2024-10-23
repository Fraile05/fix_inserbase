using ac_insertdocs_inbase.Domain.Dto;

namespace ac_insertdocs_inbase.Domain.Contracts
{
    public interface IBaseInserted
    {
        Task<InsertBaseResult> InsertedInfo(IEnumerable<InsertArgs> args);
    }
}
