using Microsoft.AspNetCore.Cors.Infrastructure;

namespace GraphqlDemo.Schema
{
    public class Mutation
    {

        private readonly List<CourseType> _courses;


        public Mutation()
        {
            _courses = new List<CourseType>();
        }
        public bool CreateCourse(string name, Subject subject, Guid instructorId)
        {
            CourseType courseType = new ()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Subject = subject,
                Instructor = new InstructorType()
                {
                    Id = instructorId,
                },
                
            };

            _courses.Add(courseType);

            return true;
        }
    }
}
