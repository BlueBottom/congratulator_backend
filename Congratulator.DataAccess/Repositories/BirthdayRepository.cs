using Congratulator.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Congratulator.Domain.Birthday;

namespace Congratulator.DataAccess.Repositories
{
    public class BirthdayRepository : IBirthdayRepository
    {
        private readonly CongratulatorDbContext _context;

        public BirthdayRepository(CongratulatorDbContext context)
        {
            _context = context;
        }

        public async Task<List<Birthday>> Get(string intervalTime, string searchString)
        {
            var today = DateTime.Today;
            var dayOfYear = DateTime.Today.DayOfYear;
            var birthdayEntities = await _context.Birthdays.AsNoTracking().ToListAsync();

            return intervalTime switch
            {
                "all" => birthdayEntities.Where(x=>x.Name.Contains(searchString)).OrderBy(x=>x.Date.DayOfYear).ThenBy(x=>x.Name).ToList(),
                "today" => FilterBirthdays(birthdayEntities, searchString,x => x.Date.Day == today.Day && x.Date.Month == today.Month),
                "tomorrow" => FilterBirthdays(birthdayEntities, searchString,x => x.Date.DayOfYear - dayOfYear == 1, x => x.Name),
                "10 days" => FilterBirthdays(birthdayEntities, searchString,x => x.Date.DayOfYear - dayOfYear >= 0 && x.Date.DayOfYear - dayOfYear <= 10, x => x.Date.DayOfYear),
                "this month" => FilterBirthdays(birthdayEntities, searchString,x => x.Date.Month == today.Month, x => x.Date.Day),
                _ => FilterBirthdays(birthdayEntities, searchString,x => true).OrderBy(x=>x.Date.DayOfYear).ThenBy(x=>x.Name).ToList()
            };
            
            List<Birthday> FilterBirthdays(List<Birthday> birthdays, string searchString, Predicate<Birthday> predicate, Func<Birthday, object> ordering = null)
            {
                var filtered = birthdays.FindAll(predicate);
                
                if (ordering != null)
                {
                    filtered.Sort((x, y) => Comparer<object>.Default.Compare(ordering(x), ordering(y)));
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    filtered = filtered.Where(x => x.Name.Contains(searchString)).ToList();
                }
                return filtered;
            }
        }

        
        

        public async Task<Guid> Create(Birthday birthday)
        {
            var birthdayEntity = new Birthday
            {
                Id = birthday.Id,
                Name = birthday.Name,
                Description = birthday.Description,
                Date = birthday.Date,
                Image = birthday.Image
            };

            await _context.Birthdays.AddAsync(birthdayEntity);
            await _context.SaveChangesAsync();

            return birthdayEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string name, string description, DateTime date, byte[] image)
        {
            await _context.Birthdays
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Name, b => name)
                    .SetProperty(b => b.Description, b => description)
                    .SetProperty(b => b.Date, b => date)
                    .SetProperty(b => b.Image, b => image));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Birthdays
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }


}
