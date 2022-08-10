using Dapper;
using Microsoft.Extensions.Configuration;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Repositories;
using Npgsql;
using System.Data.SqlClient;

namespace MobileStatisticsApp.Infrastructure;
public class MobileStatisticsRepository : IMobileStatisticsRepository
{
    private readonly IConfiguration configuration;
    public MobileStatisticsRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task<bool> AddAsync(MobileStatisticsItem entity)
    {
        entity.Id = Guid.NewGuid();
        entity.LastStatistics = DateTime.Now;
        var sql = "INSERT INTO public.\"MobileStatistics\"(\"Id\", \"Title\", \"LastStatistics\", \"VersionClient\", \"Type\")  VALUES(@Id, @Title, @LastStatistics, @VersionClient, @Type);";
        using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        await connection.ExecuteAsync(sql, entity);
        return true;
    }
    public async Task<IReadOnlyList<MobileStatisticsItem>> GetAllAsync()
    {
        var sql = "SELECT \"Title\", \"LastStatistics\", \"VersionClient\", \"Type\",\"Id\" FROM public.\"MobileStatistics\";";
        using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        var result = await connection.QueryAsync<MobileStatisticsItem>(sql);
        return result.ToList();
    }
    public async Task<MobileStatisticsItem> GetByIdAsync(Guid id)
    {
        var sql = "SELECT \"Id\", \"Title\", \"LastStatistics\", \"VersionClient\", \"Type\" FROM public.\"MobileStatistics\" where \"Id\"=\'" + id + "\'";
        using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        var result = await connection.QuerySingleOrDefaultAsync<MobileStatisticsItem>(sql, new { Id = id });
        return result;
    }
    public async Task<bool> UpdateAsync(MobileStatisticsItem entity)
    {
        var sql = "UPDATE public.\"MobileStatistics\" SET \"Title\" = \'" + entity.Title + "\', \"LastStatistics\" = \'" + entity.LastStatistics + "\', " +
            " \"VersionClient\" = \'" + entity.VersionClient + "\', \"Type\" = \'" + entity.Type + "\' WHERE \"Id\" = \'" + entity.Id + "\'; ";
        using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        await connection.ExecuteAsync(sql, new { Id = entity.Id });
        return true;
    }
}
