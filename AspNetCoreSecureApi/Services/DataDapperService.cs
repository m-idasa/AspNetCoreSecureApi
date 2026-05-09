using Microsoft.Data.SqlClient;
using AspNetCoreSecureApi.Models;
using Dapper;
using Z.Dapper.Plus;
using System.Data;


namespace AspNetCoreSecureApi.Services;

public class DataDapperService
{
    public static IDbConnection  connection = new
        SqlConnection("Server=FATEMEHASADI\\SQLEXPRESS;Database=appdb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
    public static List<Test> UseDapper()
    {
        var sql = "SELECT * FROM tests";
        var tests = new List<Test>();
        tests = connection.Query<Test>(sql).ToList();
        
        return tests;

    }

    public static List<Test> MultipleInsert(List<Test> tests)
    {
        connection.BulkInsert(tests);
        return UseDapper();
    }

    public static List<Bill> ViewBills()
    {
        var sql = "Select * from dbo.bills";
        var bills = new List<Bill>();
        bills = connection.Query<Bill>(sql).ToList();
        
        return bills;
    }

    public static int InsertTest(TestDto testdto)
    {
        int o = connection.Execute(string.Format("insert into dbo.test (Text) values({0});",testdto.Text));
        return o;
    }
}
