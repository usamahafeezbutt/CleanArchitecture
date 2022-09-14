
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace CleanArchitecture.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>, ICreationAudit, IModificationAudit, ISoftDelete
    {
        public int Name { get; set; }
        public int Address { get; set; }
        public int Phone { get; set; }
        public string ImageUrl { get; set; } = null!;
        public Subscription Subscription { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
    }
}
