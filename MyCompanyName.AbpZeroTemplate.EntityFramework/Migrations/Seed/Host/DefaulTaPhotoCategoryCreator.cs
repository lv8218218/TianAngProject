using System.Collections.Generic;
using System.Linq;
using Abp.Timing;
using MyCompanyName.AbpZeroTemplate.EntityFramework;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory;

namespace MyCompanyName.AbpZeroTemplate.Migrations.Seed.Host
{
    /// <summary>
    ///     Lv：添加
    /// </summary>
    public class DefaulTaPhotoCategoryCreator
    {
        private static readonly List<PhotoCategory> _photoCategorys;
        private readonly AbpZeroTemplateDbContext _context;

        static DefaulTaPhotoCategoryCreator()
        {
            _photoCategorys = new List<PhotoCategory>
            {
                new PhotoCategory
                {
                    Name = "新闻照片",
                    Description = "用于新闻发布所使用的照片",
                    CreationTime = Clock.Now,
                    CreatorUserId = 3,
                    IsDeleted = false
                },
                new PhotoCategory
                {
                    Name = "课堂照片",
                    Description = "用于记录课堂信息所使用的照片",
                    CreationTime = Clock.Now,
                    CreatorUserId = 3,
                    IsDeleted = false
                }
            };
        }

        public DefaulTaPhotoCategoryCreator(AbpZeroTemplateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            foreach (var photoCategory in _photoCategorys)
            {
                if (_context.PhotoCategories.FirstOrDefault(t => t.Name == photoCategory.Name) == null)
                {
                    _context.PhotoCategories.Add(photoCategory);
                }
                _context.SaveChanges();
            }
        }
    }
}