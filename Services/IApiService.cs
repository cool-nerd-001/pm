namespace ProductMicroservice.Services
{
    public interface IApiService
    {

        public Task<HttpResponseMessage> isAuthorized(string token);
    }
}
