namespace CleanArchitecture.Domain.Common
{
    public interface IModificationAudit
    {
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
