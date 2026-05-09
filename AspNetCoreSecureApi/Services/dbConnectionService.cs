using Microsoft.Data.SqlClient;
using System.Data;

namespace AspNetCoreSecureApi.Services;

public class dbConnectionService
{
    public String RunStoredProc(String input)
    {
        SqlConnection conn = null;
        SqlDataReader rdr = null;

        Console.WriteLine("\nTop 10 Most Expensive Products:\n");
        String result = "no result";
        try
        {
            conn = new SqlConnection("Server=FATEMEHASADI\\SQLEXPRESS;Database=appdb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Insert_test", conn);
            
            IDbDataParameter idParameter = cmd.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Direction = System.Data.ParameterDirection.Output;
            idParameter.DbType = System.Data.DbType.Int32;

            cmd.Parameters.Add(idParameter);
            cmd.Parameters.Add("@Text", SqlDbType.VarChar).Value = input;

            
            
            cmd.CommandType = CommandType.StoredProcedure;
            rdr = cmd.ExecuteReader();
            
            result = idParameter.Value.ToString();
                    
            
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
            if (rdr != null)
            {
                rdr.Close();
            }
        }
        return result;
    }
}
