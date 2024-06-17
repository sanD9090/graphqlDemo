using GraphqlDemo.DataLoader;
using GraphqlDemo.DTOs;
using GraphqlDemo.Models;
using GraphqlDemo.Services.Instructors;

namespace GraphqlDemo.Schema.Queries
{

   
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        
        public Guid InstructorId { get; set; }

        // public InstructorType? Instructor { get; set; }
        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
        {
            InstructorDTO instructorDTO = await instructorDataLoader.LoadAsync(InstructorId, CancellationToken.None);
            
            return new InstructorType()
            {
                Id = instructorDTO.Id,
                FirstName = instructorDTO.FirstName,
                LastName = instructorDTO.LastName,
                Salary = instructorDTO.Salary,
            };
        }

        public IEnumerable<StudentType>? Students { get; set; }
    }
}
