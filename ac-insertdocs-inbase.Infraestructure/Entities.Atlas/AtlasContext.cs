using ac_insertdocs_inbase.Domain.Helpers;

namespace ac_insertdocs_inbase.Infraestructure.Entities.Atlas
{
    public class AtlasContext
    {
        private readonly ConfigurationValues _configurationValues;
        public AtlasContext(ConfigurationValues configurationValues)
        {
            _configurationValues = configurationValues;
        }
    }
}
