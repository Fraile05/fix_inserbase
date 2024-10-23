namespace ac_insertdocs_inbase.Domain.Dto
{
    public class InsertArgsResult : OperationResult
    {
        public IEnumerable<InsertArgs> InsertArgs { get; set; } = new List<InsertArgs>();
    }
}
