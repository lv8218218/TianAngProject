using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory.Dto;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory
{
    public interface IPhotoCategoryAppService : IApplicationService
    {
        Task<List<PhotoCategoryListDto>> GetPhotoCategories(GetPhotoCategoryInput input);

        Task CreatePhotoCategory(PhotoCategoryDto input);

        Task UpdatePhotoCategory(PhotoCategoryDto input);

        Task DeletePhotoCategory(long id);

        Task<PhotoCategoryDto> GetPhotoCategory(long id);
    }
}