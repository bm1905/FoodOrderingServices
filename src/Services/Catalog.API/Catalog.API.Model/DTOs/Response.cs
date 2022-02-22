namespace Catalog.API.Model.DTOs
{
    public class Response<T>
    {
        public Response()
        {
            
        }

        public Response(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
    }
}
