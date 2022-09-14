
namespace CleanArchitecture.Application.Common.Models.Emails
{
    public class EmailOptions
    {
        public List<string> ToEmails { get; set; } = null!;
        public List<string>? CCEmails { get; set; }
        public List<string>? BccEmails { get; set; }
        public string Subject { get; set; } = null!;
        public string? Body { get; set; }
        public List<KeyValuePair<string, string>>? PlaceHolders { get; set; }
    }
}
