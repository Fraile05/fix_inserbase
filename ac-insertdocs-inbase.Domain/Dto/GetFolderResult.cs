namespace ac_insertdocs_inbase.Domain.Dto
{
    public class GetFolderResult : OperationResult
    {
        public IEnumerable<Folder> Folder {  get; set; } = new List<Folder>();
    }
}
