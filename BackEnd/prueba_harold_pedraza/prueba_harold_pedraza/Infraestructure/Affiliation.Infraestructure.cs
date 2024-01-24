using prueba_harold_pedraza.Domain;
using prueba_harold_pedraza.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace prueba_harold_pedraza.Infraestructure;

public class Affiliations: IAffiliationInfraestructure
{
    private string DbConnection = "";

    public Affiliations(string _connection)
    {
        this.DbConnection = _connection;
    }

    public async Task<List<Affiliation>> GetAllAffiliations()
    {
        List<Affiliation> allAffilations = new();

        try
        {
            using (SqlConnection connection = new(this.DbConnection))
            {
                await connection.OpenAsync();
                SqlCommand command = new("ObtenerTodasLasAfiliaciones", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader data = await command.ExecuteReaderAsync();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        allAffilations.Add(new()
                        {
                            IdAffiliationType = DBNull.Value != data["idTipoAfiliacion"] ? Convert.ToInt32(data["idTipoAfiliacion"].ToString()) : 0,
                            Description = DBNull.Value != data["descripcion"] ? data["descripcion"].ToString() : "",
                        }); ;
                    }
                }

                await data.CloseAsync();
            }
        }
        catch (Exception e)
        {
            // log
        }

        return allAffilations;
    }
}
