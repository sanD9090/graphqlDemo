using GraphqlDemo.DTOs;
using GraphqlDemo.Schema.Queries;
using GraphqlDemo.Schema.Subscriptions;
using GraphqlDemo.Services.Courses;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace GraphqlDemo.Schema.Mutations
{
    public class Mutation
    {

        private readonly List<CourseResult> _courses;
        private readonly CoursesRepository _coursesRepository;

        public Mutation(CoursesRepository coursesRepository)
        {
            _courses = new List<CourseResult>();
            _coursesRepository = coursesRepository;
        }

       
        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {

            CourseDTO courseDto = new()
            {
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,
            };


            courseDto = await _coursesRepository.Create(courseDto);


            CourseResult course = new()
            {
                Id = courseDto.Id,
                Name = courseDto.Name,
                Subject = courseDto.Subject,
                InstructorId = courseDto.InstructorId

            };


            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

            return course;
        }

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {

            CourseDTO courseDto = new()
            {
                Id = id,
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,
            };

            courseDto = await _coursesRepository.Update(courseDto);


            CourseResult course = new()
            {
                Id = courseDto.Id,
                Name = courseDto.Name,
                Subject = courseDto.Subject,
                InstructorId = courseDto.InstructorId

            };

            string updatedCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";

            await topicEventSender.SendAsync(updatedCourseTopic, course);
            return course;
        }

        public async Task<bool> DeleteCourse(Guid id)
        {
            try
            {


                return await _coursesRepository.Delete(id);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
