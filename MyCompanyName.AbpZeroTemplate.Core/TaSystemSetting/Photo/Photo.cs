using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory;
using Abp.Timing;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo
{
    public class Photo : FullAuditedEntity<long, User> //, FullAuditedEntity< PhotoCategoryDto >
    {
        public const int MaxNameLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64kb

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public long PhotoCategoryId { get; set; }
        [ForeignKey("PhotoCategoryId")]
        public MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory.PhotoCategory PhotoCategory { get; set; }

        public Photo()
        {

        }

        public Photo(string url, string description = null) : this()
        {
            Url  = url;
            Description = description;
        }
    }
}
