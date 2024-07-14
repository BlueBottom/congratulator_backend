using Congratulator.Domain.Birthday;

namespace Congratulator.Core.Abstractions
{
    public interface IBirthdayService
    {
        Task<Guid> CreateBirthday(Birthday birthday);
        Task<Guid> DeleteBirthday(Guid id);
        Task<List<Birthday>> GetAllBirthdays(string intervalTime, string searchString);
        Task<Guid> UpdateBirthday(Guid id, string name, string description, DateTime date, byte[] image);
    }
}