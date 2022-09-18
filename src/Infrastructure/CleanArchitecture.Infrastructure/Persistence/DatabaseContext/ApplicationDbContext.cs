

using CleanArchitecture.Application.Common.Interfaces.DatabaseContext;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Persistence.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        SetCreationAuditProperties(entry.Entity);
                        break;

                    case EntityState.Modified:
                        SetModificationAuditProperties(entry.Entity);
                        break;

                    case EntityState.Deleted:
                        CancelDeletionForSoftDelete(entry);
                        SetModificationAuditProperties(entry.Entity);
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        private void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (entry.Entity is not ISoftDelete)
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDelete>().IsDeleted = true;
        }

        private void SetModificationAuditProperties(object entity)
        {
            if (entity is not IModificationAudit)
            {
                //Object does not implement ICreationAudited
                return;
            }
            var entityWithModifiedAudit = entity as IModificationAudit;

            if (entityWithModifiedAudit == null)
            {
                return;
            }

            SetModifiedAuditProperty(entityWithModifiedAudit);

            SetModifiedByAuditProperty(_currentUserService.UserId, entityWithModifiedAudit);
        }

        private void SetCreationAuditProperties(object entity)
        {
            if (entity is not ICreationAudit)
            {
                //Object does not implement ICreationAudited
                return;
            }
            var entityWithCreateAudit = entity as ICreationAudit;

            if (entityWithCreateAudit is null)
            {
                return;
            }

            SetCreatedAuditProperty(entityWithCreateAudit);

            SetCreatedByAuditProperty(_currentUserService.UserId, entityWithCreateAudit);
        }

        private static void SetCreatedAuditProperty(ICreationAudit? entityWithCreateAudit)
        {
            if (entityWithCreateAudit!.Created == default(DateTime))
            {
                entityWithCreateAudit.Created = DateTime.Now;
            }
        }

        private static void SetCreatedByAuditProperty(string userId, ICreationAudit? entityWithCreateAudit)
        {
            if (string.IsNullOrWhiteSpace(userId) && entityWithCreateAudit!.CreatedBy != null)
            {
                //Unknown user or Id already set in database table
                return;
            }

            //Finally, set CreatorUserId!
            entityWithCreateAudit!.CreatedBy = userId;
        }

        private static void SetModifiedAuditProperty(IModificationAudit? entityWithModifiedAudit)
        {
            if (entityWithModifiedAudit!.LastModified == default(DateTime))
            {
                entityWithModifiedAudit.LastModified = DateTime.Now;
            }
        }

        private static void SetModifiedByAuditProperty(string userId, IModificationAudit? entityWithModifiedAudit)
        {
            if (string.IsNullOrWhiteSpace(userId) && entityWithModifiedAudit!.LastModifiedBy != null)
            {
                //Unknown user or Id already set in database table
                return;
            }

            //Finally, set CreatorUserId!
            entityWithModifiedAudit!.LastModifiedBy = userId;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
