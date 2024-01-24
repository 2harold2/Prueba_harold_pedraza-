using Microsoft.AspNetCore.Mvc;
using prueba_harold_pedraza.Domain;
using prueba_harold_pedraza.Infraestructure;
using prueba_harold_pedraza.Interfaces;

namespace prueba_harold_pedraza.Controllers;


[ApiController]
[Route("api")]
public class DocumentsController : Controller
{
    private IConfiguration _config;
    private IDocumentInfraestructure Repositorie;

    public DocumentsController(IConfiguration config)
    {
        this._config = config;
        string dbConnect = this._config.GetValue<string>("ConnectionStrings:DBConnection");

        if (string.IsNullOrEmpty(dbConnect))
            throw new Exception("No se encontro la cadena de conexion");

        this.Repositorie = new Documents(dbConnect);
    }

    [HttpGet]
    [Route("GetAllDocuments")]
    public async Task<List<DocumentFormatt>> GetAllPatients()
    {
        List<DocumentFormatt> response = await this.Repositorie.GetAllDocumments();
        return response;
    }
}
