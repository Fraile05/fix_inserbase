﻿using ac_insertdocs_inbase.Domain.Dto;

namespace ac_insertdocs_inbase.Domain.Contracts
{
    public interface IOperationProcessor
    {
        OperationResult BuildFolders(string pathNamesFolder);
        OperationResult ProcessorFiles(string pathFolder);
    }
}
