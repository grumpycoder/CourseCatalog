using System.Web.Configuration;
using CourseCatalog.Application.Contracts;

namespace CourseCatalog.App.Services
{
    public class PublisherApiConfiguration : IPublisherApiConfiguration
    {
        public PublisherApiConfiguration()
        {
            ApiRequestUrl = WebConfigurationManager.AppSettings["ApiRequestUrl"];
            ApiPluginClientId = WebConfigurationManager.AppSettings["ApiPluginClientId"];
            ClientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
        }

        public string ApiRequestUrl { get; set; }
        public string ApiPluginClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}