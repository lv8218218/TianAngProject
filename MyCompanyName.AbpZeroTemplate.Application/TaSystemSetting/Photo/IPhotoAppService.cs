using Abp.Application.Services;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo
{
    public interface IPhotoAppService : IApplicationService
    {
        Task<List<PhotoListDto>> GetPhotoCategories(GetPhotoInput input);

        Task CreatePhoto(PhotoDto input);

        Task UpdatePhoto(PhotoDto input);

        #region 补充
        void UpdateThePhoto(PhotoDto input);
        #endregion

        Task DeletePhoto(long id);

        Task<PhotoDto> GetPhoto(long id);

        void DeletePhoto(long id, string url);


    }
}
