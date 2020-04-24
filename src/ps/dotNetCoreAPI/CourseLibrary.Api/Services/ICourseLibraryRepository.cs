﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseLibrary.Api.Entities;
using CourseLibrary.Api.Helpers;
using CourseLibrary.Api.ResourceParameters;

namespace CourseLibrary.Api.Services
{
    public interface ICourseLibraryRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync(Guid authorId);
        Task<Course> GetCourseAsync(Guid authorId, Guid courseId);
        void AddCourse(Guid authorId, Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<IEnumerable<Author>> GetAuthorsWithIdsAsync(IEnumerable<Guid> authorIds);
        PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);
        Task<Author> GetAuthorAsync(Guid authorId);
        Task<IEnumerable<Author>> GetAuthorsAsync(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void AddAuthorRange(IEnumerable<Author> author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        Task<bool> AuthorExistsAsync(Guid authorId);
        Task<bool> SaveAsync();
    }
}
