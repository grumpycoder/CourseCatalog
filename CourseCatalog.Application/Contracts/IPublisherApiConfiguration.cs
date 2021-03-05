namespace CourseCatalog.Application.Contracts
{
    public interface IPublisherApiConfiguration
    {
        string ApiRequestUrl { get; set; }
        string ApiPluginClientId { get; set; }
        string ClientSecret { get; set; }
    }

}
