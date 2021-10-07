namespace TwitchCommunity.Persistence.Configuration
{
    internal class PersistenceConfiguration
    {
        public const string ConfigurationSectionName = "TwitchCommunity:Persistence";

        public string ProviderName { get; set; }

        public string ConnectionString { get; set; }
    }
}
