using System.Data;
using Dapper;
using Microsoft.Extensions.Options;

namespace MobileStatisticsApp.Infrastructure;

public class DapperDatabase
{
    private readonly IDbConnection dbconnection;
    private readonly PathToFileSql pathToFileSql;

    public DapperDatabase(IDbConnection dbconnection,
        IOptions<PathToFileSql> pathToFileSql)
    {
        this.dbconnection = dbconnection;
        this.pathToFileSql = pathToFileSql.Value;
    }

    public void CreateDatabase(string dbName)
    {
        var query = "SELECT datname FROM pg_database where datname = @dbName";
        var script = File.ReadAllText(this.pathToFileSql.Path);
        var parameters = new DynamicParameters();
        parameters.Add("dbName", dbName);
        dbconnection.Open();
        try
        {
            var records = dbconnection.Query(query, parameters);
            if (!records.Any())
            {
                dbconnection.Execute(script);
            }
        }
        finally
        {
            dbconnection.Close();
        }
    }
}