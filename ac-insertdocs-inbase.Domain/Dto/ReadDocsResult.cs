namespace ac_insertdocs_inbase.Domain.Dto
{
    public class ReadDocsResult : OperationResult
    {
        public IEnumerable<Docs> Docs { get; set; } = new List<Docs>();
    }
}
