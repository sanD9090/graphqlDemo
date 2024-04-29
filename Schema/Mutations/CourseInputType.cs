using GraphqlDemo.Models;
using GraphqlDemo.Schema.Queries;

namespace GraphqlDemo.Schema.Mutations
{
    public class CourseInputType
    {
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }
    }
}
