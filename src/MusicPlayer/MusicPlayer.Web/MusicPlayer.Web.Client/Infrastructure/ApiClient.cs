namespace MusicPlayer.Web.Client.Infrastructure
{
    using MusicPlayer.Web.Shared;
    using MusicPlayer.Web.Shared.Authentication;
    using Microsoft.AspNetCore;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System;
    using MusicPlayer.Web.Shared.Songs;

    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        private readonly IAppState applicationState;
        private readonly IJSRuntime jsRuntime;
        private const string envUrl = "https://localhost:5002";
        public ApiClient(HttpClient http, IAppState state, IJSRuntime runtime) 
        {
            this.httpClient = http;
            this.jsRuntime = runtime;
            this.applicationState = state;
        }
        public Task<ApiResponse<AddSongResponseModel>> AddSong(AddSongInputModel request) =>
            this.PostJson<AddSongInputModel, AddSongResponseModel>(envUrl + "/api/songs/add", request);
        private async Task<ApiResponse<TResponse>> PostJson<TRequest, TResponse>(string url, TRequest request)
        {
            if (this.applicationState.IsLoggedIn)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.applicationState.UserToken);
            }
            else if (await this.jsRuntime.ReadToken() != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.applicationState.UserToken);
            }

            try
            {
                var response = await this.httpClient.PostAsJsonAsync(url, request);
                var responseObject = await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
                return responseObject;
            }
            catch (Exception ex)
            {
                return new ApiResponse<TResponse>(new ApiError("HTTP Client", ex.Message));
            }
        }
        private async Task<ApiResponse<TResponse>> GetJson<TResponse>(string url)
        {
            if (this.applicationState.IsLoggedIn)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.applicationState.UserToken);
            }
            //else if (await this.jsRuntime.ReadToken() != null)
            //{
            //    this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.applicationState.UserToken);
            //}

            try
            {
                return await this.httpClient.GetFromJsonAsync<ApiResponse<TResponse>>(url);
            }
            catch (Exception exception)
            {
                return new ApiResponse<TResponse>(new ApiError("HTTP Client", exception.Message));
            }
        }
    }
}
