using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory.Dto;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory
{
    public class PhotoCategoryAppService : AbpZeroTemplateAppServiceBase, IPhotoCategoryAppService
    {
        private readonly IRepository<PhotoCategory, long> _photoCategoryRepository;

        public PhotoCategoryAppService(IRepository<PhotoCategory, long> photoCategoryRepository)
        {
            _photoCategoryRepository = photoCategoryRepository;
        }

        public async Task CreatePhotoCategory(PhotoCategoryDto input)
        {
            await _photoCategoryRepository.InsertAsync(input.MapTo<PhotoCategory>());
        }

        public async Task DeletePhotoCategory(long id)
        {
            var thePhotoCategory = await _photoCategoryRepository.GetAsync(id);
            await _photoCategoryRepository.DeleteAsync(thePhotoCategory);
        }

        public async Task UpdatePhotoCategory(PhotoCategoryDto input)
        {
            await _photoCategoryRepository.UpdateAsync(input.MapTo<PhotoCategory>());
        }

        public async Task<List<PhotoCategoryListDto>> GetPhotoCategories(GetPhotoCategoryInput input)
        {
            var photoCategories = _photoCategoryRepository.GetAll();
            if (!string.IsNullOrEmpty(input.Name))
            {
                photoCategories = photoCategories.Where(c => c.Name == input.Name);
            }
            photoCategories = photoCategories.OrderByDescending(c => c.LastModificationTime);
            var list = await photoCategories.ToListAsync();
            var photoCategoryList = list.MapTo<List<PhotoCategoryListDto>>();
            return photoCategoryList;
        }

        public List<PhotoCategoryListDto> GetPhotoCategories()
        {
            var photoCategories = _photoCategoryRepository.GetAll();            
            photoCategories = photoCategories.OrderByDescending(c => c.LastModificationTime);
            var list = photoCategories.ToList ();
            var photoCategoryList = list.MapTo<List<PhotoCategoryListDto>>();
            return photoCategoryList;
        }

        public async Task<PhotoCategoryDto> GetPhotoCategory(long id)
        {
            var photoCategory = await _photoCategoryRepository.GetAsync(id);
            var dto = photoCategory.MapTo<PhotoCategoryDto>();
            return dto;
        }
    }
}