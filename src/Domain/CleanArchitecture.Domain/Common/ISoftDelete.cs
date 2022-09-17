
using System.ComponentModel;

namespace CleanArchitecture.Domain.Common
{
    public interface ISoftDelete
    {
        [DefaultValue(false)]
        bool IsDeleted { get; set; }
    }
}
