using System.Collections.Generic;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Api.Models.Responses.Courses
{
    public class GetCoursesResponseModel : ResponseModel
    {
        public IEnumerable<Subsection> Data { get; set; }
    }
}
