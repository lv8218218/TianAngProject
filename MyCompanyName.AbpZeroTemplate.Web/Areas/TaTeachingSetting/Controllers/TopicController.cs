using Abp.Web.Models;
using Abp.Web.Mvc.Controllers;
using MyCompanyName.AbpZeroTemplate.TaTeachingSetting.Topic;
using MyCompanyName.AbpZeroTemplate.TaTeachingSetting.Topic.Dto;
using MyCompanyName.AbpZeroTemplate.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyCompanyName.AbpZeroTemplate.Web.Areas.TaTeachingSetting.Controllers
{
    public class TopicController : AbpController
    {
        // GET: TaTeachingSetting/Topic
        private readonly ITopicAppService _topicAppService;
        public TopicController(ITopicAppService topicAppService)
        {
            _topicAppService = topicAppService;
        }
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var theTopicDto = new TopicDto();
            return PartialView(theTopicDto);
        }
       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult Create(TopicDto input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新输入"));
            }
            OperationResult theOperationResult;
            try
            {
                _topicAppService.CreateTopic(input);
                theOperationResult = new OperationResult(OperationResultType.Success, "保存数据成功");
            }
            catch (Exception e)
            {
                theOperationResult = new OperationResult(OperationResultType.Error, "保存数据失败") { Message = e.Message };
            }
            return Json(theOperationResult);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Edit(long id)
        {
            var theTopicDto = await _topicAppService.GetTopic(id);
            if (theTopicDto==null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit",theTopicDto);
        }
        [HttpPost]
        public ActionResult Edit(TopicDto input)
        {
            if (!ModelState.IsValid)
            {
                return Json(new OperationResult(OperationResultType.ParamError, "专题参数错误，请重新输入"));
            }
            OperationResult theOperationResult;
            try
            {
                _topicAppService.UpdateTopic(input);
                theOperationResult = new OperationResult(OperationResultType.Success, "更新成功");
            }
            catch (Exception e)
            {
                theOperationResult = new OperationResult(OperationResultType.Error, "更新失败") { Message=e.Message};
            }
            return Json(theOperationResult);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">根据id删除</param>
        /// <returns></returns>
        public async Task<JsonResult> Delete(long id)
        {
            OperationResult theOprationResult;
            try
            {
                await _topicAppService.DeleteTopic(id);
                theOprationResult = new OperationResult(OperationResultType.Success, "删除成功");
            }
            catch (Exception e)
            {
                theOprationResult = new OperationResult(OperationResultType.Error, "删除失败") { Message = e.Message };
            }
            return Json(theOprationResult,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 自动补全
        /// </summary>
        /// <param name="term"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        [DontWrapResult]
        public async Task<JsonResult> AutoCompleteTopicName(string term,int top=10)
        {
            if (string.IsNullOrEmpty(term))
            {
                return null;
            }
            var iterms = await _topicAppService.GetTopics(new GetTopicInput { Name=term.Trim()});
            var findModel = iterms.Take(top).Select(r => new
            {
                r.Id,
                r.Name
            });
            return Json(findModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 采集表的数据（暂未看懂）
        /// </summary>
        /// <param name="sortOder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="topicName"></param>
        /// <returns></returns>
        public async Task<ContentResult> GetTableData(string sortOder,int pageSize,int pageNumber,string topicName)
        {
            var theTopicTableData = await _topicAppService.GetTopics(new GetTopicInput { Name = topicName });
            List<TopicListDto>  theTopicTableDtoPageList = theTopicTableData.ToPagedList(pageNumber, pageSize);
            dynamic bootstrapTableJsonData = new ExpandoObject();
            bootstrapTableJsonData.total = theTopicTableData.Count;
            bootstrapTableJsonData.rows = theTopicTableDtoPageList;
            return Content(JsonConvert.SerializeObject(bootstrapTableJsonData));
        }
    }
}