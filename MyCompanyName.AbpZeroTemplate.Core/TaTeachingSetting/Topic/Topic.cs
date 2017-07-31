using Abp.Domain.Entities.Auditing;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.TaTeachingSetting.Topic
{
    public class Topic : FullAuditedEntity<long,User>
    {
        //专题名称
        public const int MaxNameLength = 50;//名称最大长度为50
        [Required]//前端检验
        [MaxLength(MaxNameLength)]//前端检验
        public string Name { get; set; }
        //专题描述
        public const int MaxDescriptionLength = 500;//名称最大长度为500
        [Required]//前端检验
        [MaxLength(MaxDescriptionLength)]//前端检验
        public string Description { get; set; }
        public Topic(string name,string description=null):this()
        {
            Name = name;
            Description = description;
        }
        public Topic()
        {

        }
    }
}
