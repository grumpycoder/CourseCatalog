using CourseCatalog.Application.Contracts;
using System.Web.Configuration;

namespace CourseCatalog.App.Services
{
    public class PublisherApiConfiguration : IPublisherApiConfiguration
    {
        public string ApiRequestUrl { get; set; }
        public string ApiPluginClientId { get; set; }
        public string ClientSecret { get; set; }

        public PublisherApiConfiguration()
        {
            ApiRequestUrl = WebConfigurationManager.AppSettings["ApiRequestUrl"];
            ApiPluginClientId = WebConfigurationManager.AppSettings["ApiPluginClientId"];
            ClientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
        }
    }
}
