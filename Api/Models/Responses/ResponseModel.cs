namespace WebTutorialsApp.Api.Models
{
    public class ResponseModel
    {
        public dynamic Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int TotalPageItems { get; set; }
        public int MaxPageItems { get; set; }
        public int PageIndex { get; set; }
    }
}
