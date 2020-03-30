using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Books.Api.Entities;
using Books.Api.ExternalModels;
using Books.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.Api.Filters
{
    //We also implement that in the controller.
    public class BookWithCoversResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if (resultFromAction?.Value == null
                || resultFromAction.StatusCode < 200
                || resultFromAction.StatusCode >= 300)
            {
                await next(); //its middleware because of we ınvoke next middleware
                return;
            }
            
            var (book, bookCovers) = ((Book, IEnumerable<BookCover>))resultFromAction.Value;

            var mappedBook = Mapper.Map<BookWithCoversDto>(book);
            resultFromAction.Value = Mapper.Map(bookCovers, mappedBook);


            await next();
        }
    }
}
