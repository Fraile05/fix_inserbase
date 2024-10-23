using System;
using System.Collections.Generic;

namespace ac_insertdocs_inbase.Infraestructure.Entities.Atlas;

public partial class Paramscargue
{
    public long IdTipoNegocio { get; set; }

    public string TipoNegocio { get; set; } = null!;

    public string Docnamevalide { get; set; } = null!;

    public string Docnamefields { get; set; } = null!;

    public string Contentvalide { get; set; } = null!;

    public string Contentfields { get; set; } = null!;
}
