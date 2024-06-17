using GraphqlDemo.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphqlDemo.Services.Instructors
{
    public class InstructorRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public InstructorRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
 

        public async Task<InstructorDTO?> GetById(Guid id)
        {
            using SchoolDbContext context = _contextFactory.CreateDbContext();
            return await context.Instructors.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<InstructorDTO>> GetManyByIds(IReadOnlyList<Guid> instructorIds)
        {
            try
            {
                using (SchoolDbContext context = _contextFactory.CreateDbContext())
                {
                    return await context.Instructors
                        .Where(i => instructorIds.Contains(i.Id))
                        .ToListAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
