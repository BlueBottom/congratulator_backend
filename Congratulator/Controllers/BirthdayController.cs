using System.Net.Mime;
using Congratulator.API.Contracts;
using Congratulator.Contracts.Contracts;
using Congratulator.Core.Abstractions;
using Congratulator.Domain.Birthday;
using Microsoft.AspNetCore.Mvc;

namespace Congratulator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BirthdaysController : ControllerBase
    {
        private readonly IBirthdayService _birthdayService;

        public BirthdaysController(IBirthdayService birthdayService)
        {
            _birthdayService = birthdayService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BirthdayResponse>>> GetBirthdays(string intervalTime = "", string searchString = "") 
        {
            var birthdays = await _birthdayService.GetAllBirthdays(intervalTime, searchString);

            var response = birthdays.Select(b => new BirthdayResponse(b.Id, b.Name, b.Description, b.Date, b.Image));

            return Ok(response);
        }
        /*/*#1#
        [HttpGet]
        public async Task<*/

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBirthday([FromForm] BirthdayRequest request)
        {
            var birthday = new Birthday
            {
                Name = request.Name,
                Description = request.Description,
                Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc),
            };

            if (request.Image != null)
            {
                using var target = new MemoryStream();
                await request.Image.CopyToAsync(target);
                birthday.Image = target.ToArray();
            }
            

            var birthdayId = await _birthdayService.CreateBirthday(birthday);

            return Ok(birthdayId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBirthday(Guid id, [FromForm] BirthdayRequest request)
        {
            var birthday = new Birthday
            {
                Name = request.Name,
                Description = request.Description,
                Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc),
            };
            if (request.Image != null)
            {
                using var target = new MemoryStream();
                await request.Image.CopyToAsync(target);
                birthday.Image = target.ToArray();
            }
            
            var birthdayId = await _birthdayService.UpdateBirthday(id, birthday.Name, birthday.Description, birthday.Date, birthday.Image);

            return Ok(birthdayId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBirthday(Guid id)
        {
            return Ok(await _birthdayService.DeleteBirthday(id));
        }
    }
}
