namespace WebTutorialsApp.Api.Models
{
    public abstract class ResponseModel
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int TotalPageItems { get; set; }
        public int MaxPageItems { get; set; }
        public int PageIndex { get; set; }
    }
}
