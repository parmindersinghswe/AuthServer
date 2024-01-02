using Microsoft.Extensions.Configuration;

namespace Auth.Domain.Utilities
{
	public class ConfigurationUtility
	{
		public static ConfigurationType GetConfigurationSection<ConfigurationType>(string sectionName, IConfiguration configuration)  where ConfigurationType : new ()
		{
			var configurations = new ConfigurationType();
			configuration.GetSection(sectionName).Bind(configurations);
			return configurations;
		}
	}
}
