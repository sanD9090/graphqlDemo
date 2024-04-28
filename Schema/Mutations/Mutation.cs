using GraphqlDemo.Schema.Queries;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace GraphqlDemo.Schema.Mutations
{
    public class Mutation
    {

        private readonly List<CourseResult> _courses;


        public Mutation()
        {
            _courses = new List<CourseResult>();
        }
        public CourseResult CreateCourse(CourseInputType courseInput)
        {
            CourseResult courseType = new()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId

            };

            _courses.Add(courseType);

            return courseType;
        }

        public CourseResult UpdateCourse(Guid id, CourseInputType courseInput)
        {
            CourseResult? course = _courses.FirstOrDefault(c => c.Id == id) ?? throw new GraphQLException(new Error("Course Not Found", "COURSE_NOT_FOUND"));
            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.InstructorId = courseInput.InstructorId;
            return course;
        }

        public bool DeleteCourse(Guid id)
        {
            return _courses.RemoveAll( c => c.Id == id) >= 1 ;
        }
    }
}
