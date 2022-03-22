namespace MusicPlayer.Web.Shared
{
    using System.Collections.Generic;
    using System.Linq;
    public class ApiResponse<TData>
    {
        public ApiResponse()
        {

        }
        public ApiResponse(TData data)
        {
            this.Data = data;
        }
        public ApiResponse(IEnumerable<ApiError> errors)
            => this.Errors = errors;
        public ApiResponse(ApiError error) 
            => this.Errors = new List<ApiError> { error };
        public TData Data { get; private set; }
        public IEnumerable<ApiError> Errors { get; set; }
        public bool IsValid => this.Errors.Count() == 0 ? true : false;
    }
}
