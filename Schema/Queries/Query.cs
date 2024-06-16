using Bogus;
using GraphqlDemo.DTOs;
using GraphqlDemo.Models;
using GraphqlDemo.Services.Courses;

namespace GraphqlDemo.Schema.Queries
{
    public class Query
    {
        
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
                InstructorId = c.InstructorId
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
                InstructorId =  courseDto.InstructorId
                 
            };
        }

        [GraphQLDeprecated("This query is deprecated.")]
        public string Instructions => "Smash that like button and subscribe to SingletonSean";
    }
}
