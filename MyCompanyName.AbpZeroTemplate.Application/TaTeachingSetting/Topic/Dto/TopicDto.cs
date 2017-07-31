using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.TaTeachingSetting.Topic.Dto
{
    [AutoMap(typeof(Topic))]
    
    public class TopicDto:EntityDto<long>//与core文件中的实体类类型一致，主键为long类型
    {
        [Required]//前端检验
        [DisplayName("专题名称")]
        [StringLength(Topic.MaxNameLength)]//前端检验实现名称长度不超过50
        public string Name { get; set; }
        [DisplayName("专题描述")]
        [StringLength(Topic.MaxDescriptionLength)]//前端检验实现描述长度不超过500
        public string Description { get; set; }
    }
}
