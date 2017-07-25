using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory.Dto
{
    [AutoMapFrom(typeof(PhotoCategory))]
    public class PhotoCategoryListDto : EntityDto<long>, IHasModificationTime
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
    }
}