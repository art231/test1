using Microsoft.AspNetCore.Mvc.Testing;

namespace MobileStatisticsApp.Test;

public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
}