﻿
namespace CleanArchitecture.Domain.Common
{
    public class BaseAuditEntity : ICreationAudit, IModificationAudit
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
