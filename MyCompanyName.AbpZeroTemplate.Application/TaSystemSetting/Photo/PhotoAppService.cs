using Abp.AutoMapper;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo
{
    public class PhotoAppService : AbpZeroTemplateAppServiceBase, IPhotoAppService
    {
        private readonly IRepository<Photo, long> _photoCategoryRepository;

        public PhotoAppService(IRepository<Photo, long> photoCategoryRepository)
        {
            _photoCategoryRepository = photoCategoryRepository;
        }

        public async Task CreatePhoto(PhotoDto input)
        {
            await _photoCategoryRepository.InsertAsync(input.MapTo<Photo>());
        }

        public async Task DeletePhoto(long id)
        {
            var thePhoto = await _photoCategoryRepository.GetAsync(id);
            await _photoCategoryRepository.DeleteAsync(thePhoto);
        }

        #region 非异步方法   用于测试
        /// <summary>
        /// 通过点击删除更新数据库
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        public void DeletePhoto(long id, string url)
        {
            var thePhoto = _photoCategoryRepository.GetAll();
            if (id != null)
            {
                thePhoto = thePhoto.Where(c => c.Id == id);
            }
            if (!string.IsNullOrEmpty(url))
            {
                thePhoto = thePhoto.Where(c => c.Url == url);
            }
            var list = thePhoto.ToList();
            if (list != null && list.Count > 0)
            {
                list[0].Url = "";
            }
            //var photoList = list.MapTo<List<PhotoListDto>>();
            //var photos = photoList.MapTo<List<Photo>>();
            _photoCategoryRepository.Update(list[0]);
        }
        /// <summary>
        /// 非异步更新
        /// </summary>
        /// <param name="input"></param>
        public void UpdateThePhoto(PhotoDto input)
        {
             _photoCategoryRepository.Update(input.MapTo<Photo>());
        }
        #endregion


        public async Task UpdatePhoto(PhotoDto input)
        {
            await _photoCategoryRepository.UpdateAsync(input.MapTo<Photo>());
        }

        

        public async Task<List<PhotoListDto>> GetPhotoCategories(GetPhotoInput input)
        {
            var photoCategories = _photoCategoryRepository.GetAll();
            if (!string.IsNullOrEmpty(input.Name))
            {
                photoCategories = photoCategories.Where(c => c.Name == input.Name);
            }
            photoCategories = photoCategories.OrderByDescending(c => c.LastModificationTime);
            var list = await photoCategories.ToListAsync();
            var photoCategoryList = list.MapTo<List<PhotoListDto>>();
            return photoCategoryList;
        }

        public async Task<PhotoDto> GetPhoto(long id)
        {
            var photoCategory = await _photoCategoryRepository.GetAsync(id);
            var dto = photoCategory.MapTo<PhotoDto>();
            return dto;
        }
    }
}