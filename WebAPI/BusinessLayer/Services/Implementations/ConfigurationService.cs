using BusinessLayer.Exceptions;
using BusinessLayer.Services.Contracts;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services.Implementations
{
	public class ConfigurationService : IConfigurationService
	{
		private readonly IConfiguration configuration;

		public ConfigurationService(IConfiguration configuration)
        {
			this.configuration = configuration;
		}

		public string GetSetting(string key)
			=> configuration[key] ?? throw new ConfigurationKeyNotFoundException($"{key} was not found in configuration.");
	}
}
