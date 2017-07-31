using Abp.Web.Models;
using Abp.Web.Mvc.Controllers;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.Photo.Dto;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory;
using MyCompanyName.AbpZeroTemplate.TaSystemSetting.PhotoCategory.Dto;
using MyCompanyName.AbpZeroTemplate.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyCompanyName.AbpZeroTemplate.Web.Areas.TaSystemSetting.Controllers
{
    public class PhotoController : AbpController
    {
        // GET: TaSystemSetting/Photo
        private readonly IPhotoAppService _photoAppService;
        private readonly IPhotoCategoryAppService _photoCategoryAppService;
        protected readonly List<string> filesPath;
        public PhotoController(IPhotoAppService photoAppService, IPhotoCategoryAppService photoCategoryAppService)
        {
            _photoAppService = photoAppService;
            _photoCategoryAppService = photoCategoryAppService;
            filesPath = new List<string>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            InitializeDropdowmList();
            var thePhotoCagegoryDto = new PhotoDto();
            return View(thePhotoCagegoryDto);
        }

        [HttpPost]
        [DontWrapResult]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(PhotoDto input)
        {
            //string[] fillName = new string[] { };
            if (!ModelState.IsValid)
            {
                return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新输入"));
            }
            OperationResult theOperationResult;
            try
            {
                _photoAppService.CreatePhoto(input);
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
            InitializeDropdowmList();
            var thePhotoCagegoryDto = await _photoAppService.GetPhoto(id);
            var allUrl = new List<string>();
            if (thePhotoCagegoryDto != null)
            {
                allUrl.Add(thePhotoCagegoryDto.Url);
                ViewBag.PhotoCattegoryId = thePhotoCagegoryDto.PhotoCategoryId;
                ViewBag.PhotoCattegoryName = _photoCategoryAppService.GetPhotoCategory(thePhotoCagegoryDto.PhotoCategoryId);
            }
            ViewBag.PhotoCagegoryDtoUrl = allUrl;


            if (thePhotoCagegoryDto == null)
            {
                return HttpNotFound();
            }
            return View("Edit", thePhotoCagegoryDto);
        }

        [HttpPost]
        [DontWrapResult]
        public ActionResult Edit(PhotoDto input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新检查输入"));
            }
            OperationResult theOperationResult;
            try
            {
                _photoAppService.UpdatePhoto(input);
                theOperationResult = new OperationResult(OperationResultType.Success, "更新数据成功！");
            }
            catch (Exception e)
            {
                theOperationResult = new OperationResult(OperationResultType.Error, "更新数据失败!") { Message = e.Message };
            }
            return Json(theOperationResult);
        }


        public async Task<JsonResult> Delete(long id)
        {
            OperationResult theOperationResult;
            try
            {
                await _photoAppService.DeletePhoto(id);
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
        public async Task<JsonResult> AutoCompletePhotoName(string term, int top = 10)
        {
            if (string.IsNullOrEmpty(term))
            {
                return null;
            }
            var items =
                await _photoAppService.GetPhotoCategories(new GetPhotoInput { Name = term.Trim() });
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
            string photoName)
        {
            var thePhotoCagegoryTableData = await
                _photoAppService.GetPhotoCategories(new GetPhotoInput { Name = photoName });

            List<PhotoListDto> thePhotoCagegoryTableDtoPageList = thePhotoCagegoryTableData.ToPagedList(
                pageNumber, pageSize);
            dynamic bootstrapTableJsonData = new ExpandoObject();
            bootstrapTableJsonData.total = thePhotoCagegoryTableData.Count;
            bootstrapTableJsonData.rows = thePhotoCagegoryTableDtoPageList;
            return Content(JsonConvert.SerializeObject(bootstrapTableJsonData));
        }

        public void InitializeDropdowmList()
        {
            var photoCategorys = _photoCategoryAppService.GetPhotoCategories();
            var photoCategoryList = new List<SelectListItem>();

            foreach (var item in photoCategorys)
            {
                photoCategoryList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            ViewBag.PhotoCategoryList = photoCategoryList;
        }

        public JsonResult Addedfile()
        {
            var model = _photoAppService.GetPhotoCategories(new GetPhotoInput { Name = "" });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #region 照片上传相应函数
        /// <summary>
        ///     图片缩略图生成算法
        /// </summary>
        /// <param name="intWidth">宽度</param>
        /// <param name="intHeight">高度</param>
        /// <param name="inputImgFile">文件路径</param>
        /// <param name="outImgFile">保存文件路径</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static bool MakeThumbnail(int intWidth, int intHeight, string inputImgFile, string outImgFile,
            string filename)
        {
            var oldimage = Image.FromFile(inputImgFile + filename);
            float newWidth; // 新的宽度    
            float newHeight; // 新的高度    
            float oldWidth, oldHeight; //原始高宽    
            var flat = 0; //标记图片是不是等比   
            var xPoint = 0; //若果要补白边的话，原图像所在的x，y坐标。    
            var yPoint = 0;
            //判断图片    
            oldWidth = oldimage.Width;
            oldHeight = oldimage.Height;
            if (oldWidth / oldHeight > intWidth / (float)intHeight) //当图片太宽的时候    
            {
                newHeight = oldHeight * (intWidth / oldWidth);
                newWidth = intWidth;
                //此时x坐标不用修改    
                yPoint = (int)((intHeight - newHeight) / 2);
                flat = 1;
            }
            else if (oldimage.Width / oldimage.Height == intWidth / (float)intHeight)
            {
                newWidth = intWidth;
                newHeight = intHeight;
            }
            else
            {
                newWidth = oldimage.Width * (intHeight / (float)oldimage.Height); //太高的时候   
                newHeight = intHeight;
                //此时y坐标不用修改    
                xPoint = (int)((intWidth - newWidth) / 2);
                flat = 1;
            }

            // ＝＝＝缩小图片＝＝＝    
            //调用缩放算法
            var thumbnailImage = Makesmallimage(oldimage, (int)newWidth, (int)newHeight);
            var bm = new Bitmap(thumbnailImage);

            if (flat != 0)
            {
                var bmOutput = new Bitmap(intWidth, intHeight);
                var gc = Graphics.FromImage(bmOutput);
                var tbBg = new SolidBrush(Color.White);
                gc.FillRectangle(tbBg, 0, 0, intWidth, intHeight); //填充为白色 
                gc.DrawImage(bm, xPoint, yPoint, (int)newWidth, (int)newHeight);
                bmOutput.Save(outImgFile + filename);
            }
            else
            {
                bm.Save(outImgFile + filename);
            }
            oldimage.Dispose();
            return true;
        }

        /// <summary>
        ///     生成缩略图 (高清缩放)
        /// </summary>
        /// <param name="originalImage">原图片</param>
        /// <param name="width">缩放宽度</param>
        /// <param name="height">缩放高度</param>
        /// <returns></returns>
        public static Image Makesmallimage(Image originalImage, int width, int height)
        {
            var towidth = 0;
            var toheight = 0;
            if (originalImage.Width > width && originalImage.Height < height)
            {
                towidth = width;
                toheight = originalImage.Height;
            }

            if (originalImage.Width < width && originalImage.Height > height)
            {
                towidth = originalImage.Width;
                toheight = height;
            }
            if (originalImage.Width > width && originalImage.Height > height)
            {
                towidth = width;
                toheight = height;
            }
            if (originalImage.Width < width && originalImage.Height < height)
            {
                towidth = originalImage.Width;
                toheight = originalImage.Height;
            }
            var x = 0; //左上角的x坐标 
            var y = 0; //左上角的y坐标 
            //新建一个bmp图片 
            Image bitmap = new Bitmap(towidth, toheight);
            //新建一个画板 
            var g = Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, x, y, towidth, toheight);
            originalImage.Dispose();
            g.Dispose();
            return bitmap;
        }

        public ActionResult Upload()
        {
            return View();
        }

        //[HttpPost]
        [DontWrapResult]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult BatchUpload()
        {
            var isSavedSuccessfully = true;
            var count = 0;
            var msg = "";

            //这里是获取隐藏域中的数据
            var albumId = string.IsNullOrEmpty(Request.Params["Id"])
                ? 0
                : int.Parse(Request.Params["Id"]);

            try
            {
                var directoryPath = Server.MapPath("~/Image/Photo");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                foreach (string f in Request.Files)
                {
                    var file = Request.Files[f];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = file.FileName;
                        var fileNewName = fileName;
                        var filePath = Path.Combine(directoryPath, fileNewName);
                        file.SaveAs(filePath);
                        filesPath.Add(fileNewName);
                        count++;
                    }
                }
                //theOperationResult = new OperationResult(OperationResultType.Success, "上传照片成功！");
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //theOperationResult = new OperationResult(OperationResultType.Error, "上传照片失败!") { Message = ex.Message };
                isSavedSuccessfully = false;
            }
            return Json(new
            {
                Result = isSavedSuccessfully,
                FilesPath = filesPath,
                Count = count,
                Message = msg
                //,
                //theOperationResult
            }/*, JsonRequestBehavior.AllowGet*/);
        }
        [DontWrapResult]
        public JsonResult RealDelete(string deleteFillName, bool flag,long id)
        {
            //bool flag = false;
            OperationResult theOperationResult;
            try
            {
                var path = Server.MapPath("~/Image/Photo/") + deleteFillName;
                //判断文件夹中是否含有当前要删除的文件
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    theOperationResult = new OperationResult(OperationResultType.Success, "已从文件中删除制定文件！！");
                    //判断是否需要从数据库中删除数据
                    if (flag)
                    {
                        //这里是获取隐藏域中的数据
                        var albumId = string.IsNullOrEmpty(Request.Params["Id"])
                            ? 0
                            : long.Parse(Request.Params["Id"]);
                        _photoAppService.DeletePhoto(id,deleteFillName);

                        theOperationResult = new OperationResult(OperationResultType.Success, "已为您彻底删除该图片！");
                    }
                    else
                    {
                        //theOperationResult = new OperationResult(OperationResultType.Error, "删除图片失败！");
                    }
                }
                else
                {
                    theOperationResult = new OperationResult(OperationResultType.Error, "已从页面删除，但文件尚未上传");
                }


            }
            catch (Exception)
            {
                theOperationResult = new OperationResult(OperationResultType.Error, "已从页面删除，但文件尚未上传!");
            }
            return Json(theOperationResult, JsonRequestBehavior.AllowGet);
        }
        #endregion 

    }
}