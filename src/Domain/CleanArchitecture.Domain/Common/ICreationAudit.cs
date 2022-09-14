

namespace CleanArchitecture.Domain.Common
{
    public interface ICreationAudit
    {
        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }
    }
}
