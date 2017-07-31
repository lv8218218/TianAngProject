using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.TaTeachingSetting.Topic.Dto
{
    [AutoMapFrom(typeof(Topic))]//数据从core下的Topic传过来
    public class TopicListDto : Entity<long>, IHasModificationTime
    {
        public string Name { get; set; }//名称属性
        public string Description { get; set; }//描述属性
        public DateTime? LastModificationTime { get; set; } //最后一次修改时间（实现IHasModificationTime接口）

    }
}
