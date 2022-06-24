namespace Poketto.Application.Common.Options
{
    public class ApplicationScopes
    {
        public const string ConfigSectionName = "ApplicationScopes";
        public string ChartOfAccountsRead { get; set; } = string.Empty;
        public string ChartOfAccountsReadWrite { get; set; } = string.Empty;
        public string TransactionsRead { get; set; } = string.Empty;
        public string TransactionsReadWrite { get; set; } = string.Empty;

    }
}
