using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Responses;
using CourseCatalog.Domain.Entities;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace CourseCatalog.App.Services
{
    public class PublishService : IPublishCourseService
    {
        private readonly IMapper _mapper;
        private readonly IPublisherApiConfiguration _configuration;

        public string BearerToken { get; set; }
        public DateTime TokenExpiration { get; set; }
        public bool TokenHasExpired => TokenExpiration <= DateTime.Now.AddMinutes(-5);

        public PublishService(IMapper mapper, IPublisherApiConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BaseResponse> PublishCourse(Course course)
        {

            if (TokenHasExpired)
                GetBearerToken(_configuration.ApiRequestUrl, _configuration.ApiPluginClientId,
                    _configuration.ClientSecret);

            var publishEndPointUrl = WebConfigurationManager.AppSettings["PublishEndPointURL"];
            var client = MethodHeaders(BearerToken, publishEndPointUrl);

            var dto = _mapper.Map<UDefCourses>(course);
            var container = new UDefCoursesContainer()
            {
                Name = "u_def_courses",
                Tables = new Tables() { UDefCourses = dto }
            };
            var json = JsonConvert.SerializeObject(container);
            var request =
                new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()))
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var tokenResponse =
                await client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content);

            Log.Logger.Information($"bearer token: {BearerToken}");
            Log.Logger.Information($"tokenResponse Status: {tokenResponse.ReasonPhrase}");

            if (!tokenResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to publish: {tokenResponse.ReasonPhrase}");
            }

            var message = await tokenResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(message)) throw new Exception("No response message from publish endpoint");
            Log.Logger.Information(message);

            var jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(message);
            var response = new BaseResponse(jsonMessage.Message, tokenResponse.IsSuccessStatusCode);
            return response;
        }

        public void GetBearerToken(string apiRequestUrl, string pluginClientId, string clientSecret)
        {
            var base64Encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(pluginClientId + ":" + clientSecret));
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var apiRequest = (HttpWebRequest)WebRequest.Create(apiRequestUrl + "/oauth/access_token?grant_type=client_credentials");

            apiRequest.Method = "POST";
            apiRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            apiRequest.Accept = "application/json";

            WebHeaderCollection authorizationHeaders = apiRequest.Headers;
            authorizationHeaders.Add("Authorization: Basic " + base64Encoded);

            var apiResponse = apiRequest.GetResponse();
            var stream = apiResponse.GetResponseStream();
            var streamReader = new StreamReader(stream ?? throw new InvalidOperationException("No response getting bearer token"), Encoding.Default);
            var content = streamReader.ReadToEnd();
            stream.Close();
            apiResponse.Close();
            dynamic responseObject = JsonConvert.DeserializeObject(content);
            string apiBearerToken = responseObject?["access_token"].ToString();
            BearerToken = apiBearerToken;
            var expiresIn = responseObject?["expires_in"].ToString();
            TokenExpiration = DateTime.Now.AddMilliseconds(int.Parse(expiresIn ?? 0));
        }

        private static HttpClient MethodHeaders(string bearerToken, string endpointUrl)
        {
            var handler = new HttpClientHandler() { UseDefaultCredentials = false };
            var client = new HttpClient(handler) { BaseAddress = new Uri(endpointUrl) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
            return client;
        }
    }

    class JsonMessage
    {
        public string Message { get; set; }
    }

    [JsonObject(IsReference = false, Title = "u_def_courses")]
    public class UDefCourses
    {
        [JsonProperty("inow_course_code")]
        public string CourseCode { get; set; }

        [JsonProperty("iscareertech")]
        public string IsCareerTech { get; set; }

        [JsonProperty("code")]
        public string CipCode { get; set; }

        [JsonProperty("course_number")]
        public string CourseNumber { get; set; }

        [JsonProperty("course_name")]
        public string CourseName { get; set; }

        [JsonProperty("lowgrade")]
        public string LowGrade { get; set; }

        [JsonProperty("isspecialed")]
        public string IsSpecialEd { get; set; }

        [JsonProperty("collegecoursecode")]
        public string CollegeCourseCode { get; set; }

        [JsonProperty("locally_editable")]
        public string LocallyEditable { get; set; }

        [JsonProperty("endorsements")]
        public string Endorsements { get; set; }

        [JsonProperty("highgrade")]
        public string HighGrade { get; set; }

        [JsonProperty("regcoursegroup")]
        public string Subject { get; set; }

        [JsonProperty("iscollege")]
        public string IsCollege { get; set; }

        [JsonProperty("sched_fullcatalogdescription")]
        public string Description { get; set; }

        [JsonProperty("credit_hours")]
        public string CreditHours { get; set; }

        [JsonProperty("credittype")]
        public string CreditType { get; set; }

        [JsonProperty("beginyear")]
        public string BeginYear { get; set; }

        [JsonProperty("endyear")]
        public string EndYear { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class Tables
    {
        [JsonProperty("u_def_courses")]
        public UDefCourses UDefCourses { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class UDefCoursesContainer
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tables")]
        public Tables Tables { get; set; }
    }

}
