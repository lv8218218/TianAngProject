using Abp.Application.Services;
using MyCompanyName.AbpZeroTemplate.TaTeachingSetting.Topic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.TaTeachingSetting.Topic
{
    /// <summary>
    /// 定义接口目的是实现数据库增删查改刷新
    /// </summary>
    public interface ITopicAppService : IApplicationService
    {
        Task<List<TopicListDto>> GetTopics(GetTopicInput input);//得到专题名称以便查询
        Task CreateTopic(TopicDto input);//根据名称新增
        Task UpdateTopic(TopicDto input);//根据名称刷新
        Task DeleteTopic(long id);//根据编号删除（软删除）
        Task<TopicDto> GetTopic(long id);//得到专题编号以便查询

    }
}
