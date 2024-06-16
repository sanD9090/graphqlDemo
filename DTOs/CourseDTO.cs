using GraphqlDemo.Models;
using GraphqlDemo.Schema.Queries;
using GraphqlDemo.Services.Instructors;

namespace GraphqlDemo.DTOs
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorRepository instructoryRepository)
        {
            InstructorDTO instructor = await instructoryRepository.GetById(InstructorId);
            return new InstructorType()
            {
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Salary = instructor.Salary,
            };
        }

        public IEnumerable<StudentDTO> Students { get; set; }
    }
}
