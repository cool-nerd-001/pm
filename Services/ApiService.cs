using System.Net.Http.Headers;

namespace ProductMicroservice.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;

        public ApiService(IConfiguration configuration)
        {

            _configuration = configuration ??
                    throw new ArgumentNullException(nameof(configuration));

        }


        public async Task<HttpResponseMessage> isAuthorized(string token)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {

                string? domin = _configuration["ActiveDirectoryMicroservice:domin"];
                client.BaseAddress = new Uri(domin);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = await client.GetAsync("/api/rest/v1/validate/admin/");
            }


            return response;
        }
    }
}
