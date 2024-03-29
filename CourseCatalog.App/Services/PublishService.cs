﻿using AutoMapper;
using CourseCatalog.Application.Contracts;
using CourseCatalog.Application.Responses;
using CourseCatalog.Domain.Entities;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CourseCatalog.App.Services
{
    public class PublishService : IPublishCourseService
    {
        private readonly IPublisherApiConfiguration _configuration;
        private readonly IMapper _mapper;

        public PublishService(IMapper mapper, IPublisherApiConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public string BearerToken { get; set; }
        public DateTime TokenExpiration { get; set; }
        public bool TokenHasExpired => TokenExpiration <= DateTime.Now.AddMinutes(-5);

        public async Task<BaseResponse> PublishCourse(Course course)
        {
            if (TokenHasExpired)
                await GetBearerToken(_configuration.ApiRequestUrl, _configuration.ApiPluginClientId,
                    _configuration.ClientSecret);

            var dto = _mapper.Map<UDefCourses>(course);
            //HACK: mapping is not correcting missing subject to empty string
            dto.Subject = dto.Subject ?? string.Empty; 

            var container = new UDefCoursesContainer
            {
                Name = "u_def_courses",
                Tables = new Tables { UDefCourses = dto }
            };

            try
            {
                var post = await _configuration.ApiRequestUrl
                    .AppendPathSegment("ws/schema/table/u_def_courses")
                    .WithOAuthBearerToken(BearerToken)
                    .WithHeader("Content-Type", "application/json")
                    .PostJsonAsync(container)
                    .ReceiveJson();

                var postResult = post.result[0];
                var status = postResult.status.ToLower() == "success";
                var message = JsonConvert.SerializeObject(postResult.success_message);
                var response = new BaseResponse(message, status);
                return response;
            }
            catch (FlurlHttpException ex)
            {
                //Log error
                var errorResponse = await ex.GetResponseJsonAsync();
                throw new Exception($"PowerSchool error: {errorResponse.message}", new Exception($"Payload Error: {ex.Call.RequestBody}", ex));
            }

        }

        public async Task GetBearerToken(string apiRequestUrl, string pluginClientId, string clientSecret)
        {
            var url = apiRequestUrl;
            var config = new PublisherApiConfiguration();
            var result = await url
                .AppendPathSegment(Uri.EscapeUriString("oauth/access_token"))
                .SetQueryParam("grant_type", "client_credentials")
                .SetQueryParam("client_id", config.ApiPluginClientId)
                .SetQueryParam("client_secret", config.ClientSecret)
                .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                .PostAsync().ReceiveJson();

            var accessToken = result.access_token;
            var expiresIn = result.expires_in;

            BearerToken = accessToken;
            TokenExpiration = DateTime.Now.AddMilliseconds(int.Parse(expiresIn ?? 0));
        }

        private static HttpClient MethodHeaders(string bearerToken, string endpointUrl)
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = false };
            var client = new HttpClient(handler) { BaseAddress = new Uri(endpointUrl) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
            return client;
        }
    }

    internal class JsonMessage
    {
        public string Message { get; set; }
    }

    [JsonObject(IsReference = false, Title = "u_def_courses")]
    public class UDefCourses
    {
        [JsonProperty("inow_course_code")] public string CourseCode { get; set; } = string.Empty;

        [JsonProperty("iscareertech")] public string IsCareerTech { get; set; } = string.Empty;

        [JsonProperty("code")] public string CipCode { get; set; } = string.Empty;

        [JsonProperty("course_number")] public string CourseNumber { get; set; } = string.Empty;

        [JsonProperty("course_name")] public string CourseName { get; set; } = string.Empty;

        [JsonProperty("lowgrade")] public string LowGrade { get; set; } = string.Empty;

        [JsonProperty("isspecialed")] public string IsSpecialEd { get; set; } = string.Empty;

        [JsonProperty("collegecoursecode")] public string CollegeCourseCode { get; set; } = string.Empty;

        [JsonProperty("locally_editable")] public string LocallyEditable { get; set; } = string.Empty;

        [JsonProperty("endorsements")] public string Endorsements { get; set; } = string.Empty;

        [JsonProperty("highgrade")] public string HighGrade { get; set; } = string.Empty;

        [JsonProperty("regcoursegroup")] public string Subject { get; set; } = string.Empty;

        [JsonProperty("iscollege")] public string IsCollege { get; set; } = string.Empty;

        [JsonProperty("sched_fullcatalogdescription")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("credit_hours")] public string CreditHours { get; set; } = string.Empty;

        [JsonProperty("credittype")] public string CreditType { get; set; } = string.Empty;

        [JsonProperty("beginyear")] public string BeginYear { get; set; } = string.Empty;

        [JsonProperty("endyear")] public string EndYear { get; set; } = string.Empty;

    }

    [JsonObject(IsReference = false)]
    public class Tables
    {
        [JsonProperty("u_def_courses")] public UDefCourses UDefCourses { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class UDefCoursesContainer
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("tables")] public Tables Tables { get; set; }
    }
}