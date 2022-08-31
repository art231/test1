using System.Data;
using Dapper;

namespace MobileStatisticsApp.Infrastructure;

/// <summary>
/// Выгрузка миграций.
/// </summary>
public class DapperDatabase
{
    private readonly IDbConnection dbconnection;

    /// <summary>
    /// Конструктор для базы данных.
    /// </summary>
    /// <param name="dbconnection">Подключение.</param>
    public DapperDatabase(IDbConnection dbconnection)
    {
        this.dbconnection = dbconnection;
    }

    /// <summary>
    /// Создание базы данных.
    /// </summary>
    /// <param name="dbName">Название базы данных.</param>
    public void CreateDatabase(string dbName)
    {
        var query = "SELECT datname FROM pg_database where datname = @dbName";
        var path = Path.Combine(Environment.CurrentDirectory, @"CreateDatabase.sql");
        var script = File.ReadAllText(path);
        var parameters = new DynamicParameters();
        parameters.Add("dbName", dbName);
        dbconnection.Open();
        try
        {
            var records = dbconnection.Query(query, parameters);
            if (!records.Any()) dbconnection.Execute(script);
        }
        finally
        {
            dbconnection.Close();
        }
    }
}