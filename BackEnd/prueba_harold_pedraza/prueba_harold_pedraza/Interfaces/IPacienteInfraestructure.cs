using prueba_harold_pedraza.Domain;

namespace prueba_harold_pedraza.Interfaces;

public interface IPacienteInfraestructure
{
    public Task<List<PatientFormatted>> GetAllPatients();

    public Task<PatientRelations> GetPatientById(int id);

    public Task<bool> CreatePatient(PatientRelations request);

    public Task<bool> UpdatePatient(PatientRelations request);

    public Task<bool> DeletePatient(int id);
}
