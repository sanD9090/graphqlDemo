﻿using GraphqlDemo.DTOs;

namespace GraphqlDemo.Schema.Queries
{
    public class StudentType
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GPA { get; set; }
        public IEnumerable<CourseDTO> Courses{ get; set; }
    }
}
