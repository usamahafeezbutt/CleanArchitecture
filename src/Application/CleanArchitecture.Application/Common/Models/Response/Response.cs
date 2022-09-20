
namespace CleanArchitecture.Application.Common.Models.Response
{
    public class Response<T> : BaseResponse
    {
        public T? Result { get; set; }
    }
}
