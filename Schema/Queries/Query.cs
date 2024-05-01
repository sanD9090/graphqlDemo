using Bogus;
using GraphqlDemo.DTOs;
using GraphqlDemo.Models;
using GraphqlDemo.Services.Courses;

namespace GraphqlDemo.Schema.Queries
{
    public class Query
    {
        private readonly Faker<InstructorType> _instructorFaker;
        private readonly Faker<StudentType> _studentFaker;
        private readonly Faker<CourseType> _courseFaker;
        private readonly CoursesRepository _courseRepository;

        public Query(CoursesRepository coursesRepository)
        {
            _courseRepository = coursesRepository;
        }

        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            IEnumerable<CourseDTO> courseDTOs = await _courseRepository.GetAll();
            return courseDTOs.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                Instructor = new InstructorType()
                {
                    Id = c.Instructor.Id,
                    FirstName = c.Instructor.FirstName,
                    LastName = c.Instructor.LastName,
                    Salary = c.Instructor.Salary,
                }
            });
        }

        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            CourseDTO courseDto =  await _courseRepository.GetById(id);
            return new CourseType()
            {
                Id = courseDto.Id,
                Name = courseDto.Name,
                Subject = courseDto.Subject,
                Instructor = new InstructorType()
                {
                    Id = courseDto.Instructor.Id,
                    FirstName = courseDto.Instructor.FirstName,
                    LastName = courseDto.Instructor.LastName,
                    Salary = courseDto.Instructor.Salary,
                }
            };
        }

        [GraphQLDeprecated("This query is deprecated.")]
        public string Instructions => "Smash that like button and subscribe to SingletonSean";
    }
}
