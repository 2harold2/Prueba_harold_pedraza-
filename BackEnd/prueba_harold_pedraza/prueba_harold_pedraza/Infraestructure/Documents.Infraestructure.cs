using prueba_harold_pedraza.Domain;
using prueba_harold_pedraza.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace prueba_harold_pedraza.Infraestructure;

public class Documents: IDocumentInfraestructure
{
    private string DbConnection = "";

    public Documents(string _connection)
    {
        this.DbConnection = _connection;
    }

    public async Task<List<DocumentFormatt>> GetAllDocumments()
    {
        List<DocumentFormatt> allDocuments = new();

        try
        {
            using (SqlConnection connection = new(this.DbConnection))
            {
                await connection.OpenAsync();
                SqlCommand command = new("ObtenerTodosLosDocumentos", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader data = await command.ExecuteReaderAsync();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        allDocuments.Add(new()
                        {
                            IdDocument = DBNull.Value != data["idTipoDocumento"] ? Convert.ToInt32(data["idTipoDocumento"].ToString()) : 0,
                            Name = DBNull.Value != data["Document"] ? data["Document"].ToString() : ""
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

        return allDocuments;
    }
}
