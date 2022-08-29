using System.Data;
using Dapper;
using Microsoft.Extensions.Options;

namespace MobileStatisticsApp.Infrastructure;
/// <summary>
/// Выгрузка миграций.
/// </summary>
public class DapperDatabase
{
    private readonly IDbConnection dbconnection;
    private readonly PathToFileSql pathToFileSql;
    /// <summary>
    /// Конструктор для базы данных.
    /// </summary>
    /// <param name="dbconnection">Подключение.</param>
    /// <param name="pathToFileSql">Путь к файлу.</param>
    public DapperDatabase(IDbConnection dbconnection,
        IOptions<PathToFileSql> pathToFileSql)
    {
        this.dbconnection = dbconnection;
        this.pathToFileSql = pathToFileSql.Value;
    }
    /// <summary>
    /// Создание базы данных.
    /// </summary>
    /// <param name="dbName">Название базы данных.</param>
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