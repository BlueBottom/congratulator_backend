using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Congratulator.Contracts.Contracts
{
    public class BirthdayRequest
    {
        [StringLength(200)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public IFormFile? Image { get; set; }
    }
}


