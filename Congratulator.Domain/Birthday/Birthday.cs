namespace Congratulator.Domain.Birthday
{
    public class Birthday
    {
        public Guid Id { get; set; }
        public string Name { get; set;  } = string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public byte[]? Image { get; set; }
    }
}
