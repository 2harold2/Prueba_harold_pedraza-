using Microsoft.AspNetCore.Mvc;
using prueba_harold_pedraza.Domain;
using prueba_harold_pedraza.Infraestructure;
using prueba_harold_pedraza.Interfaces;

namespace prueba_harold_pedraza.Controllers;

[ApiController]
[Route("api")]
public class PacienteController : Controller
{
    private IConfiguration _config;
    private IPacienteInfraestructure Repositorie;

    public PacienteController(IConfiguration config)
    {
        this._config = config;
        string dbConnect = this._config.GetValue<string>("ConnectionStrings:DBConnection");

        if (string.IsNullOrEmpty(dbConnect))
            throw new Exception("No se encontro la cadena de conexion");

        this.Repositorie = new Paciente(dbConnect);
    }

    [HttpGet]
    [Route("GetAllPatients")]
    public async Task<List<PatientFormatted>> GetAllPatients()
    {
        List<PatientFormatted> response = await this.Repositorie.GetAllPatients();

        return response;
    }

    [HttpGet]
    [Route("GetPatientById/id")]
    public async Task<PatientRelations> GetPatientById([FromQuery] string id)
    {
        PatientRelations response = await this.Repositorie.GetPatientById(Convert.ToInt32(id));
        return response;
    }

    [HttpPost]
    [Route("CreatePatient")]
    public async Task<bool> CreatePatient([FromBody] PatientRelations request)
    {
        bool response = await this.Repositorie.CreatePatient(request);
        return response;
    }

    [HttpPost]
    [Route("UpdatePatient")]
    public async Task<bool> UpdatePatient([FromBody] PatientRelations request)
    {
        if (request.IdPatient == null || request.IdPatient == 0)
            return false;

        bool response = await this.Repositorie.UpdatePatient(request);
        return response;
    }

    [HttpDelete]
    [Route("DeletePatient")]
    public async Task<bool> DeletePatient([FromQuery] int id)
    {
        bool response = await this.Repositorie.DeletePatient(id);
        return response;
    }
}
