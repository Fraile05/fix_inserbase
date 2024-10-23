namespace ac_insertdocs_inbase.Domain.Dto
{
    public class GetNamesResult : OperationResult
    {
        public IEnumerable<FoldersName> FoldersNames { get; set; } = new List<FoldersName>();
    }
}
