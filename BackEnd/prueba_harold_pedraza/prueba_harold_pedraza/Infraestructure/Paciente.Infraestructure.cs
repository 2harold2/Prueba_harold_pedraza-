using prueba_harold_pedraza.Domain;
using prueba_harold_pedraza.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace prueba_harold_pedraza.Infraestructure;

public class Paciente: IPacienteInfraestructure
{
    private string DbConnection = "";

    public Paciente(string _connection)
    {
        this.DbConnection = _connection;
    }


    public async Task<List<PatientFormatted>> GetAllPatients()
    {
        List<PatientFormatted> allPatients = new();

        try
        {
            using (SqlConnection connection = new(this.DbConnection)) {
                await connection.OpenAsync();
                SqlCommand command = new("ObtenerTodosLosPacientes", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader data = await command.ExecuteReaderAsync();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        allPatients.Add(new()
                        {
                            IdPatient = DBNull.Value != data["idPaciente"] ? Convert.ToInt32(data["idPaciente"].ToString()) : 0,
                            DocumentType = DBNull.Value != data["tipoDocumento"] ? data["tipoDocumento"].ToString() : "",
                            DocumentNumber = DBNull.Value != data["numeroDocumento"] ? data["numeroDocumento"].ToString() : "",
                            Names = DBNull.Value != data["nombres"] ? data["nombres"].ToString() : "",
                            LastNames = DBNull.Value != data["apellidos"] ? data["apellidos"].ToString() : "",
                            Email = DBNull.Value != data["email"] ? data["email"].ToString() : "",
                            Phone = DBNull.Value != data["telefono"] ? data["telefono"].ToString() : "",
                            BirthDate = DBNull.Value != data["fechaNacimiento"] ? data["fechaNacimiento"].ToString() : "",
                            Afiliation = DBNull.Value != data["Afiliacion"] ? data["Afiliacion"].ToString() : "",
                        });
                    }
                }

                await data.CloseAsync();
            }
        }
        catch (Exception e) {
            // log
        }

        return allPatients;
    }

    public async Task<PatientRelations> GetPatientById(int id)
    {
        PatientRelations patient = new();

        try
        {
            using (SqlConnection connection = new(this.DbConnection))
            {
                await connection.OpenAsync();
                SqlCommand command = new("ObtenerUnPaciente", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@idPaciente", id));

                SqlDataReader data = await command.ExecuteReaderAsync();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        patient = new()
                        {
                            IdPatient = DBNull.Value != data["idPaciente"] ? Convert.ToInt32(data["idPaciente"].ToString()) : 0,
                            IdTypeDocument = DBNull.Value != data["idTipoDocumento"] ? Convert.ToInt32(data["idTipoDocumento"].ToString()) : 0,
                            DocumentNumber = DBNull.Value != data["numeroDocumento"] ? data["numeroDocumento"].ToString() : "",
                            Names = DBNull.Value != data["nombres"] ? data["nombres"].ToString() : "",
                            LastNames = DBNull.Value != data["apellidos"] ? data["apellidos"].ToString() : "",
                            Email = DBNull.Value != data["email"] ? data["email"].ToString() : "",
                            Phone = DBNull.Value != data["telefono"] ? data["telefono"].ToString() : "",
                            BirthDate = DBNull.Value != data["fechaNacimiento"] ? data["fechaNacimiento"].ToString() : "",
                            IdAffiliationType = DBNull.Value != data["idTipoAfiliacion"] ? Convert.ToInt32(data["idTipoAfiliacion"].ToString()) : 0,
                        };
                    }
                }

                await data.CloseAsync();
            }
        }
        catch (Exception e)
        {
            // log
        }

        return patient;
    }

    public async Task<bool> CreatePatient(PatientRelations request)
    {
        bool success = false;

        try
        {
            using (SqlConnection connection = new(this.DbConnection))
            {
                await connection.OpenAsync();
                SqlCommand command = new("CrearPaciente", connection);
                command.CommandType = CommandType.StoredProcedure;
      
                command.Parameters.Add(new SqlParameter("@numeroDocumento", request.DocumentNumber));
                command.Parameters.Add(new SqlParameter("@nombres", request.Names));
                command.Parameters.Add(new SqlParameter("@apellidos", request.LastNames));
                command.Parameters.Add(new SqlParameter("@email", request.Email));
                command.Parameters.Add(new SqlParameter("@telefono", request.Phone));
                command.Parameters.Add(new SqlParameter("@fechaNacimiento", request.BirthDate));
                command.Parameters.Add(new SqlParameter("@idTipoDocumento", request.IdTypeDocument));
                command.Parameters.Add(new SqlParameter("@idTipoAfiliacion", request.IdAffiliationType));

                await command.ExecuteNonQueryAsync();
                success = true;
            }
        }
        catch (Exception e)
        {
        }

        return success;
    }

    public async Task<bool> UpdatePatient(PatientRelations request)
    {
        bool success = false;

        try
        {
            using (SqlConnection connection = new(this.DbConnection))
            {
                await connection.OpenAsync();
                SqlCommand command = new("ActualizarPaciente", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@idPaciente", request.IdPatient));
                command.Parameters.Add(new SqlParameter("@numeroDocumento", request.DocumentNumber));
                command.Parameters.Add(new SqlParameter("@nombres", request.Names));
                command.Parameters.Add(new SqlParameter("@apellidos", request.LastNames));
                command.Parameters.Add(new SqlParameter("@email", request.Email));
                command.Parameters.Add(new SqlParameter("@telefono", request.Phone));
                command.Parameters.Add(new SqlParameter("@fechaNacimiento", request.BirthDate));
                command.Parameters.Add(new SqlParameter("@idTipoDocumento", request.IdTypeDocument));
                command.Parameters.Add(new SqlParameter("@idTipoAfiliacion", request.IdAffiliationType));
                await command.ExecuteNonQueryAsync();
                success = true;
            }
        }
        catch (Exception e)
        {
        }

        return success;
    }

    public async Task<bool> DeletePatient(int id)
    {
        bool success = false;

        try
        {
            using (SqlConnection connection = new(this.DbConnection))
            {
                await connection.OpenAsync();
                SqlCommand command = new("EliminarPaciente", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@idPaciente", id));
                await command.ExecuteNonQueryAsync();
                success = true;
            }
        }
        catch (Exception e)
        {
        }

        return success;
    }
}
