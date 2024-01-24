using Microsoft.AspNetCore.Mvc;
using prueba_harold_pedraza.Domain;
using prueba_harold_pedraza.Infraestructure;
using prueba_harold_pedraza.Interfaces;

namespace prueba_harold_pedraza.Controllers;

[ApiController]
[Route("api")]
public class AffiliationController : Controller
{
    private IConfiguration _config;
    private IAffiliationInfraestructure Repositorie;

    public AffiliationController(IConfiguration config)
    {
        this._config = config;
        string dbConnect = this._config.GetValue<string>("ConnectionStrings:DBConnection");

        if (string.IsNullOrEmpty(dbConnect))
            throw new Exception("No se encontro la cadena de conexion");

        this.Repositorie = new Affiliations(dbConnect);
    }

    [HttpGet]
    [Route("GetAllAffilations")]
    public async Task<List<Affiliation>> GetAllAffilations()
    {
        List<Affiliation> response = await this.Repositorie.GetAllAffiliations();
        return response;
    }
}
