using Microsoft.AspNetCore.Mvc.Testing;

namespace MobileStatisticsApp.Test;

/// <summary>
///     Общий класс для тестов.
/// </summary>
/// <typeparam name="TEntryPoint"></typeparam>
public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
}