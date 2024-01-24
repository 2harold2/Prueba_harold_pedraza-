using prueba_harold_pedraza.Domain;

namespace prueba_harold_pedraza.Interfaces;

public interface IDocumentInfraestructure
{
    public Task<List<DocumentFormatt>> GetAllDocumments();
}
