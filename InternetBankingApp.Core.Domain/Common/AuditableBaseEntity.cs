using System.ComponentModel.DataAnnotations;

namespace MiniRedSocial.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedby { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
