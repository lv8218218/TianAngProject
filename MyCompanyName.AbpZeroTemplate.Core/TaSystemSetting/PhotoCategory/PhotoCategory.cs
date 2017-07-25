using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory
{
    public class PhotoCategory : FullAuditedEntity<long, User>
    {
        public const int MaxNameLength = 256;
        public const int MaxDescriptionLength = 64*1024; //64kb

        public PhotoCategory()
        {
        }

        public PhotoCategory(string name, string description = null) : this()
        {
            Name = name;
            Description = description;
            
        }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }


        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
    }
}