namespace ac_insertdocs_inbase.Domain.Dto
{
    public class InsertArgs
    {
        public int IdArgs {get; set;}
        public string TipoNegocio {get; set;} = string.Empty;
        public string Docnamevalide { get; set; } = string.Empty;
        public string Docnamefields { get; set; } = string.Empty;
        public string Contentvalide { get; set; } = string.Empty;
        public string Contentfields { get; set; } = string.Empty;
    }
}
