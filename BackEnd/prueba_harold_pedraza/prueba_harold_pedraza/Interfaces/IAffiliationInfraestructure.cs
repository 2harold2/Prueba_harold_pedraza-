using prueba_harold_pedraza.Domain;

namespace prueba_harold_pedraza.Interfaces;

public interface IAffiliationInfraestructure
{
    public Task<List<Affiliation>> GetAllAffiliations();
}
