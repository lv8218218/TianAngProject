using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo.Dto
{
    [AutoMap(typeof(Photo))]
    public class PhotoDto:EntityDto<long>
    {
        [Required]
        [DisplayName("照片名称")]
        public string Name { get; set; }
        [DisplayName("照片描述")]
        public string Description { get; set; }
        [Required]
        [DisplayName("照片地址")]
        public string Url { get; set; }
        [Required]
        [DisplayName("照片类型")]
        public long PhotoCategoryId { get; set; }
        ///// <summary>
        ///// 外键，链接  照片类型表
        ///// </summary>
        //public MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory.PhotoCategory PhotoCategory { get; set; }
        ////[AutoMapFrom(typeof(PhotoCategory))]
        
    }
}
