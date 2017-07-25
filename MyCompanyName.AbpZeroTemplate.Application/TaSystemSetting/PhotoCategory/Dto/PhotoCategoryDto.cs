using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory.Dto
{
    [AutoMap(typeof(PhotoCategory))]
    public class PhotoCategoryDto : EntityDto<long>
    {
        [Required]
        [StringLength(PhotoCategory.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(PhotoCategory.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}