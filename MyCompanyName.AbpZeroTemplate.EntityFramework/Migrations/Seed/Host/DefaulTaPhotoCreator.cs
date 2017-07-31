using Abp.Timing;
using MyCompanyName.AbpZeroTemplate.EntityFramework;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.Migrations.Seed.Host
{
    public class DefaulTaPhotoCreator
    {
        private static readonly List<Photo> _photos;
        private readonly AbpZeroTemplateDbContext _context;

        static DefaulTaPhotoCreator()
        {
            _photos = new List<Photo>
            {
                new Photo
                {
                    Name = "新闻照片",
                    Description = "新闻发布所使用的照片",
                    Url="*",
                    PhotoCategoryId =2,
                    CreationTime = Clock.Now,
                    CreatorUserId = 3,
                    IsDeleted = false
                },
                new Photo
                {
                    Name = "课堂照片",
                    Description = "课堂信息所使用的照片",
                    Url="*",
                    PhotoCategoryId =3,
                    CreationTime = Clock.Now,
                    CreatorUserId = 3,
                    IsDeleted = false
                }
            };
        }

        public DefaulTaPhotoCreator(AbpZeroTemplateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            foreach (var photo in _photos)
            {
                if (_context.Photos.FirstOrDefault(t => t.Name == photo.Name) == null)
                {
                    _context.Photos.Add(photo);
                }
                _context.SaveChanges();
            }
        }
    }
}
