using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo.Dto
{
    [AutoMapFrom(typeof(Photo))]
    public class PhotoListDto: EntityDto<long>, IHasModificationTime
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
    }
}
