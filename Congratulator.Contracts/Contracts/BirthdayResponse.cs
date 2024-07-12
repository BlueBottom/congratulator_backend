namespace Congratulator.API.Contracts
{
    public record BirthdayResponse(
        Guid Id, 
        string Name,
        string Description,
        DateTime Date,
        byte[] Image
         );
}
