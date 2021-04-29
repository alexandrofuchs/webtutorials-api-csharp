using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Api.Models.Responses.Courses
{
    public class CourseCreatedModel : ResponseModel
    {
        public Subsection Data { get; set; }
    }
}
