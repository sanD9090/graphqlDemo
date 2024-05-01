using GraphqlDemo.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphqlDemo.Services.Courses
{
    public class CoursesRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public CoursesRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<IEnumerable<CourseDTO>> GetAll()
        {
            using SchoolDbContext context = _contextFactory.CreateDbContext();

            return await context.Courses.Include(c => c.Instructor).Include(c => c.Students).ToListAsync();
        }

        public async Task<CourseDTO?> GetById(Guid id)
        {
            using SchoolDbContext context = _contextFactory.CreateDbContext();
            return await context.Courses.Include(c => c.Instructor).Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<CourseDTO> Create(CourseDTO course)
        {

            try
            {


                using SchoolDbContext context = _contextFactory.CreateDbContext();
                context.Courses.Add(course);
                await context.SaveChangesAsync();
                return course;
            }
            catch (Exception ex)
            {

                var dd = ex;
                return course;

            }
        }

        public async Task<CourseDTO> Update(CourseDTO course)
        {
            using SchoolDbContext context = _contextFactory.CreateDbContext();
            context.Courses.Update(course);

            await context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> Delete(Guid id)
        {
            using SchoolDbContext context = _contextFactory.CreateDbContext();
            CourseDTO course = new()
            {
                Id = id
            };

            context.Remove(course);
            return await context.SaveChangesAsync() > 0;
        }


    }
}
