namespace WebTutorialsApp.Api.Models.Requests
{
    public class RequestPaginatorModel
    {
        public int? MaxPageItems { get; set; } = 10;
        public int? PageIndex { get; set; } = 0;
    }
}
