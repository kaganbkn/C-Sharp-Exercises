using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookCovers.Api.Controllers
{
    [Route("api/bookcovers")]
    [ApiController]
    public class BookCoversController : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<IActionResult> GetBookCover(
            string name,
            bool returnFault = false)
        {
            // if returnFault is true, wait 500ms and
            // return an Internal Server Error
            if (returnFault)
            {
                await Task.Delay(500);
                return new StatusCodeResult(500);
            }

            // generate a "book cover" (byte array) between 2 and 10MB
            var random = new Random();
            var fakeCoverBytes = random.Next(10000000, 20000000);
            var fakeCover = new byte[fakeCoverBytes];
            random.NextBytes(fakeCover);

            return Ok(new
            {
                Name = name,
                Content = fakeCover
            });
        }
    }
}
