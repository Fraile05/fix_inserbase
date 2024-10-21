namespace ac_insertdocs_inbase.Domain.Dto
{
    public class GetDocsResult : OperationResult
    {
        public IEnumerable<FileInfo> Files { get; set; } = new List<FileInfo>();
    }
}
