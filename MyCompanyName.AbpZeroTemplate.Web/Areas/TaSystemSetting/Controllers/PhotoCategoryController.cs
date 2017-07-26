using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Models;
using Abp.Web.Mvc.Controllers;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory.Dto;
using MyCompanyName.AbpZeroTemplate.Web.Helpers;
using Newtonsoft.Json;

namespace MyCompanyName.AbpZeroTemplate.Web.Areas.TaSystemSetting.Controllers
{
    public class PhotoCategoryController : AbpController
    {
        // GET: TaSystemSetting/PhotoCategory
        private readonly IPhotoCategoryAppService _photoCategoryAppService;

        public PhotoCategoryController(IPhotoCategoryAppService photoCategoryAppService)
        {
            _photoCategoryAppService = photoCategoryAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var thePhotoCagegoryDto = new PhotoCategoryDto();
            return PartialView(thePhotoCagegoryDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhotoCategoryDto input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新输入"));
            }
            OperationResult theOperationResult;
            try
            {
                _photoCategoryAppService.CreatePhotoCategory(input);
                theOperationResult = new OperationResult(OperationResultType.Success, "保存数据成功！");
            }
            catch (Exception e)
            {
                theOperationResult = new OperationResult(OperationResultType.Error, "保存数据失败！") { Message = e.Message };
            }
            return Json(theOperationResult);
        }

        public async Task<ActionResult> Edit(long id)
        {
            var thePhotoCagegoryDto = await _photoCategoryAppService.GetPhotoCategory(id);
            if (thePhotoCagegoryDto == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit", thePhotoCagegoryDto);
        }

        [HttpPost]
        public ActionResult Edit(PhotoCategoryDto input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new OperationResult(OperationResultType.ParamError, "驾驶员参数错误，请重新检查输入"));
            }
            OperationResult theOperationResult;
            try
            {
                _photoCategoryAppService.UpdatePhotoCategory(input);
                theOperationResult = new OperationResult(OperationResultType.Success, "更新驾驶员数据成功！");
            }
            catch (Exception e)
            {
                theOperationResult = new OperationResult(OperationResultType.Error, "更新驾驶员数据失败!") { Message = e.Message };
            }
            return Json(theOperationResult);
        }


        public async Task<JsonResult> Delete(long id)
        {
            OperationResult theOperationResult;
            try
            {
                await _photoCategoryAppService.DeletePhotoCategory(id);
                theOperationResult = new OperationResult(OperationResultType.Success, "删除数据成功！");
            }
            catch (Exception e)
            {
                theOperationResult = new OperationResult(OperationResultType.Error, "删除数据失败!") { Message = e.Message };
            }
            return Json(theOperationResult, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///     名称自动补全
        /// </summary>
        [DontWrapResult]
        public async Task<JsonResult> AutoCompletePhotoCategoryName(string term, int top = 10)
        {
            if (string.IsNullOrEmpty(term))
            {
                return null;
            }
            var items =
                await _photoCategoryAppService.GetPhotoCategories(new GetPhotoCategoryInput { Name = term.Trim() });
            var findModel = items.Take(top)
                .Select(r => new
                {
                    r.Id,
                    r.Name
                });
            return Json(findModel, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///     获取表格数据
        /// </summary>
        public async Task<ContentResult> GetTableData(string sortOrder, int pageSize, int pageNumber,
            string photoCategoryName)
        {
            var thePhotoCagegoryTableData = await
                _photoCategoryAppService.GetPhotoCategories(new GetPhotoCategoryInput { Name = photoCategoryName });

            List<PhotoCategoryListDto> thePhotoCagegoryTableDtoPageList = thePhotoCagegoryTableData.ToPagedList(
                pageNumber, pageSize);
            dynamic bootstrapTableJsonData = new ExpandoObject();
            bootstrapTableJsonData.total = thePhotoCagegoryTableData.Count;
            bootstrapTableJsonData.rows = thePhotoCagegoryTableDtoPageList;
            return Content(JsonConvert.SerializeObject(bootstrapTableJsonData));
        }
    }
}