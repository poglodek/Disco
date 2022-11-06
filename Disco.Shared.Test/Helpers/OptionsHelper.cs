using Microsoft.Extensions.Configuration;

namespace Disco.Service.Users.Integration.Helpers;

public static class OptionsHelper
{
    public static TSettings GetOptions<TSettings>(string section, string settingsFileName = null) where TSettings : class, new()
    {
        settingsFileName ??= "appsettings.json";
        var configuration = new TSettings();
            
        GetConfigurationRoot(settingsFileName)
            .GetSection(section)
            .Bind(configuration);

        return configuration;
    }
        
    private static IConfigurationRoot GetConfigurationRoot(string settingsFileName)
        => new ConfigurationBuilder()
            .AddJsonFile(settingsFileName, optional: true)
            .AddEnvironmentVariables()
            .Build();
}