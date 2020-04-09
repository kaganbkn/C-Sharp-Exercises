using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.Api.DbContexts;
using CourseLibrary.Api.Entities;
using CourseLibrary.Api.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Api.Services
{
    public class CourseLibraryRepository : ICourseLibraryRepository, IDisposable
    {
        private readonly CourseLibraryContext _context;

        public CourseLibraryRepository(CourseLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCourse(Guid authorId, Course course)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            // always set the AuthorId to the passed-in authorId
            course.AuthorId = authorId;
            _context.Courses.Add(course);
        }

        public void DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
        }

        public async Task<Course> GetCourseAsync(Guid authorId, Guid courseId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return await _context.Courses.FirstOrDefaultAsync(c => c.AuthorId == authorId && c.Id == courseId);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return await _context.Courses
                        .Where(c => c.AuthorId == authorId)
                        .OrderBy(c => c.Title).ToListAsync();
        }

        public void UpdateCourse(Course course)
        {
            // no code in this implementation
        }

        public void AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            // the repository fills the id (instead of using identity columns)
            author.Id = Guid.NewGuid();

            foreach (var course in author.Courses)
            {
                course.Id = Guid.NewGuid();
            }

            _context.Authors.Add(author);
        }

        public async Task<bool> AuthorExistsAsync(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return await _context.Authors.AnyAsync(a => a.Id == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Remove(author);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(AuthorsResourceParameters authorsResource)
        {
            if (authorsResource == null)
            {
                throw new ArgumentNullException(nameof(authorsResource));
            }

            if (string.IsNullOrWhiteSpace(authorsResource.MainCategory) 
                && string.IsNullOrWhiteSpace(authorsResource.SearchQuery))
            {
                return await GetAuthorsAsync();
            }

            //IQueryable<Author> queryableAuthors;
            var collection = _context.Authors as IQueryable<Author>; // Cast operation
            
            if (!string.IsNullOrWhiteSpace(authorsResource.MainCategory)) //filter
            {
                var mainCategory = authorsResource.MainCategory.Trim();
                collection = collection.Where(c => c.MainCategory == mainCategory);
            }

            if (!string.IsNullOrWhiteSpace(authorsResource.SearchQuery))  //search
            {
                var searchQuery = authorsResource.SearchQuery.Trim();
                collection = collection.Where(c => c.FirstName.Contains(searchQuery)
                || c.LastName.Contains(searchQuery)
                || c.MainCategory.Contains(searchQuery));
            }

            return await collection.ToListAsync();
        }

        public async Task<Author> GetAuthorAsync(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }

            return await _context.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToListAsync();
        }

        public void UpdateAuthor(Author author)
        {
            // no code in this implementation
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
